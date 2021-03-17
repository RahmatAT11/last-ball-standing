using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 12.0f;
    private bool hasPowerup;
    private bool isGamePaused;
    private float verticalInput;

    private Rigidbody playerRb;
    private GameObject focusPoint;
    private AudioSource playerAudio;
    private GameManager gameManager;

    public GameObject powerupIndicator;
    public AudioClip hitEnemy;
    public AudioClip getPowerup;

    // Start is called before the first frame update
    private void Start()
    {
        focusPoint = GameObject.Find("Focus Point");
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    // Update is called once per frame
    // Put all method that contain input
    void Update()
    {
        GetVerticalInput();
        CheckPlayerPosition(transform.position);
    }

    // Put all method that calculating physics
    private void FixedUpdate()
    {
        RollPlayer();
    }

    private void CheckPlayerPosition(Vector3 position)
    {
        if (position.y < -5)
        {
            gameManager.isGameOver = true;
            gameManager.GameOver(0);
        }
    }

    // Get the input for RollPlayer method
    void GetVerticalInput()
    {
        // Player can roll around using the WASD key
        verticalInput = Input.GetAxis("Vertical");
    }

    // Roll player with WS keys
    void RollPlayer()
    {
        // Roll direction
        Vector3 rollDirection = focusPoint.transform.forward;

        // Roll the ball forward and backward
        playerRb.AddForce(rollDirection * speed * verticalInput);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // check if player has power up and hitting the enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAudio.PlayOneShot(hitEnemy, 0.8f);
            if (hasPowerup)
            {
                Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 bounceDirection = collision.gameObject.transform.position - transform.position;
                float bouncePower = 15.0f;

                enemyRb.AddForce(bounceDirection * bouncePower, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if player get the powerup
        if (other.CompareTag("Powerup"))
        {
            if (!hasPowerup)
            {
                hasPowerup = true;
                Destroy(other.gameObject);

                StartCoroutine(PowerupDurationRoutine());

                powerupIndicator.SetActive(true);
                playerAudio.PlayOneShot(getPowerup, 0.8f);
            }
        }
    }

    // start the coroutine for power up duration
    // The durations will not be added
    IEnumerator PowerupDurationRoutine()
    {
        float powerupDurationTIme = 7.0f;
        yield return new WaitForSeconds(powerupDurationTIme);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
}
