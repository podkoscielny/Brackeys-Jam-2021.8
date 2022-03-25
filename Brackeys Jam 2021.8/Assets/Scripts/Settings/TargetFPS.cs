using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID
public class TargetFPS : MonoBehaviour
{
    private void Start() => SetTargetFPS();

    private void SetTargetFPS()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
}
#endif
