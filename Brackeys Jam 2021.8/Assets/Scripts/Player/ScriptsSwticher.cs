using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsSwticher : MonoBehaviour
{
    [SerializeField] List<MonoBehaviour> scriptsToDisable;

    void OnEnable() => PlayerHealth.OnGameOver += DisableScripts;

    void OnDisable() => PlayerHealth.OnGameOver -= DisableScripts;

    private void DisableScripts()
    {
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = false;
        }
    }
}
