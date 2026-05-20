using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [Header("Configuración de oleadas")]
    [SerializeField] private float waveDuration = 30f;
    [SerializeField] private float timeBetweenSpawns = 2f;
    [SerializeField] private int baseEnemyCount = 5;

    [Header("Referencias")]
    [SerializeField] private EnemySpawner enemySpawner;

    private int currentWave = 0;
    private float waveTimer = 0f;
    private bool waveActive = false;
    private bool isSpawning = false;

    void Start()
    {
        StartNextWave();
    }

    void Update()
    {
        if (!waveActive) return;

        waveTimer -= Time.deltaTime;

        if (waveTimer <= 0)
        {
            EndWave();
        }
    }

    private void StartNextWave()
    {
        currentWave++;
        waveTimer = waveDuration;
        waveActive = true;

        Debug.Log("Oleada " + currentWave + " iniciada");

        if (!isSpawning)
        {
            StartCoroutine(SpawnEnemiesCoroutine());
        }
    }

    private IEnumerator SpawnEnemiesCoroutine()
    {
        isSpawning = true;

        // Tiempo entre spawns decrece con la oleada (más presión)
        float spawnInterval = Mathf.Max(timeBetweenSpawns - (currentWave * 0.1f), 0.5f);

        while (waveActive)
        {
            enemySpawner.SpawnEnemy(currentWave);
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }

    private void EndWave()
    {
        waveActive = false;
        StopAllCoroutines();
        isSpawning = false;

        // Destruir enemigos restantes
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        Debug.Log("Oleada " + currentWave + " terminada. Abriendo tienda...");

        // Aquí llamaremos al ShopManager cuando exista
        // Por ahora, esperamos 2s y arrancamos la siguiente oleada
        StartCoroutine(WaitAndStartNextWave());
    }

    private IEnumerator WaitAndStartNextWave()
    {
        yield return new WaitForSeconds(2f);
        StartNextWave();
    }

    // Método público para que la tienda llame cuando el jugador la cierra
    public void OnShopClosed()
    {
        StartNextWave();
    }

    // Getter para la UI
    public int GetCurrentWave() => currentWave;
    public float GetWaveTimer() => waveTimer;
}
