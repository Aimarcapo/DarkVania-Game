using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Animator settingsAnimator;
   
    public static MainMenuScript instance;
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
        /*
        UnityEngine.SceneManagement.Scene scene =SceneManager.GetActiveScene();
        if (scene.name == "MainMenu")
        {
           AudioManager.instance.backgroundMusic.Stop();
            AudioManager.instance.PlayAudio(AudioManager.instance.mainMenu);
        }*/
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        string lastScene = PlayerPrefs.GetString("LastScene","Level 1");
        SceneManager.LoadScene(lastScene);
        //SceneManager.LoadScene(1);
    }
    public void RestartGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
       
        Application.Quit();
        print("Game Closed");
    }
    public void GoToMainMenu()
    {
      //TextButton.text = "Continue Game";
        SceneManager.LoadScene(0);
    }
    public void ShowSettings()
    {
        settingsAnimator.SetBool("ShowSettings", true);
    }
    public void HideSettings() {
        settingsAnimator.SetBool("ShowSettings", false);
    }
}
