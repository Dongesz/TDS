using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int BaseEnemies = 8;
    [SerializeField] private float enemiesPerSec = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();
    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    private bool isWaveActive = false; // �j v�ltoz� a hull�m �llapot�nak kezel�s�re

    private Timer timer;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    private void Update()
    {
        // Csak akkor h�vjuk meg StartWave-et, ha m�g nem indult el a hull�m
        if (!isWaveActive && !timer.isSprite1Active)
        {
            StartCoroutine(StartWave());
        }

        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        // Ellens�g spawnol�s
        if (timeSinceLastSpawn >= (1f / enemiesPerSec) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        // Ha az �sszes ellens�g elpusztult, befejezz�k a hull�mot
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];  // V�letlenszer� ellens�g
        Instantiate(prefabToSpawn, LevelManager.main.StartPoint.position, Quaternion.identity);
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        isWaveActive = true;  // Be�ll�tjuk, hogy a hull�m akt�v
        yield return new WaitForSeconds(timeBetweenWaves);  // K�sleltet�s a hull�m el�tt
        isSpawning = true;  // Elind�tjuk a spawnol�st
        enemiesLeftToSpawn = EnemiesPerWave();  // Be�ll�tjuk a h�tral�v� ellens�geket
    }

    private void EndWave()
    {
        isSpawning = false;  // Le�ll�tjuk a spawnol�st
        timeSinceLastSpawn = 0f;  // Vissza�ll�tjuk az id�t
        currentWave++;  // Tov�bb l�p�nk a k�vetkez� hull�mra
        isWaveActive = false;  // A hull�m befejez�d�tt
    }

    private int EnemiesPerWave()
    {
        // Sk�l�zza a hull�mok sz�m�t a neh�zs�gi szint figyelembev�tel�vel
        return Mathf.RoundToInt(BaseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}