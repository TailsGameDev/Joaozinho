using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenShots = 0.0f;
    private float timeToNextShot;

    [SerializeField]
    private Bullet bulletPrototype = null;

    [SerializeField]
    private Transform bulletSpawnPoint = null;


    [SerializeField]
    private SpriteRenderer spriteRenderer = null;
    /*
    [SerializeField]
    private Color normalColor = Color.white;
    [SerializeField]
    private Color shootingColor = Color.white;
    */

    [SerializeField]
    private Sprite idleSprite = null;
    [SerializeField]
    private Sprite aimingSprite = null;
    [SerializeField]
    private Sprite shotSprite = null;

    private TransformWrapper transformWrapper;

    private bool isShooting;

    private void Awake()
    {
        transformWrapper = new TransformWrapper(transform);
    }

    /*
    private void Update()
    {
        if (Time.time > timeToNextShot)
        {
            float timeToWait;

            if (spriteRenderer.isVisible)
            {
                if (isShooting)
                {
                    Bullet bullet = Instantiate(bulletPrototype, bulletSpawnPoint.position, Quaternion.identity);
                    Vector3 direction = (Player.Instance.TransformWrapper.Position - transformWrapper.Position).normalized;
                    bullet.TransformWrapper.Right = direction;
                    bullet.Rb2d.velocity = direction * bullet.Speed;

                    timeToWait = timeBetweenShots;

                    spriteRenderer.sprite = shotSprite;
                }
                else
                {
                    isShooting = true;
                    timeToWait = timeToStartShooting;

                    spriteRenderer.sprite = idleSprite;
                }
            }
            else
            {
                isShooting = false;
                // Wait 0.0f so when player gets in range, the wait is precise.
                timeToWait = 0.0f;

                spriteRenderer.sprite = idleSprite;
            }

            timeToNextShot = Time.time + timeToWait;

            
            if (isShooting)
            {
                spriteRenderer.color = shootingColor;
            }
            else
            {
                spriteRenderer.color = normalColor;
            }
            
}
        else
        {
            spriteRenderer.sprite = aimingSprite;
        }
    }
    */

    private state currentState;

    private void Update()
    {
        if (Player.IsDead)
        {
            enabled = false;
        }

        switch (currentState)
        {
            case state.HIDDEN:
                if (spriteRenderer.isVisible)
                {
                    timeToStartAiming = Time.time + timeIntervalToStartAiming;
                    currentState = state.IDLE;
                }
                break;
            case state.IDLE:
                spriteRenderer.sprite = idleSprite;
                
                if (Time.time > timeToStartAiming)
                {
                    currentState = state.AIMING;
                    timeToNextShot = Time.time + timeBetweenShots;
                }
                break;
            case state.AIMING:
                spriteRenderer.sprite = aimingSprite;

                if (Time.time > timeToNextShot)
                {
                    spriteRenderer.flipX = (Player.Instance.TransformWrapper.Position.x > transformWrapper.Position.x);

                    // Shoot
                    Bullet bullet = Instantiate(bulletPrototype, bulletSpawnPoint.position, Quaternion.identity);
                    Vector3 direction = (Player.Instance.TransformWrapper.Position - transformWrapper.Position).normalized;
                    bullet.TransformWrapper.Right = direction;
                    bullet.Rb2d.velocity = direction * bullet.Speed;

                    // Manage state
                    currentState = state.COOLDOWN;
                    timeToStartAiming = Time.time + cooldownAnimationTime;
                }

                break;
            case state.COOLDOWN:
                spriteRenderer.sprite = shotSprite;

                if (Time.time > timeToStartAiming)
                {
                    if (spriteRenderer.isVisible)
                    {
                        currentState = state.AIMING;
                        timeToNextShot = Time.time + timeBetweenShots;
                    }
                    else
                    {
                        currentState = state.HIDDEN;
                    }
                }
                break;
        }
    }

    [SerializeField]
    private float timeIntervalToStartAiming = 0.0f;
    private float timeToStartAiming;

    [SerializeField]
    private float cooldownAnimationTime = 0.0f;

    private enum state
    {
        HIDDEN = 0,
        IDLE = 1,
        AIMING = 2,
        COOLDOWN = 3,
    }
}
