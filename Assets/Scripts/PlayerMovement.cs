using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform mainCamera;
    public PlayerInputController playerInputController;
    public CharacterController controller;

    public Transform groundCheck;
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    public float walkSpeed = 7.5f;
    public float runSpeed = 12.5f;
    public float jumpHeigth = 10f;
    public float crouchHeigth = 2f;
    public float gravity = -9.81f;

    public bool isGrounded;

    Vector3 velocity;
    Vector3 playerInput;

    float movementSpeed;
    float originalHeight;
    float newHeight;
    float lastHeight;

    bool isCrouching;
    public static bool isSprinting;

    void Start()
    {
        originalHeight = controller.height;
    }

    void Update()
    {
        newHeight = originalHeight;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Sprinting
        if (isSprinting && isGrounded && PlayerStats.stamina > 0)
        {
            movementSpeed = runSpeed;
        }
        else
        {
            movementSpeed = walkSpeed;
        }

        playerInputController.inputActions.Player.Sprint.performed += sprint => isSprinting = true;
        playerInputController.inputActions.Player.Sprint.canceled += sprint => isSprinting = false;

        //set y rotation = camera y rotation
        transform.rotation = Quaternion.Euler(new Vector3(0, mainCamera.eulerAngles.y, 0));

        playerInput = playerInputController.inputActions.Player.Move.ReadValue<Vector2>();
        controller.Move(transform.right * playerInput.x * movementSpeed * Time.deltaTime + transform.forward * playerInput.y * movementSpeed * Time.deltaTime + gravity * transform.up * Time.deltaTime);

        // Crouching
        if (isCrouching)
        {
            newHeight = 0.5f * originalHeight;
        }

        playerInputController.inputActions.Player.Crouch.performed += crouch => isCrouching = true;
        playerInputController.inputActions.Player.Crouch.canceled += crouch => isCrouching = false;

        lastHeight = controller.height;
        controller.height = Mathf.Lerp(controller.height, newHeight, 10.0f * Time.deltaTime);    // Change character height
        transform.position = transform.position + new Vector3(0, (controller.height - lastHeight) * 1f, 0);   // Update vertical position

        // Jumping
        if (playerInputController.inputActions.Player.Jump.triggered && isGrounded && PlayerStats.stamina >= 5)
        {
            velocity.y = Mathf.Sqrt(jumpHeigth * -2f * gravity);
            PlayerStats.JumpStaminaReduction();
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Debug.Log("Speed: " + movementSpeed);
    }

    /*
    public void PlayerCrouch()
    {
        if (crouch)
        {
            controller.height = crouchHeigth;
        }
        else
        {
            controller.height = originalHeight;
        }

    }

    public void PlayerJump()
    {
        velocity.y = Mathf.Sqrt(jumpHeigth * -2f * gravity);
    }
    */
}