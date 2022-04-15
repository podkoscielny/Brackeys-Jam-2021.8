using System;
using UnityEngine;
using UnityEditor;
using Random = UnityEngine.Random;

[Serializable]
public struct EnemyType<T>
{
    [SerializeField] int enemyLimit;
    [SerializeField] float spawnRate;
    [SerializeField] EnemyProbability<T>[] enemyVariants;
    [HideInInspector] [SerializeField] float _probabilitySum;

    public int EnemyLimit => enemyLimit;
    public float SpawnRate => spawnRate;
    public EnemyProbability<T>[] EnemyVariants => enemyVariants;
    public float ProbabilitySum => _probabilitySum;

    public T GetRandomEnemy()
    {
        if (enemyVariants.Length == 0) return default(T);

        float randomProbability = Random.Range(0, _probabilitySum);
        float subtractFromSum = 0;

        foreach (var enemy in enemyVariants)
        {
            if (randomProbability - subtractFromSum <= enemy.Probability) return enemy.EnemyType;

            subtractFromSum += enemy.Probability;
        }

        return enemyVariants[enemyVariants.Length - 1].EnemyType;
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(EnemyType<>))]
public class EnemyTypeDrawer : PropertyDrawer
{
    private SerializedProperty _enemyVariants;
    private SerializedProperty _probabilitySum;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label, true);

        if (!EditorGUIUtility.editingTextField)
        {
            SortEnemyVariants(property);
            CalculateProbabilitySum(property);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property);
    }

    private void CalculateProbabilitySum(SerializedProperty property)
    {
        if (_enemyVariants.arraySize == 0 || _enemyVariants == null) return;

        if (_probabilitySum == null) _probabilitySum = property.FindPropertyRelative("_probabilitySum");

        float probability = 0;

        for (int i = 0; i < _enemyVariants.arraySize; i++)
        {
            probability += _enemyVariants.GetArrayElementAtIndex(i).FindPropertyRelative("probability").floatValue;
        }

        _probabilitySum.floatValue = probability;
    }

    private void SortEnemyVariants(SerializedProperty property)
    {
        if (_enemyVariants == null) _enemyVariants = property.FindPropertyRelative("enemyVariants");

        if (_enemyVariants.arraySize == 0) return;

        for (int i = 0; i < _enemyVariants.arraySize; i++)
        {
            if (i == 0) continue;

            int desiredIndex = i;
            SerializedProperty currentElement = _enemyVariants.GetArrayElementAtIndex(i).FindPropertyRelative("probability");

            for (int j = i - 1; j >= 0; j--)
            {
                SerializedProperty previousElement = _enemyVariants.GetArrayElementAtIndex(j).FindPropertyRelative("probability");

                if (currentElement.floatValue < previousElement.floatValue) desiredIndex = j;
            }

            if (desiredIndex != i) _enemyVariants.MoveArrayElement(i, desiredIndex);
        }
    }
}
#endif
