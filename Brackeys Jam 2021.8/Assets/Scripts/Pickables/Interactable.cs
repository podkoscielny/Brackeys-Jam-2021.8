using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected AudioClip soundEffect;

    public AudioClip SoundEffect => soundEffect;

    public abstract void PickUp();
}
