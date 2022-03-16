using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;

    void OnEnable()
    {
        PlayerHealth.OnGameOver += SetGameOverPanel;
    }

    void OnDisable()
    {
        PlayerHealth.OnGameOver -= SetGameOverPanel;
    }

    private void SetGameOverPanel() => gameOverPanel.SetActive(true);
}
