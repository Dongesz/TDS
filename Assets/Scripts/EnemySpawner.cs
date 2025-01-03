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
    private bool isWaveActive = false; // Új változó a hullám állapotának kezelésére

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
        // Csak akkor hívjuk meg StartWave-et, ha még nem indult el a hullám
        if (!isWaveActive && !timer.isSprite1Active)
        {
            StartCoroutine(StartWave());
        }

        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        // Ellenség spawnolás
        if (timeSinceLastSpawn >= (1f / enemiesPerSec) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        // Ha az összes ellenség elpusztult, befejezzük a hullámot
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];  // Véletlenszerû ellenség
        Instantiate(prefabToSpawn, LevelManager.main.StartPoint.position, Quaternion.identity);
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        isWaveActive = true;  // Beállítjuk, hogy a hullám aktív
        yield return new WaitForSeconds(timeBetweenWaves);  // Késleltetés a hullám elõtt
        isSpawning = true;  // Elindítjuk a spawnolást
        enemiesLeftToSpawn = EnemiesPerWave();  // Beállítjuk a hátralévõ ellenségeket
    }

    private void EndWave()
    {
        isSpawning = false;  // Leállítjuk a spawnolást
        timeSinceLastSpawn = 0f;  // Visszaállítjuk az idõt
        currentWave++;  // Tovább lépünk a következõ hullámra
        isWaveActive = false;  // A hullám befejezõdött
    }

    private int EnemiesPerWave()
    {
        // Skálázza a hullámok számát a nehézségi szint figyelembevételével
        return Mathf.RoundToInt(BaseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}