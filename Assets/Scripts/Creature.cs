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

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            die();
        }
    }

    private void die()
    {
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
