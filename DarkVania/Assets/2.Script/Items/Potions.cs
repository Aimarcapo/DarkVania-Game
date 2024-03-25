using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    public float healthToGive;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
            healthToGive = healthToGive / 100;
            collision.GetComponent<PlayerHealth>().health += collision.GetComponent<PlayerHealth>().maxHealth * healthToGive;
            Destroy(gameObject);
        }
    }
}
