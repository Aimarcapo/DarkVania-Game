using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null) {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void MusicData(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }
    public void EffectsData(float value)
    {
        PlayerPrefs.SetFloat("EffectsVolume", value);
    }
    public void ExperienceData(float value)
    {
        PlayerPrefs.SetFloat("currentExperience", value);
    }
    public void ExperienceToNextLevel(float value)
    {
        PlayerPrefs.SetFloat("ExperienceToNextLevel", value);
    }

    public void LevelData(int value)
    {
        PlayerPrefs.SetInt("CurrentLevel", value);
    }
    public void CurrentHearts(int value)
    {
        PlayerPrefs.SetInt("heartsAmount", value);
    }
    public void MaxHearts(int value)
    {
        PlayerPrefs.SetInt("maxHearts", value);
    }
    public void CurrentVenom(int value)
    {
        PlayerPrefs.SetInt("venomAmount", value);
    }
    public void MaxVenom(int value)
    {
        PlayerPrefs.SetInt("maxVenom", value);
    }
    public void MaxHealth(float value)
    {
        PlayerPrefs.SetFloat("maxHealth", value);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
