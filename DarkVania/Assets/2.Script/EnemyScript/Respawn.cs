using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float timeToRespawn;
    public GameObject enemyToRespawn;
    public bool isRespawning;
    // Start is called before the first frame update
    void Start()
    {
        isRespawning = false;
        enemyToRespawn=transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator RespawnEnemy()
    {
        enemyToRespawn.SetActive(false);
        yield return new WaitForSeconds(timeToRespawn);
        enemyToRespawn.SetActive(true);
        enemyToRespawn.GetComponent<Enemy>().healthPoints =
          enemyToRespawn.GetComponent<EnemyHealth>().originalHealth;

        enemyToRespawn.GetComponent<SpriteRenderer>().material =
            enemyToRespawn.GetComponent<Blink>().original;

        enemyToRespawn.GetComponent<EnemyHealth>().isDamaged = false;
        yield return RespawnAnim();
    }
    IEnumerator RespawnAnim()
    {
        isRespawning=true;
        enemyToRespawn.GetComponent<Animator>().SetBool("isRespawning", true);
        yield return new WaitForSeconds(0.4f);
        enemyToRespawn.GetComponent<Animator>().SetBool("isRespawning", false);
        isRespawning=false;
    }
}
