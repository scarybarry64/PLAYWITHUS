using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{

    new AudioManager audio;

    private Text playButton;
    private bool hasStarted = false;

    private void Awake()
    {

        audio = FindObjectOfType<AudioManager>();
        playButton = FindObjectOfType<Canvas>().transform.Find("Play").GetComponent<Text>();

    }

    private void Update()
    {


        if (!hasStarted)
        {
            hasStarted = true;
            audio.Play("Ambience");
        }

        if (Random.Range(0, 1000) < 995f)
        {
            playButton.text = "PLAY ▶";
        }
        else
        {
            playButton.text = "PLAY ▶ WITH US";
        }

    }

    public void PlayGame()
    {
        StartCoroutine(Do());
    }

    private IEnumerator Do()
    {
        audio.Play("PlaySound");

        yield return new WaitForSecondsRealtime(1.5f);

        SceneManager.LoadScene("GameScene");
    }
}
