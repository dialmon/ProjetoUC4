using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneratorEnemys : MonoBehaviour
{
    public GameObject[] prefabFishs;
    public static int lostObjectsForGameOver = 3;
    public TextMeshProUGUI score_txt;
    public TextMeshProUGUI missScore_txt;
    public TextMeshProUGUI waveInfoText;
    public int maxEnemiesPerWave = 10;
    public float initialSpawnDelay = 3f;
    public float spawnInterval = 3f;
    public float spawnRateIncrease = 0.2f;

    private int currentWave = 1;
    private int enemiesSpawned = 0;
    private float nextSpawnTime = 0f;

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    public void Update()
    {
        //Score();
    }

    IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(initialSpawnDelay);

        while (true)
        {
            if (Time.time >= nextSpawnTime && enemiesSpawned < maxEnemiesPerWave)
            {
                float x = Random.Range(-17f, 17f);
                float y = 10f;
                Vector2 spawnPosition = new Vector2(x, y);
                Instantiate(prefabFishs[Random.Range(0, prefabFishs.Length)], spawnPosition, Quaternion.identity);
                enemiesSpawned++;
                nextSpawnTime = Time.time + spawnInterval;
            }
            if (enemiesSpawned >= maxEnemiesPerWave)
            {
                currentWave++;
                maxEnemiesPerWave += Mathf.RoundToInt(maxEnemiesPerWave * spawnRateIncrease);
                enemiesSpawned = 0;
                nextSpawnTime = Time.time + initialSpawnDelay;
                spawnInterval -= 0.1f;
            }
            waveInfoText.text = string.Format("Wave: {0}\nEnemies Spawned: {1}/{2}\nSpawn Interval: {3:F1}s", currentWave, enemiesSpawned, maxEnemiesPerWave, spawnInterval);

            yield return null;
        }
    }

    //public void Score()
    //{
    //    //texts que aparecem no game
    //    missScore_txt.text = "peixes caídos: " + PlayerColeta.missingObjects.ToString();
    //    score_txt.text = "Pontuação: " + FishsFalling.points.ToString();
    //}

    public static void GameOver()
    {
        // Exibir a tela de game over
        SceneManager.LoadScene("GameOver");
    }
}
