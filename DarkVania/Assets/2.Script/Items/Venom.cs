using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Venom : MonoBehaviour
{
    // Start is called before the first frame update
    public Text heartText;
    public int venomAmount;
    public int maxVenom;
    public static Venom instance;
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
        venomAmount = PlayerPrefs.GetInt("venomAmount", 0);
        maxVenom = PlayerPrefs.GetInt("maxVenom", 5); //Default max value is 5
        UpdateHeartText();
    }
    private void Update()
    {
        UpdateHeartText();
    }
    

    // Update is called once per frame

    public void Venoms(int ammo)
    {
        venomAmount += ammo;
        if (venomAmount > maxVenom)
        {
            venomAmount = maxVenom;
        }

        UpdateHeartText();
    }
    void UpdateHeartText()
    {
        heartText.text = "x " + venomAmount.ToString() + "/ " + maxVenom.ToString();
    }
    public void VenomSave()
    {
        DataManager.instance.CurrentVenom(venomAmount);
        venomAmount = PlayerPrefs.GetInt("venomAmount");
    }
}
