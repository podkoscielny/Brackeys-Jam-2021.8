using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerHitter
{
    public float PlayerDamageAmount { get; }
    public float CameraShakeIntensity { get; }
    public float CameraShakeDuration { get; }
}
