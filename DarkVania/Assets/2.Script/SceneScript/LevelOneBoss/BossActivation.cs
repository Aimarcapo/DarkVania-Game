using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivation : MonoBehaviour
{
    public GameObject bossGo;
    private void Start()
    {
        bossGo.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BossUI.instance.BossActivator();
            StartCoroutine(WaitForBoss());
            AudioManager.instance.StopMusic(AudioManager.instance.backgroundMusic);
            AudioManager.instance.PlayAudio(AudioManager.instance.bossMusic);
            //Call the boss
           
        }
    }
    IEnumerator WaitForBoss()
    {
       var currentSpeed= PlayerController.instance.speed;
        PlayerController.instance.speed = 0;
        bossGo.SetActive (true);
        yield return new WaitForSeconds(3f);
        PlayerController.instance.speed=currentSpeed;
        Destroy(gameObject);
    }
}
