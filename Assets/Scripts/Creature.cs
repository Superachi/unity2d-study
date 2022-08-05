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

    public enum Sounds
    {
        attack,
        hit,
        death
    }

    public Dictionary<Sounds, AudioClip> soundDictionary = new();

    public void takeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            die();
        }
        else
        {
            AudioManager.PlaySound("NPC_Hit_2", 0.5f);
        }
    }

    private void die()
    {
        AudioManager.PlaySound("NPC_Killed_14", 0.5f);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
