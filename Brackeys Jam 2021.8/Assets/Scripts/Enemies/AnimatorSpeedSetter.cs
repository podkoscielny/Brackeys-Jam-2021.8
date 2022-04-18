using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSpeedSetter : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;
    [SerializeField] ChaosStarsSystem chaosStarsSystem;

    private IEnemyMovement enemyMovement;

    private const float BASE_SPEED = 4f;

    private void Awake() => enemyMovement = GetComponent<IEnemyMovement>();

    private void OnEnable() => SetAnimatorSpeed();

    private void SetAnimatorSpeed()
    {
        float enemySpeed = enemyMovement == null ? BASE_SPEED : enemyMovement.MovementSpeed;
        float animatorSpeed = enemySpeed / BASE_SPEED;

        enemyAnimator.speed = animatorSpeed;
    }
}
