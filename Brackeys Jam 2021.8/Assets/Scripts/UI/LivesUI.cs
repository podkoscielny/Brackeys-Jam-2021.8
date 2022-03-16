using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] GameObject heartPrefab;

    private List<Image> _hearts = new List<Image>();
    private const float SPACING_FACTOR = 1.5f;

    private void OnEnable() => PlayerHealth.OnHealthChanged += UpdateHeartsAmount;

    private void OnDisable() => PlayerHealth.OnHealthChanged -= UpdateHeartsAmount;

    private void Start()
    {
        InitializeLives();
        UpdateHeartsAmount();
    }

    private void InitializeLives()
    {
        float sideSize = Screen.width / 19;
        Vector2 lifeSize = new Vector2(sideSize, sideSize);
        Vector3 wrapperPosition = transform.position;

        for (int i = 0; i < playerHealth.MAX_HEALTH; i++)
        {
            GameObject life = Instantiate(heartPrefab);

            Vector3 lifePosition = wrapperPosition;
            lifePosition.x += (i * lifeSize.x * SPACING_FACTOR);

            life.transform.SetParent(transform);
            life.transform.position = lifePosition;
            life.GetComponent<RectTransform>().sizeDelta = lifeSize;

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