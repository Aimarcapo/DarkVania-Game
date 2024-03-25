using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int collectiblesToGive;
    public int venomToCollect;
    public bool isVenom;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           // Debug.Log(heartsToGive);
            
                if(isVenom==false) {
                Heart.instance.Hearts(collectiblesToGive);

            }
            else
            {
                Venom.instance.Venoms(venomToCollect);
            }
            
              
            
            Destroy(gameObject);
        }
    }
}
