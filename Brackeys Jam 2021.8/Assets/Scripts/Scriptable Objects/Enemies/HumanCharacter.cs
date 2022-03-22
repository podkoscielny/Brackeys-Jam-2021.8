using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HumanCharacter", menuName = "ScriptableObjects/HumanCharacter")]
public class HumanCharacter : ScriptableObject
{
    [SerializeField] Sprite sprite;
    [SerializeField] RuntimeAnimatorController animatorController;

    public Sprite Sprite => sprite;
    public RuntimeAnimatorController AnimatorController => animatorController;
}
