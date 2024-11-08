using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 2f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent OnEnemyDestroy;
    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;

    private void Start() {
        StartCoroutine(StartWave());
    }

    private void Awake() {
        OnEnemyDestroy = new UnityEvent();
        OnEnemyDestroy.AddListener(OnEnemyDestroyed);
    }

    private void OnEnemyDestroyed() {
        enemiesAlive--;
    }

    private void Update() {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;
        Debug.Log("timeSinceLastSpawn :" + timeSinceLastSpawn);
        Debug.Log("Threshold :" + (1f / enemiesPerSecond));
        if (timeSinceLastSpawn >= 1f / enemiesPerSecond && enemiesLeftToSpawn > 0) {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0;
        }

        if (enemiesAlive == 0&& enemiesLeftToSpawn == 0) {
            EndWave();
        }
    }

    private void EndWave() {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    private void SpawnEnemy() {
        // Debug.Log("Spawning enemy");
        // Debug.Log("Enemies Alive: " + enemiesAlive);
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(enemyPrefab, LevelManager.main.startPoint.position, Quaternion.identity);
    }

    private IEnumerator StartWave() {
        Debug.Log("Starting coiuroutine");
        yield return new WaitForSeconds(timeBetweenWaves);
        Debug.Log("Starting coiuroutine2");
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private int EnemiesPerWave() {
        return (int)(baseEnemies * Mathf.Pow(difficultyScalingFactor, currentWave));
    }
}
