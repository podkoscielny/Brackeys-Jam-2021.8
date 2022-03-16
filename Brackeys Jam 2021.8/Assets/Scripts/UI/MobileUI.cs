using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUI : MonoBehaviour
{
    [SerializeField] GameObject mobileUI;

    void Start() => DisplayMobileUI();

    private void DisplayMobileUI()
    {
        bool shouldMobileUIBeDisplayed = false;

#if UNITY_ANDROID
        shouldMobileUIBeDisplayed = true;
#endif

        mobileUI.SetActive(shouldMobileUIBeDisplayed);
    }
}