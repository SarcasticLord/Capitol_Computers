// as a reminder i followed a tutorial for this i do have my own additions to the script

 using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour

{
    private Rigidbody rb;

    [SerializeField] private InputActionAsset playerControls;
    [SerializeField] private string actionMapName = "Game";
    [SerializeField] private string movement = "Move";
    [SerializeField] private string rotation = "rotation";
    [SerializeField] private string jump = "jump";
    [SerializeField] private string sprint = "sprint";

    private InputAction movementAction;
    private InputAction rotationAction;
    private InputAction jumpAction;
    private InputAction sprintAction;

   public Vector2 MovementInput { get; private set; }
   public Vector2 RotationInput { get; private set; }
   public bool JumpInput { get; private set; }
   public bool SprintInput { get; private set; }


    void Awake()
    {
        InputActionMap mapRefrence = playerControls.FindActionMap(actionMapName);
        
        movementAction = mapRefrence.FindAction(movement);
        rotationAction = mapRefrence.FindAction(rotation);
        jumpAction = mapRefrence.FindAction(jump);
        sprintAction = mapRefrence.FindAction(sprint);

        SubscribeActionValuesToInputEvents();

        if (instance == null)
        {
            instance = this;
            
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    private void SubscribeActionValuesToInputEvents()
    {
        movementAction.performed += inputInfo => MovementInput = inputInfo.ReadValue<Vector2>();
        movementAction.canceled += inputInfo => MovementInput = Vector2.zero;

        rotationAction.performed += inputInfo => RotationInput = inputInfo.ReadValue<Vector2>();
        rotationAction.canceled += inputInfo => RotationInput = Vector2.zero;

        jumpAction.performed += inputInfo => JumpInput = true;
        jumpAction.canceled += inputInfo => JumpInput = false;

        sprintAction.performed += inputInfo => SprintInput = true;
        sprintAction.canceled += inputInfo => SprintInput = false;
    }

    private void OnEnable()
    {
        playerControls.FindActionMap(actionMapName).Enable();
    }

    private void OnDisable()
    {
        playerControls.FindActionMap(actionMapName).Disable();
    }

        // just kidding this is the stuff that really matters
    public Timer timer;
    public Transform BoxSnap;
    public Transform BroomSnap;
    public GameObject holdItem;
    public static PlayerController instance;
    
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }

        
    }

    


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
