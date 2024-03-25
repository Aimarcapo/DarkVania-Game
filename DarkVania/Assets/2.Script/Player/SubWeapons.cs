using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubWeapons : MonoBehaviour
{
    public int HeartCost;
    public SpriteRenderer sr;
    public GameObject arrow;
    public static SubWeapons instance;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        UseSubWeapon();
    }
    public void UseSubWeapon()
    {
        
        if (Input.GetButtonDown("Fire2")&&HeartCost<=Heart.instance.heartsAmount)
        {

            Heart.instance.Hearts(-HeartCost);
            AudioManager.instance.PlayAudio(AudioManager.instance.arrow);
            GameObject subItem = Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, -132));
            if (transform.localScale.x < 0)
            {
              
                subItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(-800f, 0f), ForceMode2D.Force);
                subItem.transform.localScale = new Vector2(-1, -1);
            }
            else
            {
               
                subItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(800f, 0f), ForceMode2D.Force);
            }
           
        }
       
    }
   
}
