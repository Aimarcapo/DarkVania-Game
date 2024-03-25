using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExperienceScript : MonoBehaviour
{
    public Image expImage;
    public Text currentLevelText;
    public float currentExperience, experienceToNextLevel;
    public int currentLevel;
    public bool isPoisonActive = false;
    public static ExperienceScript instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (experienceToNextLevel <= 0)
        {
            experienceToNextLevel = 1;
        }
        currentExperience = PlayerPrefs.GetFloat("currentExperience", 0f);
        experienceToNextLevel = PlayerPrefs.GetFloat("ExperienceToNextLevel", experienceToNextLevel);
        currentLevel = PlayerPrefs.GetInt("CurrentLevel",1);
        Debug.Log(experienceToNextLevel+"experience next level");
        currentLevelText.text=currentLevel.ToString();
        expImage.fillAmount = currentExperience / experienceToNextLevel;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Venom.instance.venomAmount > 0)
        {
            StartCoroutine(ApplyPoisonEffect());
        }
    }
    IEnumerator ApplyPoisonEffect()
    {
        Venom.instance.venomAmount--;
        isPoisonActive = true;
        SubWeapons.instance.sr.color = Color.green;
        WeaponScript.instance.isPoisonActive = true;
        yield return new WaitForSeconds(5);
        isPoisonActive = false;
        SubWeapons.instance.sr.color = Color.white;
        WeaponScript.instance.isPoisonActive = false;

        Debug.Log("Poison effect finished!");
    }

    public void expModifier(float experience)
    {
        //currentExperience = PlayerPrefs.GetFloat("currentExperience", 0f);
        currentExperience += experience;

        experienceToNextLevel = PlayerPrefs.GetFloat("ExperienceToNextLevel", experienceToNextLevel);

        if (currentExperience >= experienceToNextLevel) 
        {
            experienceToNextLevel *= 2;
            currentExperience = 0;
            AudioManager.instance.PlayAudio(AudioManager.instance.levelUP);
            PlayerHealth.instance.maxHealth += 10f;
            Heart.instance.maxHearts += 5;
            currentLevel++;
           
        }
       

        expImage.fillAmount = currentExperience / experienceToNextLevel;
        currentLevelText.text = currentLevel.ToString();

    }
    public void DataToSave()
    {
        DataManager.instance.MaxHearts(Heart.instance.maxHearts);
        DataManager.instance.MaxHealth(PlayerHealth.instance.maxHealth);
        DataManager.instance.MaxVenom(Venom.instance.maxVenom);
        DataManager.instance.ExperienceData(currentExperience);//Updating the experience
        DataManager.instance.ExperienceToNextLevel(experienceToNextLevel);
        DataManager.instance.LevelData(currentLevel);

        currentExperience = PlayerPrefs.GetFloat("currentExperience");
        experienceToNextLevel = PlayerPrefs.GetFloat("ExperienceToNextLevel");
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        Heart.instance.maxHearts = PlayerPrefs.GetInt("maxHearts");
        PlayerHealth.instance.maxHealth = PlayerPrefs.GetFloat("maxHealth");
        Venom.instance.maxVenom= PlayerPrefs.GetInt("maxVenom");
    }
}
