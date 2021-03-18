using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPlayer : MonoBehaviour
{
    public GameObject powerupIndicator;
    public AudioClip hitEnemy;
    public AudioClip getPowerup;

    private AudioSource playerAudio;

    private bool hasPowerup;

    private PlayerControl playerControl;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerControl = GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // check if player has power up and hitting the enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerAudio.PlayOneShot(hitEnemy, 0.8f);
            if (hasPowerup)
            {
                GameObject enemy = collision.gameObject;
                Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
                Vector3 bounceDirection = enemy.transform.position - transform.position;

                enemyRb.AddForce(bounceDirection * playerControl.BouncePower, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if player get the powerup, then make the player have powerup if they don't have one
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
