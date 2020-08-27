using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MouseLook : MonoBehaviour
{
    Vector2 mouseInput;

    public Transform Target;

    public float mouseSensitivity = 100f;
    public float MaxLookUpAngle = 90f;
    public float MinLookUpAngle = -90f;

    public Transform playerBody;

    public PlayerInputController playerInputController;
    float rotX = 0, rotY = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        /* OLD INPUT SYSTEM
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
        */

        mouseInput = playerInputController.inputActions.Player.Look.ReadValue<Vector2>();
        rotX += mouseInput.x * mouseSensitivity * Time.deltaTime;
        rotY += mouseInput.y * mouseSensitivity * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, MinLookUpAngle, MaxLookUpAngle);
        transform.localRotation = Quaternion.Euler(-rotY, rotX, 0f);
    }

    void FixedUpdate()
    {

        transform.position = Target.position;
    }
}
