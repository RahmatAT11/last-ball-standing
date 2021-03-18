using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    float speedMultiplier = 1.002f;

    private Rigidbody enemyRb;
    private GameObject player;
    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        spawnManager = GameObject.FindObjectOfType<SpawnManager>().GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSpeedEveryWave(spawnManager.waveNum);
        CheckIfEnemyFall();
    }

    private void FixedUpdate()
    {
        EnemyRoll();
    }

    // Mechanic for how the enemy find the player and roll to player vector position
    void EnemyRoll()
    {
        if (!GameManager.isPause)
        {
            // Move into player direction
            Vector3 playerDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(playerDirection * speed);
        }
    }

    // Check the enemy if they fall from the arena
    void CheckIfEnemyFall()
    {
        float yBound = 5.0f;

        // Destroy enemy if drop below certain position
        if (transform.position.y < -yBound)
        {
            Destroy(gameObject);
        }
    }

    // multiply enemy speed over certain wave number
    void ChangeSpeedEveryWave(int waveNum)
    {
        if (spawnManager.isWaveNumChanged)
        {
            speed *= speedMultiplier;
        }
    }
}
