using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public bool isSpawning = false;
    public List<WaveEnemy> waveEnemies = new List<WaveEnemy>();
    public int currentWave = 1;
    public int waveValue = 2;
    public List<GameObject> generatedEnemies = new List<GameObject>();
    private Vector2 screenBounds;
    public Transform spawnLocation;
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        spawnLocation = this.transform;
    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (isSpawning)
        {
            LaunchWave();
        }
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
            yield return new WaitForSeconds(.1f);
            //random location off-screen
            

            Instantiate(generatedEnemies[0], spawnLocation.position, Quaternion.identity);
            Debug.Log("xdd");
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
        waveValue *= 2;
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
}

[System.Serializable]
public class WaveEnemy
{
    public GameObject enemyPrefab;
    public int cost;

}