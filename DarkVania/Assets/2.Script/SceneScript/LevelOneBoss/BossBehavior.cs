using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehavior : MonoBehaviour
{
    public Transform[] transforms;
    public GameObject flame;
    public float timeToShoot, countdown;
    public float timeToTP, countdownToTP;
    public float bossHealth, currentHealth;
    public Image healthImage;

    // Start is called before the first frame update
    void Start()
    {
        var initialPosition = Random.Range(0, transforms.Length);
        transform.position = transforms[1].position;
        //bossHealth = currentHealth;
        countdown = timeToShoot;
        countdownToTP = timeToTP;
    }
    private void Update()
    {
        CountDowns();
        DamageBoss();
        BossScale();
    }
    public void CountDowns()
    {
        countdown -= Time.deltaTime;
        countdownToTP -= Time.deltaTime;
        if (countdown <= 0f)
        {
            ShootPlayer();
            AudioManager.instance.PlayAudio(AudioManager.instance.fireball);
            countdown = timeToShoot;

        }
        if (countdownToTP <= 0f)
        {
            countdownToTP = timeToTP;
            Teleport();
        }
    }
    public void ShootPlayer()
    {
        GameObject spell = Instantiate(flame, transform.position, Quaternion.identity);
    }
    public void Teleport()
    {
        var initialPosition = Random.Range(0, transforms.Length);
        transform.position = transforms[initialPosition].position;

    }
    public void DamageBoss()
    {
        currentHealth = GetComponent<Enemy>().healthPoints;
        healthImage.fillAmount = currentHealth / bossHealth;
    }
    private void OnDestroy()
    {
        BossUI.instance.BossDeativator();
    }
    public void BossScale()
    {
        if (transform.position.x > PlayerController.instance.transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

}
