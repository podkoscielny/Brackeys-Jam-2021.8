using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tags = TagSystem.Tags;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] PickableCoords[] pickableSpawnPositions;
    [SerializeField] ObjectPool objectPool;
    [SerializeField] PoopSystem poopSystem;
    [SerializeField] Score score;
    [SerializeField] ChaosStarsSystem chaosStarsSystem;

    private readonly Quaternion RIGHT_ROTATION = new Quaternion(0, 0, 0, 1);
    private readonly Quaternion LEFT_ROTATION = new Quaternion(0, 1, 0, 0);

    private const float SPAWN_MAX_Y = -1.98f;
    private const float SPAWN_MIN_Y = -2.8f;
    private const float SPAWN_X_RANGE = 16f;

    private void Awake() => objectPool.InitializePool();

    private void OnEnable()
    {
        PlayerHealth.OnGameOver += StopSpawnCoroutine;
        SceneController.OnGameStart += SpawnEnemy;
    }

    private void OnDisable()
    {
        PlayerHealth.OnGameOver -= StopSpawnCoroutine;
        SceneController.OnGameStart -= SpawnEnemy;
    }

    private void SpawnEnemy() => StartCoroutine(SpawnEnemyCoroutine());

    private IEnumerator SpawnEnemyCoroutine()
    {
        while (true)
        {
            ChaosStar currentChaosStar = chaosStarsSystem.CurrentChaosStar;
            
            int spawnedEnemies = TagSystem.FindAllGameObjectsWithTag(Tags.Enemy).Count;
            Tags randomEnemyTag = currentChaosStar.GetRandomEnemy();

            if (objectPool.IsTagInDictionary(randomEnemyTag) && spawnedEnemies < currentChaosStar.EnemiesLimit) GetCharacterFromPool(randomEnemyTag);

            yield return new WaitForSeconds(currentChaosStar.EnemySpawnRate);
        }
    }

    private void StopSpawnCoroutine() => StopCoroutine(SpawnEnemyCoroutine());

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
