using System;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(ChaosStar))]
class ChaosStarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ChaosStar chaosStar = (ChaosStar)target;

        if (!EditorGUIUtility.editingTextField) chaosStar.SortEnemyTypesByProbability();
    }
}
#endif
