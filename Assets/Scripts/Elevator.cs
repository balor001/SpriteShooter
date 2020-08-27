using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed = 2f;
    bool defaultPosition = true;

    public Transform platform;
    public Transform bottomPos;
    public Transform topPos;

    Vector3 currentPosition;
    Vector3 bottomPosition;
    Vector3 topPosition;

    void Start()
    {
        bottomPosition = bottomPos.localPosition;
        topPosition = topPos.localPosition;
    }

    void Update()
    {
        currentPosition = platform.localPosition;   // Track current position

        // From bottom to top
        if (defaultPosition == true)
        {
            platform.localPosition = new Vector3(platform.localPosition.x, Mathf.Lerp(platform.localPosition.y, topPosition.y, Time.deltaTime * speed), platform.localPosition.z);

            if (currentPosition == topPosition)
            {
                defaultPosition = false;
                this.enabled = false;   // Disable the script
            }
        }
        // From top to bottom
        else if (defaultPosition == false)
        {
            platform.localPosition = new Vector3(platform.localPosition.x, Mathf.Lerp(platform.localPosition.y, bottomPosition.y, Time.deltaTime * speed), platform.localPosition.z);

            if (currentPosition == bottomPosition)
            {
                defaultPosition = true;
                this.enabled = false;
            }
        }
    }
}
