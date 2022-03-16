using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;

    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChanelPerlin;

    void Awake() => _cinemachineBasicMultiChanelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

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
