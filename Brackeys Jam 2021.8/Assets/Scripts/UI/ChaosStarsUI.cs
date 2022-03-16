using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosStarsUI : MonoBehaviour
{
    [SerializeField] GameObject chaosStarPrefab;
    [SerializeField] ChaosStarsSystem chaosStarsSystem;

    private List<GameObject> _chaosStars = new List<GameObject>();

    private void OnEnable() => ChaosStarsSystem.OnChaosStarGained += EnableChaosStar;

    private void OnDisable() => ChaosStarsSystem.OnChaosStarGained -= EnableChaosStar;

    private void Start() => InitializeChaosStars();

    private void InitializeChaosStars()
    {
        for (int i = 0; i < chaosStarsSystem.MAX_CHAOS_STARS_AMOUNT; i++)
        {
            GameObject star = Instantiate(chaosStarPrefab);
            star.transform.SetParent(transform);
            _chaosStars.Add(star);
        }
    }

    private void EnableChaosStar() => _chaosStars[chaosStarsSystem.ChaosStarsAmount - 1].SetActive(true);
}