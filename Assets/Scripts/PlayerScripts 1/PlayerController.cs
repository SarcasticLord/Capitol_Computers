// 

using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Components
    private CharacterController _controller;
    public static PlayerController instance = null;


    [Header("Inputs")]
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference slideAction;

    //States
    private enum PlayerState {IDLE, RUN, SLIDE, SLIDEWALK, DASH};
    private PlayerState _currentState = PlayerState.IDLE;

    //Player Ingame Stats
    private float _speed;
    private bool _isJumping;
    private bool _canMove = true;
    private float _playerHeight = 2.0f;
    private float _playerSlideHeight = 0.0f;
    private float _currSlideSpeed = 0.0f;

    private float _verticalVelocity;
    private bool _isGrounded;
    
    //Coyote timer variables
    private float _coyoteTimer;

    //Player Inputs
    private Vector3 _inputDirection;
    private Vector2 _moveInput;
    private bool _slideInput;

    [Header("Player Base Stats")]
    public float playerSpeed = 10f;
    public float playerCrouchSpeed = 5f;
    public float playerSlideSpeed = 20f;
    public float playerRotationSpeed = 1.0f;

    public float playerJumpForce = 9f;
    public float playerFastFall = -20f;
    public float playerGravity = -12f;
    public float initialFallVelocity = -3f;
    public float coyoteTime = 0.3f;

    public float acceleration = 7.0f;
    
    [Header("Blockblaster")]

    public Timer timer;
    public Transform BoxSnap;
    public Transform BroomSnap;
    public GameObject holdItem;
    public static GameObject player;

    private void OnEnable()
    {
        jumpAction.action.performed += OnJumpPressed;
        jumpAction.action.canceled += OnJumpPressed;
        slideAction.action.performed += OnSlidePressed;
        slideAction.action.canceled += OnSlidePressed;
    }

    private void OnDisable()
    {
        jumpAction.action.performed -= OnJumpPressed;
        jumpAction.action.canceled -= OnJumpPressed;
        slideAction.action.performed -= OnSlidePressed;
        slideAction.action.canceled -= OnSlidePressed;
    }


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _coyoteTimer = coyoteTime;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("dont destroy blockblaster");
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Statistics.instance.stats.BlockBlaster.BlockTimesAttempted++;
        Statistics.instance.SaveStats();

        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {

        //Debug.Log(_currentState);

        //Ground Check
        _isGrounded = _controller.isGrounded;

        //Allow player the ability to jump a little bit after falling off a ledge
        if(_coyoteTimer > 0)
        {
            _coyoteTimer -= Time.deltaTime; 
        }
        if(_isGrounded)
        {
            _coyoteTimer = coyoteTime;
        }

        ApplyGravity();

        if(_canMove)
        {
            _inputDirection = (transform.right * _moveInput.x + transform.forward * _moveInput.y).normalized;
        }
        else
        {
            //TODO: Add slight movement in a direction while sliding
        }

        float targetSpeed = playerSpeed;
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
        float speedOffset = 0.1f;

        //Player State Machine
        switch (_currentState)
        {
            case PlayerState.IDLE:

                targetSpeed = 0.0f;

                //Switch to RUN state
                if (_moveInput != Vector2.zero)
                    _currentState = PlayerState.RUN;
                //Switch to SLIDE state
                if (_slideInput == true)
                    _currentState = PlayerState.SLIDE;

                break;
            case PlayerState.RUN:

                targetSpeed = playerSpeed;

                //Switch to IDLE state
                if (_moveInput == Vector2.zero)
                    _currentState = PlayerState.IDLE;

                //Switch to SLIDE state
                if (_slideInput == true)
                {
                    //Set the initial slide speed when switching to SLIDE state from RUN state
                    _currSlideSpeed = playerSlideSpeed;
                    _currentState = PlayerState.SLIDE;
                }

                break;
            case PlayerState.SLIDE:

                if(_currSlideSpeed > playerCrouchSpeed)
                {
                    //Lock player movement if they are moving
                    _canMove = false;
                    if (_isGrounded)
                    {
                        //Decrease slide speed as slide is held down
                        if (_currSlideSpeed > 0.0f)
                        {
                            _currSlideSpeed -= Time.deltaTime * acceleration * 2;
                        }
                        targetSpeed = _currSlideSpeed;
                    }
                }
                else
                {
                    _canMove = true;
                    targetSpeed = playerCrouchSpeed;
                }

                //Allow player to fast fall with crouch
                if (!_isJumping && !_isGrounded)
                {
                    _verticalVelocity = playerFastFall;
                }

                //Shrink player capsule height
                _controller.height = _playerSlideHeight;



                //Switch to RUN state
                if (_slideInput == false)
                {
                    _canMove = true;
                    _controller.height = _playerHeight;

                    _currSlideSpeed = 0f;

                    _currentState = PlayerState.RUN;

                }
                break;
        }

        //Handle player acceleration 
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * acceleration);
        }
        else
        {   
            _speed = targetSpeed;       
        }

        Vector3 finalMove = _inputDirection * _speed;
        finalMove.y = _verticalVelocity;
        _controller.Move(finalMove * Time.deltaTime);
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
        {
            Debug.Log('Q');
            DropItem();
        }
    }

    void ApplyGravity()
    {
        //Keep gravity to an initial value while grounded
        if (_isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = initialFallVelocity;
        }

        //Exponentially increase gravity as you fall
        _verticalVelocity += 2 * playerGravity * Time.deltaTime;

    }

    //Retrieve move input
    void OnMove(InputValue movementValue)
    {
        _moveInput = movementValue.Get<Vector2>();
    }

    void OnSlidePressed(InputAction.CallbackContext slideValue)
    {
        if (slideValue.ReadValue<float>() == 0)
        {
            _slideInput = false;
        }
        else
        {
            _slideInput = true;
        }
    }

    //Retrieve jump input
    void OnJumpPressed(InputAction.CallbackContext jumpValue)
    {
        //Variable jump height
        if (jumpValue.ReadValue<float>() == 0)
        {
            //Cut jump velocity
            _isJumping = false;
            if (!_isGrounded && _verticalVelocity > 0)
            {
                _verticalVelocity = 0;
            }
        }
        else
        {
            _isJumping = true;
            //On Jump pressed
            if (_isGrounded || _coyoteTimer > 0)
            {   
                _verticalVelocity = playerJumpForce;   
            }
        }
        //Prevent double jump
        _coyoteTimer = 0;
    }

