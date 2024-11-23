using System.Collections;
using System.Collections.Generic;
using TMPro; // For TextMeshPro UI
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Enemies = new List<GameObject>();
    [SerializeField] private GameObject smallEnemyPb;
    [SerializeField] private GameObject largeEnemyPb;
    [SerializeField] private GameObject largeEnemyArmorPb;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private int maxEnemies = 1000;     
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int waveNumber = 1;       
    [SerializeField] private int enemiesPerWave = 5;  
    [SerializeField] private float waveBreakTime = 5f;
    [SerializeField] private float spawnIntervalDecrement = 0.1f; 
    [SerializeField] private int additionalEnemiesPerWave = 2;
    private bool spawningWave = false;
    [SerializeField] private TextMeshProUGUI waveText;

    private void Start()
    {
        UpdateWaveText(); 

        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            spawningWave = true;

            for (int i = 0; i < enemiesPerWave; i++)
            {
                if (Enemies.Count < maxEnemies)
                {
                    Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                    GameObject enemyToSpawn = RandomEnemyType();

                    GameObject newEnemy = Instantiate(enemyToSpawn, spawnPoint.position, Quaternion.identity);

                    AddEnemy(newEnemy);
                }
                yield return new WaitForSeconds(spawnInterval);
            }

            spawningWave = false;

            yield return new WaitForSeconds(waveBreakTime);

            waveNumber++;

            enemiesPerWave += additionalEnemiesPerWave; 

            spawnInterval = Mathf.Max(spawnInterval - spawnIntervalDecrement, 0.5f);

            UpdateWaveText(); 
        }
    }

    private GameObject RandomEnemyType()
    {
        float randomValue = Random.value; 

        if (randomValue < 0.6f) 

            return smallEnemyPb;
        else if (randomValue < 0.9f) 

            return largeEnemyPb;
        else 
            return largeEnemyArmorPb;
    }

    public void AddEnemy(GameObject enemy)
    {
        if (!Enemies.Contains(enemy))
        {
            Enemies.Add(enemy);
        }
    }
    private void UpdateWaveText()
    {
        if (waveText != null)
        {
            waveText.text = "Wave: " + waveNumber;
        }
    }
}
