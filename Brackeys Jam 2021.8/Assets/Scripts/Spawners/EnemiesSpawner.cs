using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AoOkami.MultipleTagSystem;
using Tags = AoOkami.MultipleTagSystem.TagSystem.Tags;

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
        SceneController.OnGameStart += SpawnEnemies;
    }

    private void OnDisable()
    {
        PlayerHealth.OnGameOver -= StopSpawnCoroutines;
        SceneController.OnGameStart -= SpawnEnemies;
    }

    private void SpawnEnemies()
    {
        StartCoroutine(SpawnHostileCoroutine());
        StartCoroutine(SpawnNonHostileCoroutine());
    }

    private IEnumerator SpawnHostileCoroutine()
    {
        while (true)
        {
            EnemyType<HostileEnemy> enemyType = chaosStarsSystem.CurrentChaosStar.HostileEnemies;
            
            int spawnedEnemies = TagSystem.FindAllGameObjectsWithTag(Tags.Hostile).Count;

            if (objectPool.IsTagInDictionary(Tags.Hostile) && spawnedEnemies < enemyType.EnemyLimit) GetCharacterFromPool(Tags.Hostile);

            yield return new WaitForSeconds(enemyType.SpawnRate);
        }
    }

    private IEnumerator SpawnNonHostileCoroutine()
    {
        while (true)
        {
            EnemyType<NonHostileSO> enemyType = chaosStarsSystem.CurrentChaosStar.NonHostileEnemies;

            int spawnedEnemies = TagSystem.FindAllGameObjectsWithTag(Tags.Character).Count;

            if (objectPool.IsTagInDictionary(Tags.Character) && spawnedEnemies < enemyType.EnemyLimit) GetCharacterFromPool(Tags.Character);

            yield return new WaitForSeconds(enemyType.SpawnRate);
        }
    }

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
