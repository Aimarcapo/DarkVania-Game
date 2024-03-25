using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BankAccount : MonoBehaviour
{
    public float bank=0;
    public Text bankText;

    public static BankAccount instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        UpdateBankText();
    }
    public void Money(float cashCollected)
    {
        bank += cashCollected;
        UpdateBankText();
    }
    void UpdateBankText()
    {
        bankText.text = "x " + bank.ToString();
    }
}
