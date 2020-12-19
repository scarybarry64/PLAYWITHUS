// UNFINISHED
// Always follows the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Child : MonoBehaviour
{

    // Local variables
    private GameManager game;
    new AudioManager audio;
    private Player player;
    private Rigidbody body;
    private Vector3 movement;
    private float speed;

    private LayerMask mask;
    private bool inFlashlight = false;
    private bool soundFlag = false;


    private void Awake()
    {

        game = FindObjectOfType<GameManager>();
        audio = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<Player>();
        body = GetComponent<Rigidbody>();
        speed = game.childSpeed;
        mask = LayerMask.GetMask("Obstacles");

    }

    // Update is called once per frame
    private void Update()
    {

        if (inFlashlight && !Physics.Linecast(transform.position, player.transform.position, mask))
        {

            FollowPlayer();

            if (!soundFlag) // Start playing sound on repeat
            {
                soundFlag = true;
                audio.Play("ChildScream");
            }
            

        }
        else
        {

            if (soundFlag)
            {
                soundFlag = false;
                audio.Stop("ChildScream");
            }


        }

    }

    // Player go bye-bye when Pale Lady reaches em
    private void OnTriggerEnter(Collider collider)
    {

        if (collider.CompareTag("Flashlight"))
        {
            inFlashlight = true;
        }
        if (collider.CompareTag("Player"))
        {
            game.status = "dead";
        }

    }

    private void OnTriggerExit(Collider collider)
    {

        if (collider.CompareTag("Flashlight"))
        {
            inFlashlight = false;
        }

    }

    // Faces and moves toward the player
    // Code from: https://www.youtube.com/watch?v=4Wh22ynlLyk
    private void FollowPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        body.rotation = Quaternion.Euler(0, angle, 0);
        direction.Normalize();
        body.MovePosition(transform.position + (direction * speed * Time.deltaTime));

    }

}
