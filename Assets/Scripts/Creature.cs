using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public float health = 1f;

    public enum Types
    {
        player,
        enemy,
        npc
    }

    public Types type;

    public AudioClip soundOnHit;
    public AudioClip soundOnDeath;

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
        else
        {
            AudioManager.PlaySound(soundOnHit.name, 0.5f);
        }
    }

    private void Die()
    {
        AudioManager.PlaySound(soundOnDeath.name, 0.5f);
        Destroy(gameObject);
    }
}
