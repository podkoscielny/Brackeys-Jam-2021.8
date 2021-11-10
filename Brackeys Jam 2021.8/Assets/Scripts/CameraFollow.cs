using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _player;

    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineFramingTransposer _cameraTransposer;

    void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _cameraTransposer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    void LateUpdate()
    {
        _virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_virtualCamera.m_Lens.OrthographicSize, CalculateLensSize(), Time.deltaTime * 2f);
        _cameraTransposer.m_TrackedObjectOffset = CalculateCameraOffset();
    }

    private float CalculateLensSize()
    {
        #if UNITY_ANDROID
        return _player.position.y * 0.23816f + 3.7635f;

        #else
        return _player.position.y * 0.2943f + 4.5057f;
        #endif
    }

    private Vector3 CalculateCameraOffset()
    {
        float offsetY;

        #if UNITY_ANDROID
        offsetY = Mathf.Lerp(_cameraTransposer.m_TrackedObjectOffset.y, _player.position.y * (-0.60526f) - 0.42288f, Time.deltaTime * 2f); // check if its needed

        #else
        offsetY = Mathf.Lerp(_cameraTransposer.m_TrackedObjectOffset.y, _player.position.y * (-0.6724f) + 0.3392f, Time.deltaTime * 2f); // check if its needed
        #endif

        return new Vector3(0, offsetY, 0);
    }
}
