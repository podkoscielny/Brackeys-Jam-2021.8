using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChanelPerlin;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cinemachineBasicMultiChanelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float duration)
    {
        _cinemachineBasicMultiChanelPerlin.m_AmplitudeGain = intensity;

        StartCoroutine(ResetShake(duration));
    }

    IEnumerator ResetShake(float time)
    {
        yield return new WaitForSeconds(time);

        _cinemachineBasicMultiChanelPerlin.m_AmplitudeGain = 0f;
    }
}
