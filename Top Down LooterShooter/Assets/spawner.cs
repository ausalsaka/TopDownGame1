using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawner : MonoBehaviour
{
    public GameObject enemy;
    public static int mode = 0;
    public static int currentEnemies = 0;
    //public int activateAfterWave = 0;
    [HideInInspector]
    public int wave = 1;
    private float enemiesToSpawn = 5;
    private int enemiesSpawned = 0;
    private bool stopSpawning = false;
    private bool currentlySpawning = false;
    private float spawnDelay = 2f;
    private float spawnTime = .1f;
    private bool newWaveStarted = false;
    public Text text;

    private void Start()
    {
        text.text = "Shoot Barrel To Begin";
    }

    private void Update()
    {
        if(mode == 1)
        {
            text.text = wave.ToString();
            if (enemiesSpawned / enemiesToSpawn >= 1)
            {
                stopSpawning = true;
                currentlySpawning = false;
            }
            if (currentlySpawning == false && stopSpawning == false)
            {
                enemiesToSpawn = (Mathf.Pow(1.07f, 55 + wave) -41);
                InvokeRepeating("spawnEnemy", spawnTime, spawnDelay);
                currentlySpawning = true;
            }

            if (stopSpawning == true && currentEnemies == 0 && newWaveStarted == false)
            {
                StartCoroutine(newWave());
            }

        }
    }

    IEnumerator newWave()
    {
        newWaveStarted = true;
        yield return new WaitForSeconds(10f);
        wave += 1;
        text.text = wave.ToString();
        enemiesSpawned = 0;
        Debug.Log("newwave");
        stopSpawning = false;
        newWaveStarted = false;
        //
    }

    void spawnEnemy()
    {
        if(stopSpawning)
        {
            CancelInvoke("spawnEnemy");
        }
        else
        {
            Instantiate(enemy, gameObject.transform);
            enemiesSpawned += 1;
            currentEnemies += 1;
            Debug.Log("enemiesSpawned: " + enemiesSpawned + "/" + enemiesToSpawn + "        current: " + currentEnemies);
        }

    }
}
