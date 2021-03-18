using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject powerup;
    private int numberOfEnemies;
    private float spawnRangeX = 12.0f;
    private float spawnRangeZ = 12.0f;
    private GameManager gameManager;

    public bool isWaveNumChanged;
    public int waveNum;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnManagerStart();
    }

    // start the spawn
    void SpawnManagerStart()
    {
        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (numberOfEnemies == 0)
        {
            waveNum++;
            gameManager.UpdateWaveNum(waveNum);

            isWaveNumChanged = true;

            StartCoroutine(WaveNumberChangedRoutine());

            SpawnEnemy(waveNum);
            SpawnPowerup();
        }

        gameManager.UpdateBallsRemain(numberOfEnemies);

        if (waveNum > 5)
        {
            GameManager.isOver = true;
            gameManager.GameOver(1);
        }
    }

    // generate a random vector position
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);
        float spawnPosY = 0f;

        Vector3 randomPos = new Vector3(spawnRangeX, spawnPosY, spawnPosZ);

        return randomPos;
    }

    // instantiate the enemy
    private void SpawnEnemy(int spawnNumbersOfEnemies)
    {
        for (int i = 0; i < spawnNumbersOfEnemies; i++)
        {
            int randomNum = Random.Range(0, enemies.Length);
            Instantiate(enemies[randomNum], GenerateSpawnPosition(), enemies[randomNum].transform.rotation);
        }
    }

    // Instantiate the powerup
    private void SpawnPowerup()
    {
        Instantiate(powerup, GenerateSpawnPosition(), powerup.transform.rotation);
    }

    // routine for letting wave number has changed to other scripts
    IEnumerator WaveNumberChangedRoutine()
    {
        if (isWaveNumChanged)
        {
            yield return new WaitForSecondsRealtime(1);
            isWaveNumChanged = false;
        }
    }
}
