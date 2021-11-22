using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakenDamageVFX : MonoBehaviour
{
    [SerializeField]
    private float lifeTime = 0.0f;

    [SerializeField]
    private float speed = 0.0f;

    [SerializeField]
    private Rigidbody2D rb2d = null;
    [SerializeField]
    private Text text = null;
    [SerializeField]
    private Outline outline = null;
    /*
    [SerializeField]
    private Image background = null;
    */

    [SerializeField]
    private float minXDirection = 0.0f;
    [SerializeField]
    private float maxXDirection = 0.0f;

    private float initialTime;
    private float timeToDestruction;

    public Text Text { get => text; }

    private void Awake()
    {
        initialTime = Time.time;

        float x = Random.Range(minXDirection, maxXDirection);

        Vector2 direction = new Vector2(x, 1.0f).normalized;

        rb2d.velocity = (direction * speed);

        timeToDestruction = Time.time + lifeTime;
    }

    private void Update()
    {
        Color auxColor = text.color;
        auxColor.a = Mathf.Clamp01( timeToDestruction - Time.time );
        text.color = auxColor;

        auxColor = outline.effectColor;
        auxColor.a = Mathf.Clamp01(timeToDestruction - Time.time);
        outline.effectColor = auxColor;

        if (Time.time > timeToDestruction)
        {
            Destroy(gameObject);
        }
    }
}
