using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathManager : MonoBehaviour
{

    private AudioManager audio;
    private Text deathMessage;
    private Text timeMessage;
    private bool hasStarted = false;
    
    // Start is called before the first frame update
    private void Awake()
    {

        audio = FindObjectOfType<AudioManager>();
        deathMessage = FindObjectOfType<Canvas>().transform.Find("Dead").GetComponent<Text>();
        timeMessage = FindObjectOfType<Canvas>().transform.Find("Time").GetComponent<Text>();

    }

    private void Start()
    {
        timeMessage.text = "YOU LASTED: " + PlayerPrefs.GetString("CurrentScoreString") + "\nBEST TIME: " + PlayerPrefs.GetString("HighScoreString");
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (!hasStarted)
        {
            hasStarted = true;
            audio.Play("DeathChant");
            StartCoroutine(Crash());
        }

        if (Random.Range(0, 1000) < 980f)
        {
            deathMessage.text = "DEAD";
        }
        else
        {
            deathMessage.text = "DEAD   WE FOUND YOU";
        }

    }

    private IEnumerator Crash()
    {
        yield return new WaitForSecondsRealtime(8f);
        Application.Quit();
    }

}
