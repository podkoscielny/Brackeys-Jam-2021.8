using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFPS : MonoBehaviour
{
    private void Start() => SetTargetFPS();

    private void SetTargetFPS()
    {
        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = 1;
    }
}
