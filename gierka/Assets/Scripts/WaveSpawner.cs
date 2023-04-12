using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public bool isSpawning = false;
    public List<WaveEnemy> waveEnemies = new List<WaveEnemy>();
    public int currentWave = 1;
    public int waveValue = 10;
    public List<GameObject> generatedEnemies = new List<GameObject>();
    public float spawnOffset = 5f;
    private Camera mainCamera;
    private float enemySpawningCooldown;

    void Start()
    {
        mainCamera = Camera.main;
        enemySpawningCooldown = .5f;
    }

    void FixedUpdate()
    {
        if (isSpawning) LaunchWave();
    }
    void LaunchWave()
    {
        GenerateWave();
        StartCoroutine(SpawnEnemies());
        isSpawning = false;
    }

    IEnumerator SpawnEnemies()
    {
        while (generatedEnemies.Count > 0)
        {
            yield return new WaitForSeconds(enemySpawningCooldown);

            Vector3 spawnPos;
            float spawnRadius;
            bool isColliding;
            //DOENST WORK - location and radius is good, probably something wrong with mask xd
            do
            {
                spawnPos = GetOffScreenPosition();
                spawnRadius = System.Math.Max(generatedEnemies[0].transform.localScale.x, generatedEnemies[0].transform.localScale.y);
                isColliding = Physics.CheckSphere(spawnPos, spawnRadius, LayerMask.GetMask("Floor"));
                //                                                  here ^^^^^^^^^^^^^^^^^^^^^^^^^^^
            }
            while (isColliding); 

            Instantiate(generatedEnemies[0], spawnPos, Quaternion.identity);
            generatedEnemies.RemoveAt(0);
        }
    }
    public void NextWave()
    {
        currentWave++;
        isSpawning = true;
    }
    public void GenerateWave()
    {
        CalculateWaveValue();
        GenerateEnemies();
    }
    public void CalculateWaveValue()
    {
        //TODO: Make this more interesting
        waveValue *= 1;
    }

    public void GenerateEnemies()
    {
        generatedEnemies = new List<GameObject>();
        int currentWaveValue = waveValue;
        while (currentWaveValue > 0)
        {
            int randomEnemy = Random.Range(0, waveEnemies.Count);
            int randomEnemyCost = waveEnemies[randomEnemy].cost;
            if (currentWaveValue >= randomEnemyCost)
            {
                currentWaveValue -= randomEnemyCost;
                generatedEnemies.Add(waveEnemies[randomEnemy].enemyPrefab);
            }
        }
    }
    Vector3 GetOffScreenPosition()
    {
        Vector3 spawnPos = Vector3.zero;

        Vector2 topLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector2 bottomLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector2 topRightCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector2 bottomRightCorner = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0));
        // Get a random edge of the viewport
        int randomEdge = Random.Range(0, 4);
        switch (randomEdge)
        {
            case 0: // Top edge
                spawnPos = new Vector3(Random.Range(topLeftCorner.x, topRightCorner.x), topLeftCorner.y + spawnOffset, 0f);
                break;
            case 1: // Right edge
                spawnPos = new Vector3(topRightCorner.x + spawnOffset, Random.Range(topRightCorner.y, bottomRightCorner.y), 0f);
                break;
            case 2: // Bottom edge
                spawnPos = new Vector3(Random.Range(bottomLeftCorner.x, bottomRightCorner.x), bottomLeftCorner.y - spawnOffset, 0f);
                break;
            case 3: // Left edge
                spawnPos = new Vector3(topLeftCorner.x - spawnOffset, Random.Range(topLeftCorner.y, bottomLeftCorner.y), 0f);
                break;
        }
        return spawnPos;
    }
}

[System.Serializable]
public class WaveEnemy
{
    public GameObject enemyPrefab;
    public int cost;

}