using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d = null;

    [SerializeField]
    private float speed = 0.0f;

    [SerializeField]
    private float lifeTime = 0.0f;

    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    private TransformWrapper transformWrapper = null;

    public static readonly string TAG = "Bullet";

    public TransformWrapper TransformWrapper { get => transformWrapper; }
    public Rigidbody2D Rb2d { get => rb2d; }
    public float Speed { get => speed; }

    private void Awake()
    {
        transformWrapper = new TransformWrapper(transform);
    }

    private void Update()
    {
        if (!spriteRenderer.isVisible)
        {
            Destroy(gameObject);
        }
    }
}
