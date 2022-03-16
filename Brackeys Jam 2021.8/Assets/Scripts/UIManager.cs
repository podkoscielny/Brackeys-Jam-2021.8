using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject mobileUI;

    void OnEnable()
    {
        PlayerHealth.OnGameOver += SetGameOverPanel;
    }

    void OnDisable()
    {
        PlayerHealth.OnGameOver -= SetGameOverPanel;
    }

    void Start() => DisplayMobileUI();

    private void DisplayMobileUI()
    {
        bool shouldMobileUIBeDisplayed = false;

#if UNITY_ANDROID
        shouldMobileUIBeDisplayed = true;
#endif

        mobileUI.SetActive(shouldMobileUIBeDisplayed);
    }

    private void SetGameOverPanel() => gameOverPanel.SetActive(true);
}
