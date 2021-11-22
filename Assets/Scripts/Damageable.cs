using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField]
    private float maxLife = 0.0f;

    [SerializeField]
    private TakenDamageVFX takenDamageVFX;

    private float currentLife;
    private TransformWrapper transformWrapper;

    public float CurrentLife { get => currentLife; }
    public float MaxLife { get => maxLife; }

    private void Awake()
    {
        currentLife = maxLife;
        transformWrapper = new TransformWrapper(transform);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == Bullet.TAG)
        {
            Damager damager = col.GetComponent<Damager>();
            if (damager.CharacterTypeToHit.ToString() == tag)
            {
                damager.OnDamageDealt();
                OnDamageTaken(damager.Damage);
            }
        }
    }
    public void OnDamageTaken(float damage)
    {
        currentLife -= damage;

        TakenDamageVFX vfx = Instantiate(takenDamageVFX, 
                transformWrapper.Position, Quaternion.identity);
        vfx.Text.text = damage.ToString();

        if (currentLife <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
