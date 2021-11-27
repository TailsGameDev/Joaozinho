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

    [SerializeField] private float inverseDragX = 0.0f;

    [SerializeField]
    private float jumpForce = 0.0f;

    [SerializeField]
    private Bullet bulletPrototype = null;
    [SerializeField]
    private Transform bulletSpawnPoint = null;

    [SerializeField]
    private Damageable damageable;

    [SerializeField] private Collider2D isOnGroundTrigger = null;
    [SerializeField] private Transform bulletSpawnPointRotator = null;

    [SerializeField] private SpriteRenderer spriteRenderer = null;

    // [SerializeField] private Transform isOnGroundRaycastOrigin = null;
    // [SerializeField] private Collider2D isOnGroundTrigger = null;

    private static Player instance;

    public static readonly string TAG = "Player";

    // private bool isOnGround;
    private int groundsInTouch;

    private TransformWrapper transformWrapper;

    public static Player Instance { get => instance; }
    public TransformWrapper TransformWrapper { get => transformWrapper; }
    public Damageable Damageable { get => damageable; }
    public bool IsOnGround { get => (groundsInTouch > 0); }
    public Vector3 Velocity { get => rb2d.velocity; }
    public static bool IsDead { get => instance == null; }

    private void Awake()
    {
        instance = this;

        transformWrapper = new TransformWrapper(transform);
    }

    private void Update()
    {
        bool jump = Input.GetButtonDown("Jump") && IsOnGround;
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

        /*
        isOnGround = Physics.Raycast(origin: isOnGroundRaycastOrigin.position, direction: Vector3.down, maxDistance: 30.1f);
        Debug.LogError(isOnGround);
        */
        /*
        Collider2D[] results = new Collider2D[15];
        int overlappingCollidersAmount = isOnGroundTrigger.OverlapCollider(new ContactFilter2D().NoFilter(), results);
        isOnGround = (overlappingCollidersAmount > 1);
        */
        if (spriteRenderer.flipX)
        {
            bulletSpawnPointRotator.localEulerAngles = (new Vector3(0.0f, 180.0f, 0.0f));
        }
        else
        {
            bulletSpawnPointRotator.localEulerAngles = (new Vector3(0.0f, 0.0f, 0.0f));
        }
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        rb2d.velocity = new Vector2(rb2d.velocity.x * inverseDragX, rb2d.velocity.y);
        if (
            // Don't push player further from maxXSpeed
            !(horizontalInput > 0.0f && rb2d.velocity.x > maxXSpeed)
            && !(horizontalInput < 0.0f && rb2d.velocity.x < -maxXSpeed)
            )
        {
            rb2d.AddForce(Vector3.right * horizontalInput * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Enemy")
        {
            groundsInTouch++;
            if (collision.transform.position.y < isOnGroundTrigger.transform.position.y)
            {
                // Make Y speed 0 so the impulse in the floor does not slow the player down
                rb2d.velocity = new Vector2(rb2d.velocity.x, 0.0f);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Enemy")
        {
            groundsInTouch--;
        }
    }

    public void SetTheActive(bool theActive)
    {
        gameObject.SetActive(theActive);
    }
}
