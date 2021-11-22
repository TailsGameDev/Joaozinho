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
    private float timeToStartShooting = 0.0f;


    [SerializeField]
    private SpriteRenderer spriteRenderer = null;
    [SerializeField]
    private Color normalColor = Color.white;
    [SerializeField]
    private Color shootingColor = Color.white;

    private TransformWrapper transformWrapper;

    private bool isShooting;

    private void Awake()
    {
        transformWrapper = new TransformWrapper(transform);
    }

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
                }
                else
                {
                    isShooting = true;
                    timeToWait = timeToStartShooting;
                }
            }
            else
            {
                isShooting = false;
                // Wait 0.0f so when player gets in range, the wait is precise.
                timeToWait = 0.0f;
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
    }
}
