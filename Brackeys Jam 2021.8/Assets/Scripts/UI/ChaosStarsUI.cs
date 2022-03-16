using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosStarsUI : MonoBehaviour
{
    [SerializeField] GameObject chaosStarPrefab;
    [SerializeField] ChaosStarsSystem chaosStarsSystem;

    private List<GameObject> _chaosStars = new List<GameObject>();
    private const float SPACING_FACTOR = 1.2f;

    private void OnEnable() => ChaosStarsSystem.OnChaosStarGained += EnableChaosStar;

    private void OnDisable() => ChaosStarsSystem.OnChaosStarGained -= EnableChaosStar;

    private void Start() => InitializeChaosStars();

    private void InitializeChaosStars()
    {
        float sideSize = Screen.width / 19;
        Vector2 chaosStarSize = new Vector2(sideSize, sideSize);
        Vector3 wrapperPosition = transform.position;

        for (int i = 0; i < chaosStarsSystem.MAX_CHAOS_STARS_AMOUNT; i++)
        {
            GameObject star = Instantiate(chaosStarPrefab);

            Vector3 chaosStarPosition = wrapperPosition;
            chaosStarPosition.x -= (i * chaosStarSize.x * SPACING_FACTOR);

            star.transform.SetParent(transform);
            star.transform.position = chaosStarPosition;
            star.GetComponent<RectTransform>().sizeDelta = chaosStarSize;

            _chaosStars.Add(star);
        }
    }

    private void EnableChaosStar() => _chaosStars[chaosStarsSystem.ChaosStarsAmount - 1].SetActive(true);
}