// "Pale Lady" - based off the Weeping Angels from Doctor Who
// Moves toward the payer when not in camera view

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woman : MonoBehaviour
{

    // Local variables
    private GameManager game;
    new AudioManager audio;
    private Player player;
    private Rigidbody body;
    private Vector3 movement;
    private float speed, detection;
    private bool visible = false;
    private bool soundFlag = false;

    // Initialize stuff
    private void Awake()
    {

        game = FindObjectOfType<GameManager>();
        audio = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<Player>();
        body = GetComponent<Rigidbody>();
        speed = game.womanSpeed;
        detection = game.womanDetection;

    }

    // Every frame, face player and follow if not in camera view
    private void Update()
    {

        // Face player at all times
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        body.rotation = Quaternion.Euler(0, angle, 0);
        direction.Normalize();

        // Move towards player if not in camera view
        // Code from: https://www.youtube.com/watch?v=4Wh22ynlLyk
        if (!visible && Vector3.Distance(transform.position, player.transform.position) <= detection)
        {
            body.MovePosition(transform.position + (direction * speed * Time.deltaTime));

            if (!soundFlag)
            {

                soundFlag = true;

                float random = Random.Range(0, 100);

                if (random < 20)
                {
                    audio.Play("Moan1");
                }
                else if (random < 40 && random >= 20)
                {
                    audio.Play("Moan2");
                }
                else if (random < 60 && random >= 40)
                {
                    audio.Play("Moan3");
                }
                else if (random < 80 && random >= 60)
                {
                    audio.Play("Moan4");
                }
                else
                {
                    audio.Play("Moan5");
                }

            }
        }
        else
        {
            if (soundFlag)
            {
                soundFlag = false;
            }
        }

    }

    // Track visiblity
    private void OnBecameInvisible()
    {
        visible = false;
    }

    // Ditto
    private void OnBecameVisible()
    {
        visible = true;
    }

    // Player go bye-bye when Pale Lady reaches em
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            game.status = "dead";
        }
    }

}
