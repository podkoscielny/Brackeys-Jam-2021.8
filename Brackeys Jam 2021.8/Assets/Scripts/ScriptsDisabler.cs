using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsDisabler : MonoBehaviour
{
    [SerializeField] List<MonoBehaviour> scriptsToDisable;

    void OnEnable() => GameManager.OnGameOver += DisableScripts;

    void OnDisable() => GameManager.OnGameOver -= DisableScripts;

    void DisableScripts()
    {
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = false;
        }
    }
}
