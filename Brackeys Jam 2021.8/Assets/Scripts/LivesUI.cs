using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] GameObject heartPrefab;

    private List<Image> _hearts;

    void OnEnable() => PlayerHealth.OnHealthChanged += UpdateHeartsAmount;

    private void OnDisable() => PlayerHealth.OnHealthChanged -= UpdateHeartsAmount;

    void Start()
    {
        _hearts = new List<Image>();
        InitializeLives();
        UpdateHeartsAmount();
    }

    private void InitializeLives()
    {
        for (int i = 0; i < playerHealth.MAX_HEALTH; i++)
        {
            GameObject life = Instantiate(heartPrefab);
            life.transform.SetParent(gameObject.transform);
            Image lifeImage = life.GetComponent<Image>();

            _hearts.Add(lifeImage);
        }
    }

    private void UpdateHeartsAmount()
    {
        float currentHealthValue = playerHealth.HealthValue;

        for (int i = 0; i < _hearts.Count; i++)
        {
            _hearts[i].fillAmount = i <= currentHealthValue - 1 ? 1f : 0f;
        }

        if (currentHealthValue % 1 != 0)
        {
            int notFullHeartIndex = Mathf.FloorToInt(currentHealthValue);
            _hearts[notFullHeartIndex].fillAmount = currentHealthValue - notFullHeartIndex;
        }
    }
}