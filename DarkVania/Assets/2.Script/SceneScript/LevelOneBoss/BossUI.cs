using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BossUI : MonoBehaviour
{
    public GameObject bossPanel;
    public GameObject muros;
    public bool isLast;
    public static BossUI instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        bossPanel.SetActive(false);
        muros.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BossActivator()
    {
        bossPanel.SetActive(true);
        muros.SetActive(true);
    }
    public void BossDeativator()
    {
       
  
      
        bossPanel.SetActive(false);
        muros.SetActive(false);
        StartCoroutine(BossDefeated());
    }
    IEnumerator BossDefeated()
    {
        PlayerController.instance.Idle();
        var velocity=PlayerController.instance.GetComponent<Rigidbody2D>().velocity;
        Vector2 originalSpeed= velocity;
        velocity = new Vector2(0, velocity.y);
        PlayerController.instance.speed = 0;
        PlayerController.instance.enabled = false;
        yield return new WaitForSeconds(3);
        AudioManager.instance.StopMusic(AudioManager.instance.bossMusic);
        AudioManager.instance.PlayAudio(AudioManager.instance.victoryMusic);
        PlayerController.instance.enabled = true;
        velocity = originalSpeed;
        new WaitForSeconds(20);
        if(isLast==true)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
       
    }
}
