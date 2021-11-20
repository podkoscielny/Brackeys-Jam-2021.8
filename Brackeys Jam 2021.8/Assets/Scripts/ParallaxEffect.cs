using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    [SerializeField] float parallaxEffect;

    private float _startPosition;

    void Start() => _startPosition = transform.position.x;

    void LateUpdate() => CalculateParallax();

    private void CalculateParallax()
    {
        float distance = mainCamera.position.x * parallaxEffect;

        transform.position = new Vector3(_startPosition + distance, transform.position.y, transform.position.z);
    }
}
