using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSpeedSetter : MonoBehaviour
{
    [SerializeField] Animator enemyAnimator;
    [SerializeField] ChaosStarsSystem chaosStarsSystem;

    private const float BASE_SPEED = 4f;

    private void OnEnable() => SetAnimatorSpeed();

    private void SetAnimatorSpeed()
    {
        float enemySpeed = chaosStarsSystem.CurrentChaosStar.LastEnemyPicked.MovementSpeed;
        float animatorSpeed = enemySpeed / BASE_SPEED;

        enemyAnimator.speed = animatorSpeed;
    }
}
