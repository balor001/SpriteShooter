using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideDoor : MonoBehaviour
{
    public enum OpenDirection { x, y, z }
    public OpenDirection direction = OpenDirection.y;

    public float openDistance = 3f;
    public float openSpeed = 2f;
    public float closeDelay = 5f;

    float timer = 0f;

    bool doorLocked;

    public Transform doorBody;

    Vector3 defaultPosition;

    void Start()
    {
        if (doorBody)
        {
            defaultPosition = doorBody.localPosition;
        }

        if (doorBody.name == "UnlockedDoor")
        {
            doorLocked = false;
        }
        else if (doorBody.name == "LockedDoor")
        {
            doorLocked = true;
        }
    }

    void Update()
    {
        if (!doorBody)
        {
            return;
        }

        // Door needs to be unlocked
        if (doorLocked == false || Interact.keyCard == true)
        {
            DoorOpen();
        }
        else
        {
            Debug.Log("The door is locked!");
            this.enabled = false;   // Disable the script
        }
        
        // Close the door after certain time
        if (timer > closeDelay)
        {
            DoorClose();
        }
    }

    public void DoorOpen()
    {
        // Open the door to pre-determined direction
        if (direction == OpenDirection.x)
        {
            doorBody.localPosition = new Vector3(Mathf.Lerp(doorBody.localPosition.x, defaultPosition.x + openDistance, Time.deltaTime * openSpeed), doorBody.localPosition.y, doorBody.localPosition.z);
            timer += Time.deltaTime;
        }
        else if (direction == OpenDirection.y)
        {
            doorBody.localPosition = new Vector3(doorBody.localPosition.x, Mathf.Lerp(doorBody.localPosition.y, defaultPosition.y + openDistance, Time.deltaTime * openSpeed), doorBody.localPosition.z);
            timer += Time.deltaTime;
        }
        else if (direction == OpenDirection.z)
        {
            doorBody.localPosition = new Vector3(doorBody.localPosition.x, doorBody.localPosition.y, Mathf.Lerp(doorBody.localPosition.z, defaultPosition.z + openDistance, Time.deltaTime * openSpeed));
            timer += Time.deltaTime;
        }
    }

    public void DoorClose()
    {
        // Move the door back to its original position
        if (direction == OpenDirection.x)
        {
            doorBody.localPosition = new Vector3(Mathf.Lerp(defaultPosition.x, doorBody.localPosition.x + openDistance, Time.deltaTime * openSpeed), doorBody.localPosition.y, doorBody.localPosition.z);
        }
        else if (direction == OpenDirection.y)
        {
            doorBody.localPosition = new Vector3(doorBody.localPosition.x, Mathf.Lerp(defaultPosition.y, doorBody.localPosition.y + openDistance, Time.deltaTime * openSpeed), doorBody.localPosition.z);
        }
        else if (direction == OpenDirection.z)
        {
            doorBody.localPosition = new Vector3(doorBody.localPosition.x, doorBody.localPosition.y, Mathf.Lerp(defaultPosition.z, doorBody.localPosition.z + openDistance, Time.deltaTime * openSpeed));
        }

        timer = 0;

        this.enabled = false;   // Disable the script
    }
}
