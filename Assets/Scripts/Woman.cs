// "Pale Lady" - based off the Weeping Angels from Doctor Who
// Moves toward the payer when not in camera view

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woman : MonoBehaviour
{

    // Local variables
    private GameManager game;
    private Player player;
    private Rigidbody body;
    private Vector3 movement;
    private float speed, detectionRadius;
    private bool visible = false;

    // Initialize stuff
    private void Awake()
    {

        game = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
        body = GetComponent<Rigidbody>();
        speed = game.womanSpeed;
        detectionRadius = game.womanDetection;

    }

    // Every frame, face player and follow if not in camera view
    private void Update()
    {

        if (game.status == "play")
        {
            
            // Face player at all times
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            body.rotation = Quaternion.Euler(0, angle, 0);
            direction.Normalize();

            // Move towards player if not in camera view
            // Code from: https://www.youtube.com/watch?v=4Wh22ynlLyk
            if (!visible)
            {
                body.MovePosition(transform.position + (direction * speed * Time.deltaTime));
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
