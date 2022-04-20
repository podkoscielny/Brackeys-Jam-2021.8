using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AoOkami.MultipleTagSystem;
using Tags = AoOkami.MultipleTagSystem.TagSystem.Tags;
using Random = UnityEngine.Random;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] ObjectPool objectPool;
    [SerializeField] ChaosStarsSystem chaosStarsSystem;

    private readonly Quaternion RIGHT_ROTATION = new Quaternion(0, 0, 0, 1);
    private readonly Quaternion LEFT_ROTATION = new Quaternion(0, 1, 0, 0);

    private const float SPAWN_MAX_Y = -1.71f;
    private const float SPAWN_MIN_Y = -2.3f;
    private const float SPAWN_X_RANGE = 16f;

    private void Awake() => objectPool.InitializePool();

    private void OnEnable()
    {
        PlayerHealth.OnGameOver += StopSpawnCoroutines;
        SceneController.OnSceneTransitionUnveiled += SpawnEnemies;
    }

    private void OnDisable()
    {
        PlayerHealth.OnGameOver -= StopSpawnCoroutines;
        SceneController.OnSceneTransitionUnveiled -= SpawnEnemies;
    }

    private void SpawnEnemies()
    {
        StartCoroutine(SpawnEnemyCoroutine(GetNonHostileVaraints, Tags.NonHostile));
        StartCoroutine(SpawnEnemyCoroutine(GetHostileVaraints, Tags.Hostile));
    }

    private IEnumerator SpawnEnemyCoroutine<T>(Func<EnemyType<T>> getEnemyVariantsCallback, Tags enemyTag)
    {
        while (true)
        {
            if (getEnemyVariantsCallback.Invoke().SpawnRate <= 0)
                yield return new WaitUntil(() => getEnemyVariantsCallback.Invoke().SpawnRate > 0);

            int spawnedEnemies = TagSystem.FindAllGameObjectsWithTag(enemyTag).Count;

            if (objectPool.IsTagInDictionary(enemyTag) && spawnedEnemies < getEnemyVariantsCallback.Invoke().EnemyLimit) GetCharacterFromPool(enemyTag);

            yield return new WaitForSeconds(getEnemyVariantsCallback.Invoke().SpawnRate);
        }
    }

    private EnemyType<NonHostileSO> GetNonHostileVaraints() => chaosStarsSystem.CurrentChaosStar.NonHostileEnemies;
    private EnemyType<HostileEnemy> GetHostileVaraints() => chaosStarsSystem.CurrentChaosStar.HostileEnemies;

    private void StopSpawnCoroutines() => StopAllCoroutines();

    private void GetCharacterFromPool(Tags tag)
    {
        bool isMovingRight = Random.Range(0, 2) == 1;

        float randomX = isMovingRight ? -SPAWN_X_RANGE : SPAWN_X_RANGE;
        float randomY = Random.Range(SPAWN_MIN_Y, SPAWN_MAX_Y);
        Vector3 charactersPosition = new Vector3(randomX, randomY, randomY);
        Quaternion charactersRotation = isMovingRight ? RIGHT_ROTATION : LEFT_ROTATION;

        objectPool.GetFromPool(tag, charactersPosition, charactersRotation);
    }
}
