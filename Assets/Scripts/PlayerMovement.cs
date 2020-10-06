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
    public float jumpHeigth = 27f;
    public float crouchHeigth = 2f;
    public float gravity = -20f;

    public bool isGrounded;

    Vector3 velocity;
    Vector3 playerInput;

    float movementSpeed;
    float originalHeight;
    float newHeight;
    float lastHeight;

    bool isCrouching;
    bool isSprinting;

    void Start()
    {
        originalHeight = controller.height;
    }

    void Update()
    {
        GroundCheck();
        Move();
        Crouch();
        Jump();

        //Debug.Log("Speed: " + movementSpeed);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void Move()
    {
        // Sprinting
        if (isSprinting && isGrounded)
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
    }

    void Crouch()
    {
        // Crouching
        newHeight = originalHeight;

        if (isCrouching)
        {
            newHeight = 0.5f * originalHeight;
        }

        playerInputController.inputActions.Player.Crouch.performed += crouch => isCrouching = true;
        playerInputController.inputActions.Player.Crouch.canceled += crouch => isCrouching = false;

        Vector3 up = transform.TransformDirection(Vector3.up);

        // Force crouch if there's no room to stand up
        if (Physics.Raycast(mainCamera.position, up, 0.1f))
        {
            newHeight = 0.5f * originalHeight;
        }

        lastHeight = controller.height;
        controller.height = Mathf.Lerp(controller.height, newHeight, 25.0f * Time.deltaTime);    // Change character height
        transform.position = transform.position + new Vector3(0, (controller.height - lastHeight) * 1f, 0);   // Update vertical position
    }

    void Jump()
    {
        // Jumping
        if (playerInputController.inputActions.Player.Jump.triggered && isGrounded)
        {
            velocity.y = jumpHeigth;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}