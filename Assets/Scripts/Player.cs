using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d = null;

    [SerializeField]
    private float speed = 0.0f;

    [SerializeField]
    private float maxXSpeed = 0.0f;

    [SerializeField]
    private float jumpForce = 0.0f;

    [SerializeField]
    private Bullet bulletPrototype = null;
    [SerializeField]
    private Transform bulletSpawnPoint = null;

    [SerializeField]
    private Damageable damageable;

    private static Player instance;

    private TransformWrapper transformWrapper;

    public static Player Instance { get => instance; }
    public TransformWrapper TransformWrapper { get => transformWrapper; }
    public Damageable Damageable { get => damageable; }

    private void Awake()
    {
        instance = this;

        transformWrapper = new TransformWrapper(transform);
    }

    private void Update()
    {
        bool jump = Input.GetButtonDown("Jump");
        if (jump)
        {
            rb2d.AddForce(Vector3.up * jumpForce);
        }

        bool shoot = Input.GetMouseButtonDown(0);
        if (shoot)
        {
            Vector3 aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aim.z = 0.0f;
            Bullet bullet = Instantiate(bulletPrototype, bulletSpawnPoint.position, Quaternion.identity);
            Vector2 direction = (aim - transformWrapper.Position).normalized;
            bullet.TransformWrapper.Right = direction;
            bullet.Rb2d.velocity = (direction * bullet.Speed) + rb2d.velocity;
        }
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (
            // Don't push player further from maxXSpeed
            !(horizontalInput > 0.0f && rb2d.velocity.x > maxXSpeed)
            && !(horizontalInput < 0.0f && rb2d.velocity.x < -maxXSpeed)
            )
        {
            rb2d.AddForce(Vector3.right * horizontalInput * speed);
        }
    }
}
