using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string name;
    public float healthPoints;
    public float maxHealth;
    public float defense;
    public float speed;
    public float knockBackForceX;
    public float knockBackForceY;
    public float damageToGive;
    public float experienceToGive;
    public bool shouldRespawn;
    private void Start()
    {
        maxHealth = healthPoints;
    }
}
