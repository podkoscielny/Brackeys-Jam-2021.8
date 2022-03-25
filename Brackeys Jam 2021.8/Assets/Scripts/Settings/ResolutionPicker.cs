using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
public class ResolutionPicker : MonoBehaviour
{
    private const float MOBILE_RESOLUTION_FACTOR = 0.6f;

    private void Start() => SetResolution();

    private void SetResolution()
    {
        Display display = Display.main;

        int height = (int)(display.systemHeight * MOBILE_RESOLUTION_FACTOR);
        int width = (int)(display.systemWidth * MOBILE_RESOLUTION_FACTOR);

        Screen.SetResolution(width, height, true);
    }
}
#endif
