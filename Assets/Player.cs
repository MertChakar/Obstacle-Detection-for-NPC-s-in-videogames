using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;
    public GameObject deathEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
        {

            Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
        else
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }
}
