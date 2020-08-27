using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private Text fpsDisplay;
    [SerializeField] private float fpsRefreshRate = 1f;

    private float timer;

    void Update()
    {
        if (Time.unscaledTime > timer)
        {
            int frameRate = (int)(1f / Time.unscaledDeltaTime);
            fpsDisplay.text = frameRate + " FPS";
            timer = Time.unscaledTime + fpsRefreshRate;
        }
    }
}
