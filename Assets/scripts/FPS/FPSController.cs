// i followed a tutorial on youtube for this 
// this is for the player movement stuff 
// this script was written by me but does not include any other game related things


using UnityEngine;

public class FPSController : MonoBehaviour
{

    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintMulti = 2.0f;

    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private float gravMulti = 1.0f;

    [SerializeField] private float mouseSense = 0.1f;
    [SerializeField] private float updownLook = 80f;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerController playerInputHandler;

    private Vector3 currentMovement;
    private float verticalRotation;
    private float currentSpeed => walkSpeed * (playerInputHandler.SprintInput ? sprintMulti : 1);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Statistics.instance.stats.BlockBlaster.BlockTimesAttempted++;
        Statistics.instance.SaveStats();

        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotating();
    }

    private Vector3 CalculateWorldDirection()
    {
        Vector3 inputDirection = new Vector3(playerInputHandler.MovementInput.x, 0f, playerInputHandler.MovementInput.y);
        Vector3 worldDirection = transform.TransformDirection(inputDirection);
        return worldDirection.normalized;
    }

    private void Jumping()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = 0.5f;

            if (playerInputHandler.JumpInput)
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y += Physics.gravity.y * gravMulti * Time.deltaTime;
        }
    }

    private void Movement()
    {
        Vector3 worldDirection = CalculateWorldDirection();
        currentMovement.x = worldDirection.x * currentSpeed;
        currentMovement.z = worldDirection.z * currentSpeed;

        Jumping();

        characterController.Move(currentMovement * Time.deltaTime);
    }

    private void HorizontalRotation(float rotationAmount)
    {
        transform.Rotate(0, rotationAmount, 0);
    }

    private void VerticalRotation(float rotationAmount)
    {
        verticalRotation = Mathf.Clamp(verticalRotation - rotationAmount, -updownLook, updownLook);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    private void Rotating()
    {
        float mouseXrotation = playerInputHandler.RotationInput.x * mouseSense;
        float mouseyrotation = playerInputHandler.RotationInput.y * mouseSense;

        VerticalRotation(mouseyrotation);
        HorizontalRotation(mouseXrotation);
    }

}
