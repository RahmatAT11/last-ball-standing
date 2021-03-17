using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
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
        // Move into player direction
        Vector3 playerDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(playerDirection * speed);
    }

    void CheckIfEnemyFall()
    {
        // Destroy enemy if drop below certain position
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    // multiply enemy speed over certain wave
    void ChangeSpeedEveryWave(int waveNum)
    {
        if (spawnManager.isWaveNumChanged)
        {
            float speedMultiplier = 1.002f;
            speed *= speedMultiplier;
        }
    }
}
