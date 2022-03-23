using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI startText;

    private string _textToSet = "Press any key to start";

    private void Start() => SetStartText();

    private void SetStartText()
    {
#if UNITY_ANDROID
        _textToSet = "Tap Anywhere to Start";
#endif

        startText.text = _textToSet;
    }
}