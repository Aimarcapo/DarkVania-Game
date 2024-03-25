using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Image healthImg;
    public float inmunityTime;
    bool isInmune;
    Blink material;
    SpriteRenderer sprite;
    public float knockbackForceX;
    public float knockbackForceY;
    public float attack;
    Rigidbody2D rigidbody2D;
    public GameObject GameOverImg;
    public static PlayerHealth instance;
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
        GameOverImg.SetActive(false);
        rigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        material = GetComponent<Blink>();
        maxHealth=PlayerPrefs.GetFloat("maxHealth",100f);
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthImg.fillAmount = health / maxHealth;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInmune)
        {
            health -= collision.GetComponent<Enemy>().damageToGive;
            if (collision.transform.position.x < transform.position.x)
            {
                rigidbody2D.AddForce(new Vector2(knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }
            else
            {
                rigidbody2D.AddForce(new Vector2(-knockbackForceX, knockbackForceY), ForceMode2D.Force);
            }
            StartCoroutine(Inmunity());
            if (health <= 0)
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.playerDead);
                Time.timeScale = 0;
                GameOverImg.SetActive(true);
                PlayerPrefs.DeleteAll();
                AudioManager.instance.backgroundMusic.Stop();
                AudioManager.instance.PlayAudio(AudioManager.instance.gameOver);
                //Pantalla de game over
                print("player dead");
            }
        }
        if (collision.CompareTag("Pincho"))
        {
            health -= collision.GetComponent<DamageDealers>().damageToGive;
            if (collision.transform.position.x < transform.position.x)
            {
                rigidbody2D.AddForce(new Vector2(knockbackForceX-100, knockbackForceY-100), ForceMode2D.Force);
            }
            else
            {
                rigidbody2D.AddForce(new Vector2(-(knockbackForceX-100), knockbackForceY-100), ForceMode2D.Force);
            }
            StartCoroutine(Inmunity());
            if (health <= 0)
            {
                AudioManager.instance.PlayAudio(AudioManager.instance.playerDead);
                Time.timeScale = 0;
                GameOverImg.SetActive(true);
                PlayerPrefs.DeleteAll();
                AudioManager.instance.backgroundMusic.Stop();
                AudioManager.instance.PlayAudio(AudioManager.instance.gameOver);
                //Pantalla de game over
                print("player dead");
            }
        }
       
    }
    IEnumerator Inmunity()
    {
        isInmune = true;
        sprite.material = material.blink;
        yield return new WaitForSeconds(inmunityTime);
        sprite.material = material.original;
        isInmune = false;
    }
}
