using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    Enemy enemy;
    public bool isDamaged;
    public GameObject deathEffect;
    SpriteRenderer sprite;
    Blink material;
    Animator anim;
    Rigidbody2D rigidbody2D;
    public float originalHealth;
    private void Start()
    {
      
        sprite = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        material = GetComponent<Blink>();
        enemy = GetComponent<Enemy>();
        originalHealth = enemy.healthPoints;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
      
        if (enemy.healthPoints <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            ExperienceScript.instance.expModifier(GetComponent<Enemy>().experienceToGive);
            AudioManager.instance.PlayAudio(AudioManager.instance.enemyDead);
            //Respawn
            if (enemy.shouldRespawn)
            {
                transform.GetComponentInParent<Respawn>().StartCoroutine
                    (GetComponentInParent<Respawn>().RespawnEnemy());
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Weapon") & !isDamaged)
        {
            //Debug.Log("Caminando entra");
            enemy.healthPoints -= collision.GetComponent<WeaponScript>().DamageInput(enemy.defense,this.transform);
            //enemy.healthPoints -= 1f;
            AudioManager.instance.PlayAudio(AudioManager.instance.hit);
            if (collision.transform.position.x < transform.position.x)
            {
                //Debug.Log("El daño viene por la izquierda");
                rigidbody2D.AddForce(new Vector2(enemy.knockBackForceX, enemy.knockBackForceY), ForceMode2D.Force);
                //Debug.Log(enemy.knockBackForceX+"fuerza");
            }
            else
            {
                //Debug.Log("El daño viene por la derecha");
                rigidbody2D.AddForce(new Vector2(-enemy.knockBackForceX, enemy.knockBackForceY), ForceMode2D.Force);
                //Debug.Log(enemy.knockBackForceX+"fuerza");
            }

            StartCoroutine(Damager());
            /*if (enemy.healthPoints <= 0)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
                ExperienceScript.instance.expModifier(GetComponent<Enemy>().experienceToGive);
                AudioManager.instance.PlayAudio(AudioManager.instance.enemyDead);
                //Respawn
                if(enemy.shouldRespawn)
                {
                    transform.GetComponentInParent<Respawn>().StartCoroutine
                        (GetComponentInParent<Respawn>().RespawnEnemy());
                }
                else
                {
                    Destroy(gameObject);
                }
               
            }*/
            WeaponScript weapon = collision.GetComponent<WeaponScript>();
            if (weapon != null && weapon.isPoisonActive)
            {
                // Apply poison effect to the enemy
                StartCoroutine(ApplyPoisonEffect());
                weapon.isPoisonActive = false;
                ExperienceScript.instance.isPoisonActive = false;
            }
        }
        
    }
    IEnumerator Damager()
    {
        isDamaged = true;
       // anim.SetBool("Idle", true);
        sprite.material = material.blink;
        yield return new WaitForSeconds(0.5f);
        isDamaged = false;
        sprite.material = material.original;
    }
    IEnumerator ApplyPoisonEffect()
    {
        float poisonDuration = 5f;
        float poisonTickInterval = 1f;
        float totalPoisonTicks = poisonDuration / poisonTickInterval;
        float poisonDamagePercentagePerTick = 0.05f;

        for (int i = 0; i < totalPoisonTicks; i++)
        {
            yield return new WaitForSeconds(poisonTickInterval);
            enemy.healthPoints -= enemy.maxHealth * poisonDamagePercentagePerTick;
        }
        

        Debug.Log("Poison effect finished!");
    }
}
