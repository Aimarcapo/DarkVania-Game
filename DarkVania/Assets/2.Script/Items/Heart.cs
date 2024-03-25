using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Heart : MonoBehaviour
{
    public Text heartText;
    public int heartsAmount;
    public int maxHearts;
    public static Heart instance;
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
        heartsAmount = PlayerPrefs.GetInt("heartsAmount", 0);
        maxHearts = PlayerPrefs.GetInt("maxHearts", 5); //Default max value is 5
        UpdateHeartText();
    }
    private void Update()
    {
        UpdateHeartText();
    }

    // Update is called once per frame

    public void Hearts(int ammo)
    {
        heartsAmount += ammo;
        if(heartsAmount > maxHearts)
        {
            heartsAmount = maxHearts;
        }
       
        UpdateHeartText();
    }
    void UpdateHeartText()
    {
        heartText.text = "x " + heartsAmount.ToString() +"/ "+maxHearts.ToString();
    }
   public void HeartsSave()
    {
        DataManager.instance.CurrentHearts(heartsAmount);
        heartsAmount = PlayerPrefs.GetInt("heartsAmount");
    }


}
