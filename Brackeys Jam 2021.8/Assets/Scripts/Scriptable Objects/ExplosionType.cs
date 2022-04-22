using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explosion", menuName = "ScriptableObjects/Explosion")]
public class ExplosionType : ScriptableObject
{
    [SerializeField] float range;
    [SerializeField] float cameraShakeIntensity;
    [SerializeField] float cameraShakeDuration;
    [SerializeField] Vector3 size;
    [SerializeField] RuntimeAnimatorController animatorController;
    [SerializeField] AudioClip soundEffect;

    public float Range => range;
    public float CameraShakeIntensity => cameraShakeIntensity;
    public float CameraShakeDuration => cameraShakeDuration;
    public Vector3 Size => size;
    public RuntimeAnimatorController AnimatorController => animatorController;
    public AudioClip SoundEffect => soundEffect;
}
