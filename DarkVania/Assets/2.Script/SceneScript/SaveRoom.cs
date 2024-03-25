using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveRoom : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ExperienceScript.instance.DataToSave();
            Venom.instance.VenomSave();
            Heart.instance.HeartsSave();
            PlayerController.instance.respawnPoint=transform.position;
            PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
            Debug.Log("Checkpoint position" + PlayerController.instance.respawnPoint);
            print("Game Saved");

        }
    }
}
