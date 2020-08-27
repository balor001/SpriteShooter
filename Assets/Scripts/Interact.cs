using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public PlayerInputController playerInputController;

    private GameObject raycastedObject;

    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;

    public static bool keyCard = false;

    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, forward, out hit, rayLength, layerMaskInteract.value))
        {
            // Check if the hit object is interactable
            if (hit.collider.CompareTag("Interactable"))
            {
                raycastedObject = hit.collider.gameObject;

                if (playerInputController.inputActions.Player.Interact.triggered)
                {
                    // Enable script on the hit object when player interacts with it
                    if (raycastedObject.name == "UnlockedDoor" || raycastedObject.name == "LockedDoor")
                    {
                        raycastedObject.GetComponent<SlideDoor>().enabled = true;
                    }

                    if (raycastedObject.name == "Platform")
                    {
                        raycastedObject.GetComponent<Elevator>().enabled = true;
                    }

                    // Crate contains a keycard
                    if (raycastedObject.name == "Crate")
                    {
                        keyCard = true;
                        Debug.Log("You found a key card!");
                    }

                    // Health pack heals 25HP
                    if (raycastedObject.name == "HealthPack")
                    {
                        PlayerStats.AddHealth(25);
                    }
                }
            }
        }
    }
}
