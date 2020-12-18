// Handles player controls and UI stuff

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    // Local variables
    private GameManager game;
    new AudioManager audio;
    private CharacterController controller;
    new Transform camera;
    private Text status;
    private Text time;
    private Text menu;
    private float sensitivity, speed;
    private float rotationX = 0f;
    private float initialTime, initialTimeSound;

    [HideInInspector]
    public string score;
    [HideInInspector]
    public float totalTime;





    // MAX CODE
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 3.0f;
    private float gravityValue = -9.81f;










    // Begin by getting stuff from game manager
    private void Awake()
    {

        game = FindObjectOfType<GameManager>();
        audio = FindObjectOfType<AudioManager>();
        controller = GetComponent<CharacterController>();
        camera = transform.Find("Camera");
        status = transform.Find("VHS Overlay").Find("Status").GetComponent<Text>();
        time = transform.Find("VHS Overlay").Find("Time").GetComponent<Text>();
        menu = transform.Find("VHS Overlay").Find("Menu").GetComponent<Text>();
        sensitivity = game.lookSensitivity;
        speed = game.playerSpeed;

    }

    // Set up stuff
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        game.status = "play";
        initialTime = Time.time;
        initialTimeSound = Time.time;
        menu.enabled = false;
    }

    // Handle looking and movement every frame, also resume/exit
    void Update()
    {

        HandleLooking();
        HandleMovement();
        HandleScore();
        HandleStatus();

    }

    // Look around using mouse
    private void HandleLooking()
    {

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        camera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

    }

    // Move using WASD or arrows, play footstep sounds
    private void HandleMovement()
    {

        // BARRY'S CODE
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical"); // Unity uses xzy axis order
        //Vector3 movement = transform.right * x + transform.forward * z;
        //controller.Move(movement * speed * Time.deltaTime);


        //// If moving, randomly adjust footstep sound pitch and play often
        //if ((Time.time - initialTimeSound > 0.5) && (x != 0 || z != 0))
        //{

        //    audio.SetPitch("Footstep2", Random.Range(0.1f, 1.5f));
        //    audio.Play("Footstep2");
        //    initialTimeSound = Time.time;

        //}

        // MAX'S CODE
        var oldPos = transform.localPosition;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.up * speed * 17 * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * speed * 17 * Time.deltaTime);
        }

        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        Vector3 camF = camera.forward;
        Vector3 camR = camera.right;

        camF.y = 0;
        camR.y = 0;

        camF = camF.normalized;
        camR = camR.normalized;

        var newPos = (camF * input.y + camR * input.x) * speed;

        var characterController = this.GetComponent<CharacterController>();
        characterController.SimpleMove(newPos);

        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

    }

    // Checks and displays game status
    private void HandleStatus()
    {

        if (Random.Range(0, 1000) < 993)
        {
            //menu.enabled = false;
            status.text = "PLAYING ▶";
        }
        else
        {
            //menu.enabled = true;
            status.text = "PLAYING ▶ WE'RE COMING";
        }


    }

    // Displays the time stuff for when alive and dead
    private void HandleScore()
    {

        //if (game.status == "play")
        //{

        //    //totalTime = Time.time + 6360;
        //    totalTime = Time.time - initialTime;
        //    //int hours = (int)((totalTime / 3600) % 60);
        //    int minutes = (int)((totalTime / 60) % 60);
        //    int seconds = (int)(totalTime % 60);
        //    int milliseconds = (int)((totalTime * 100) - (((int)totalTime) * 100));

        //    //string scoreHour = hours.ToString();
        //    string scoreMin = minutes.ToString();
        //    string scoreSec = seconds.ToString();
        //    string scoreMil = milliseconds.ToString();

        //    if (milliseconds < 10)
        //    {
        //        scoreMil = "0" + scoreMil;
        //    }
        //    //if (hours < 10)
        //    //{
        //    //    scoreHour = "0" + scoreHour;
        //    //}
        //    if (seconds < 10)
        //    {
        //        scoreSec = "0" + scoreSec;
        //    }
        //    if (minutes < 10)
        //    {
        //        scoreMin = "0" + scoreMin;
        //    }

        //    //score = scoreHour + ":" + scoreMin + ":" + scoreSec + ":" + scoreMil;
        //    score = scoreMin + ":" + scoreSec + ":" + scoreMil;
        //    time.text = score + "\n29 OCT. 1998";

        //}

        //else if (game.status == "dead")
        //{
        //    //PlayerPrefs.SetFloat("HighScore", totalTime);
        //    if (totalTime > PlayerPrefs.GetFloat("HighScoreFloat"))
        //    {
        //        PlayerPrefs.SetFloat("HighScoreFloat", totalTime);
        //        PlayerPrefs.SetString("HighScoreString", score);
        //    }

        //    Debug.Log(totalTime);

        //    time.text = "YOU LASTED: " + score + "\nBEST TIME: " + PlayerPrefs.GetString("HighScoreString");
        //}

        //totalTime = Time.time + 6360;
        totalTime = Time.time - initialTime;
        //int hours = (int)((totalTime / 3600) % 60);
        int minutes = (int)((totalTime / 60) % 60);
        int seconds = (int)(totalTime % 60);
        int milliseconds = (int)((totalTime * 100) - (((int)totalTime) * 100));

        //string scoreHour = hours.ToString();
        string scoreMin = minutes.ToString();
        string scoreSec = seconds.ToString();
        string scoreMil = milliseconds.ToString();

        if (milliseconds < 10)
        {
            scoreMil = "0" + scoreMil;
        }
        //if (hours < 10)
        //{
        //    scoreHour = "0" + scoreHour;
        //}
        if (seconds < 10)
        {
            scoreSec = "0" + scoreSec;
        }
        if (minutes < 10)
        {
            scoreMin = "0" + scoreMin;
        }

        //score = scoreHour + ":" + scoreMin + ":" + scoreSec + ":" + scoreMil;
        score = scoreMin + ":" + scoreSec + ":" + scoreMil;
        time.text = score + "\n29 OCT. 1998";



    }
}
