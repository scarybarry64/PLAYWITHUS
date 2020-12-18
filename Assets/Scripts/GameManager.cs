// Acts as a central brain that connects the game together
// Stores the various settings for the player and enemies

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Player variables
    [Header("PLAYER SETTINGS")]
    [Range(100, 1000)] public float lookSensitivity;
    [Range(1, 50)] public float playerSpeed;

    // Enemy variables
    [Header("ENEMY SETTINGS")]
    [Range(1, 200)] public float manSpeed;
    [Range(0.01f, 100)] public float manDetection;
    [Range(1, 200)] public float womanSpeed;
    [Range(0.01f, 100)] public float womanDetection;
    [Range(1, 200)] public float childSpeed;


    // Other stuff
    [HideInInspector] public string status;

    private Player player;

    private void Awake()
    {

        player = FindObjectOfType<Player>();

    }

    private void Update()
    {
        
        if (status != "play")
        {

            // UNCOMMENT TO RESET HIGH SCORE
            //PlayerPrefs.SetFloat("HighScoreFloat", player.totalTime);
            //PlayerPrefs.SetString("HighScoreString", player.score);

            PlayerPrefs.SetString("CurrentScoreString", player.score);

            if (player.totalTime > PlayerPrefs.GetFloat("HighScoreFloat"))
            {
                PlayerPrefs.SetFloat("HighScoreFloat", player.totalTime);
                PlayerPrefs.SetString("HighScoreString", player.score);
            }

            SceneManager.LoadScene("DeathScene");

        }

    }

}
