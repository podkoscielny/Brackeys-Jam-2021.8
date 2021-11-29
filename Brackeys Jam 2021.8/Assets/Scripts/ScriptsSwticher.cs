using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsSwticher : MonoBehaviour
{
    [SerializeField] List<MonoBehaviour> scriptsToDisable;

    void OnEnable()
    {
        PlayerHealth.OnGameOver += DisableScripts;
        SceneController.OnGameStart += EnableScripts;
    }

    void OnDisable()
    {
        PlayerHealth.OnGameOver -= DisableScripts;
        SceneController.OnGameStart -= EnableScripts;
    }

    private void EnableScripts()
    {
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = true;
        }
    }

    private void DisableScripts()
    {
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = false;
        }
    }
}
