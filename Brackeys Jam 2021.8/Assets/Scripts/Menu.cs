using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [SerializeField] Animator sceneAnimator;
    [SerializeField] SceneController controller;
    [SerializeField] TextMeshProUGUI startText;

    private bool _canShowText = true;

    void Update()
    {
        if(controller.IsReady && !startText.gameObject.activeInHierarchy && _canShowText)
        {
            startText.gameObject.SetActive(true);
        }

        if(Input.anyKey && controller.IsReady)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        sceneAnimator.SetTrigger("Hide");
        startText.gameObject.SetActive(false);
        _canShowText = false;
    }
}
