using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public PlayerInputController playerInputController;
    public Text interactTip;

    private GameObject raycastedObject;

    [SerializeField] private int rayLength = 10;
    [SerializeField] private LayerMask layerMaskInteract;

    public static bool keyCard = false;

    void Update()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);

        interactTip.enabled = false;

        if (Physics.Raycast(transform.position, forward, out hit, rayLength, layerMaskInteract.value))
        {
            // Check if the hit object is interactable
            if (hit.collider.CompareTag("Interactable"))
            {
                interactTip.enabled = true;

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

                    // Keycard
                    if (raycastedObject.name == "KeyCard")
                    {
                        keyCard = true;
                        raycastedObject.SetActive(false);
                        Debug.Log("You found a keycard!");
                    }

                    // Health pack
                    if (raycastedObject.name == "HealthPack")
                    {
                        PlayerStats.AddHealth(25);
                    }

                    // Extra shield
                    if (raycastedObject.name == "ArmorPack")
                    {
                        PlayerStats.AddShield(25);
                    }
                }
            }
        }
    }
}