// collisions collisions collisions collisions collisions collisions collisions collisions collisions collisions collisions collisions collisions collisions collisions 

    void OnTriggerEnter(Collider other) // the player can run into a lot of things
    {

        if (other.gameObject.CompareTag("pickUp") && holdItem == null) // picking up the box    dont forget E also raycast pickup the box
        {
            holdItem = other.gameObject;

            other.gameObject.transform.position = BoxSnap.position;
            other.gameObject.transform.rotation = BoxSnap.rotation;
            other.gameObject.transform.SetParent(BoxSnap);

            other.GetComponent<BoxMovement>().StopRotating();
            

        }

        if (other.gameObject.CompareTag("broom") && holdItem == null) // picking up the broom
        {
            holdItem = other.gameObject;

            other.gameObject.transform.position = BroomSnap.position;
            other.gameObject.transform.rotation = BroomSnap.rotation;
            other.gameObject.transform.SetParent(BroomSnap);

        }


        // these are the invisible walls

        if (other.gameObject.CompareTag("exit")) // colliding with the wall debug sends you to the title screen
        {
            SceneManager.LoadScene(0);
        }

        if (other.CompareTag("directions")) // colliding with the invisible wall changes the ui
        {
            SceneManagerScript.instance.directions.SetActive(false);  
            SceneManagerScript.instance.StartCoroutine(SceneManagerScript.instance.TitleText());
        }

    }

    public bool HoldingBroom() // checks to see if the player is holding the broom or not
    {
        return holdItem != null && holdItem.CompareTag("broom");
    }

    void DropItem() // dropping the items
    {
        if (holdItem == null) return;

        if (holdItem.CompareTag("broom") || holdItem.CompareTag("pickUp"))
        {
            holdItem.transform.SetParent(null);
            holdItem.transform.position = transform.position + transform.forward * 2f;

            holdItem = null;
        }

        
    }

// player death and destory player death and destory player death and destory player death and destory player death and destory player death and destory player death and destory 


    void OnDestroy() // when the player is killed restart the scene
    {
        Invoke("Die", 2f);
    }


    void Die() // this should be the thing that restarts the scene and stops the timer 
    {
        BBGameManager.instance.DecreaseLives();
        Debug.Log("lives: " + BBGameManager.instance.GetLives());
        SceneManager.LoadScene(1);
        BBGameManager.instance.ResetScores();

        if (ObjectiveUI.instance.timer != null)
        {
            ObjectiveUI.instance.timer.StopTimer();
        }

        Destroy (gameObject); // destorys the player, player controller does the rest

        if (BBGameManager.instance.lives <= 0)
        {
            SceneManagerScript.instance.ToTitle();
            BBGameManager.instance.ResetGame();
        }
            
    }
}
