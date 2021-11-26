using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Player player = null;

    [SerializeField] private Animator animator = null;

    [SerializeField] private SpriteRenderer spriteRenderer = null;

    private void Update()
    {
        animator.SetBool("IsOnGround", player.IsOnGround);

        bool isMoving;
        const float SMALL_X_VELOCITY = 0.2f;
        if (player.Velocity.x > SMALL_X_VELOCITY)
        {
            spriteRenderer.flipX = true;
            isMoving = true;
        }
        else if (player.Velocity.x < -SMALL_X_VELOCITY)
        {
            spriteRenderer.flipX = false;
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        animator.SetBool("IsMoving", isMoving);
    }
}
