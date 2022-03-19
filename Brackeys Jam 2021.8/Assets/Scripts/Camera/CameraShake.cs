using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;

    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChanelPerlin;

    private void Awake() => _cinemachineBasicMultiChanelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

    private void OnEnable() => Explosion.OnExplosionSpawaned += ShakeCamera;

    private void OnDisable() => Explosion.OnExplosionSpawaned -= ShakeCamera;

    public void ShakeCamera(float intensity = 0, float duration = 0)
    {
        StopAllCoroutines();

        _cinemachineBasicMultiChanelPerlin.m_AmplitudeGain = intensity;

        StartCoroutine(ResetShake(duration));
    }

    IEnumerator ResetShake(float time)
    {
        yield return new WaitForSeconds(time);

        _cinemachineBasicMultiChanelPerlin.m_AmplitudeGain = 0f;
    }
}
