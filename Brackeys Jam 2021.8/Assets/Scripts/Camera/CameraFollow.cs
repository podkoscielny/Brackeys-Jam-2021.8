using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    private float _cameraOffsetVelocityRef = 0f;
    private float _cameraLensVelocityRef = 0f;
    private CinemachineFramingTransposer _cameraTransposer;

    private const float SMOOTH_TIME = 0.3f;

    void Awake() => _cameraTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

    void LateUpdate()
    {
        virtualCamera.m_Lens.OrthographicSize = CalculateLensSize();
        _cameraTransposer.m_TrackedObjectOffset = CalculateCameraOffset();
    }

    private float CalculateLensSize()
    {
#if UNITY_ANDROID
        return Mathf.SmoothDamp(virtualCamera.m_Lens.OrthographicSize, CalculateLinearFunctionValue(0.23f, 3.25f), ref _cameraLensVelocityRef, SMOOTH_TIME);
#else
        return Mathf.SmoothDamp(virtualCamera.m_Lens.OrthographicSize, player.position.y * 0.2943f + 4.5057f, ref _cameraLensVelocityRef, SMOOTH_TIME);
#endif
    }

    private Vector3 CalculateCameraOffset()
    {
        float offsetY;

#if UNITY_ANDROID
        offsetY = Mathf.SmoothDamp(_cameraTransposer.m_TrackedObjectOffset.y, CalculateLinearFunctionValue(-0.62f, -0.59f), ref _cameraOffsetVelocityRef, SMOOTH_TIME);
#else
        offsetY = Mathf.SmoothDamp(_cameraTransposer.m_TrackedObjectOffset.y, player.position.y * (-0.6724f) + 0.3392f, ref _cameraOffsetVelocityRef, SMOOTH_TIME);
#endif

        return new Vector3(0, offsetY, 0);
    }

    private float CalculateLinearFunctionValue(float a, float b)
    {
        float x = player.position.y > 3 ? 3 : player.position.y;

        return x * a + b;
    }
}
