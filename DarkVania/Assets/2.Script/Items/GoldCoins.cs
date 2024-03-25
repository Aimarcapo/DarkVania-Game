using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoldCoins : MonoBehaviour
{
    public float cash;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Cash:"+cash);
            Debug.Log("Collision" + collision.gameObject);
            BankAccount.instance.Money(cash);
            AudioManager.instance.PlayAudio(AudioManager.instance.gems);
            Destroy(gameObject);
        }
    }
}
