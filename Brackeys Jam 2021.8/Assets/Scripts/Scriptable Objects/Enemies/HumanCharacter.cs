using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HumanCharacter", menuName = "ScriptableObjects/HumanCharacter")]
public class HumanCharacter : ScriptableObject
{
    public Sprite sprite;
    public RuntimeAnimatorController animatorController;
}
