using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponScript : MonoBehaviour
{
    float attack;
    float totalAttack;
    public float weaponAttack;
    public GameObject damageText;
    public float poisonDuration = 5f; // Duración del envenenamiento
    public bool isPoisonActive = false;
    public static WeaponScript instance;
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
        attack = PlayerHealth.instance.attack;
    }

    // Update is called once per frame
    void Update()
    {
        /* if (Input.GetKeyDown(KeyCode.Q) && Venom.instance.venomAmount > 0)
         {
             /*StartPoisonAbility();

             isPoisonActive = true;
             SubWeapons.instance.sr.color = Color.green;
         }*/

    }
    public float DamageInput(float enemyDefense,Transform hit)
    {
        totalAttack = attack + weaponAttack + (100 / (100 + enemyDefense));
        float finalAttackPower = Mathf.Round(Random.Range(totalAttack - 10, totalAttack + 5));

        GameObject text = Instantiate(damageText, hit.transform.position, Quaternion.identity);
        text.GetComponent<TextMeshPro>().SetText(finalAttackPower.ToString());

        if (finalAttackPower > totalAttack +3)
        {
            finalAttackPower *= 2;
            text.GetComponent<TextMeshPro>().SetText(finalAttackPower.ToString()+"!");
            Debug.Log("Critical");
        }
        if (finalAttackPower < 0)
        {
            finalAttackPower = 0;
            Debug.Log("Attack blocked");
        }
        /*if (isPoisonEnabled && Time.time - poisonStartTime < 5f && hit.CompareTag("Enemy"))
        {
            ApplyPoison(hit.gameObject);
            
        }*/


        StartCoroutine(MoveText(text));
        print(finalAttackPower);
        return finalAttackPower;
    }
    IEnumerator MoveText(GameObject go)
    {
        Vector2 initial=new Vector2(go.transform.position.x,go.transform.position.y);
        Vector2 final= new Vector2(go.transform.position.x, go.transform.position.y+10);
        int upTimes = 0;
        while (upTimes<6)
        {
            upTimes++;
            go.transform.position = Vector2.MoveTowards(initial,final,15f*Time.deltaTime);
            yield return new WaitForSeconds(1);
        }
    }
    /*void StartPoisonAbility()
    {
        isPoisonEnabled = true;
        poisonStartTime = Time.time;
        StartCoroutine(DisablePoisonAbility());
    }
    */
    // Método para desactivar la habilidad de envenenamiento después de 5 segundos
    /*IEnumerator DisablePoisonAbility()
    {
        yield return new WaitForSeconds(5f);
        isPoisonActive = false;
    }*/

    // Método para aplicar veneno a un enemigo

}
