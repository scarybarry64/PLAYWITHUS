// UNFINISHED
// Always follows the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Man : MonoBehaviour
{

    // Local variables
    private GameManager game;
    new AudioManager audio;
    private Player player;
    private Rigidbody body;
    private NavMeshAgent agent;
    private Vector3 movement;
    private float speed, detection;

    private bool soundFlag = false;

    private void Awake()
    {

        game = FindObjectOfType<GameManager>();
        audio = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<Player>();
        body = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        speed = game.manSpeed;
        detection = game.manDetection;

    }

    // Update is called once per frame
    private void Update()
    {

        if (Vector3.Distance(transform.position, player.transform.position) <= detection)
        {
            FollowPlayer();

            if (!soundFlag) // Start playing sound on repeat
            {
                soundFlag = true;

                float random = Random.Range(0, 100);

                if (random < 33)
                {
                    audio.Play("Man1");
                }
                else if (random < 66 && random >= 33)
                {
                    audio.Play("Man2");
                }
                else
                {
                    audio.Play("Man3");
                }
                
            }
        }
        else
        {
            agent.SetDestination(transform.position);

            if (soundFlag)
            {
                soundFlag = false;

            }
        }

    }

    // Player go bye-bye when Pale Lady reaches em
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            game.status = "dead";
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
        //body.MovePosition(transform.position + (direction * speed * Time.deltaTime));
        agent.SetDestination(transform.position + (direction * speed * speed * speed * Time.deltaTime)); // NEEDS ADJUST
    }
}
