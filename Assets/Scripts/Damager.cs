using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public enum characterType 
    {
        Enemy,
        Player,
    }

    [SerializeField]
    private characterType characterTypeToHit;

    [SerializeField]
    private float minDamage = 0.0f;

    [SerializeField]
    private float maxDamage = 0.0f;

    [SerializeField]
    private AudioSource audioSource = null;
    [SerializeField]
    private AudioClip[] spawnSFXs = null;
    [SerializeField]
    private float minPitch = 0.0f;
    [SerializeField]
    private float maxPitch = 0.0f;

    private void Awake()
    {
        SFXPlayer.Instance.PlaySFX
            (
                audioClip: spawnSFXs[Random.Range(0, spawnSFXs.Length)], 
                pitch: Random.Range(minPitch, maxPitch)
            );
    }

    public int Damage 
    {
        get 
        {
            return (int) Random.Range(minDamage, maxDamage);
        }
    }
    public characterType CharacterTypeToHit { get => characterTypeToHit; }

    /*
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == characterTypeToHit.ToString())
        {
            Damageable damageable = col.GetComponent<Damageable>();
            damageable.OnDamageTaken(damage);
            OnDamageDealt();
        }
    }
    */
    public void OnDamageDealt()
    {
        Destroy(gameObject);
    }
}
