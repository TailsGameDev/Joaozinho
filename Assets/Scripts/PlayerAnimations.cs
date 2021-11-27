using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Player player = null;

    [SerializeField] private Animator animator = null;

    [SerializeField] private SpriteRenderer spriteRenderer = null;

    [SerializeField] private float timePursuingAimAfterShooting = 0.0f;

    [SerializeField] private Damageable playerDamageable = null;

    public static readonly float SMALL_X_VELOCITY = 0.2f;

    private float timeToFlipXAccordinglyToVelocity;

    private bool lastShootDirectionIsLeft;

    private void Awake()
    {
        playerDamageable.RegisterOnDamageTaken(() => 
        { 
            animator.SetTrigger("Damaged"); 
        });
    }

    private void Update()
    {
        animator.SetBool("IsOnGround", player.IsOnGround);

        bool isMoving;
        bool flipX = spriteRenderer.flipX;
        if (player.Velocity.x > SMALL_X_VELOCITY)
        {
            // spriteRenderer.flipX = true;
            flipX = true;
            isMoving = true;
        }
        else if (player.Velocity.x < -SMALL_X_VELOCITY)
        {
            // spriteRenderer.flipX = false;
            flipX = false;
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        animator.SetBool("IsMoving", isMoving);

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Shoot");
            timeToFlipXAccordinglyToVelocity = Time.time + timePursuingAimAfterShooting;

            lastShootDirectionIsLeft = Input.mousePosition.x > Screen.width / 2;
        }

        if (Time.time > timeToFlipXAccordinglyToVelocity)
        {
            spriteRenderer.flipX = flipX;
        }
        else
        {
            spriteRenderer.flipX = lastShootDirectionIsLeft;
        }
    }
}
