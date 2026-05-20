using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float arenaWidth = 8f;
    [SerializeField] private float arenaHeight = 5f;

    public void SpawnEnemy(int waveNumber)
    {
        Vector2 spawnPosition = GetRandomPerimeterPosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // Escalar stats según la oleada
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            int scaledHealth = Mathf.RoundToInt(30 * Mathf.Pow(1.1f, waveNumber - 1));
            enemyHealth.SetMaxHealth(scaledHealth);
        }

        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            float scaledSpeed = Mathf.Min(2f + (waveNumber - 1) * 0.1f, 5f);
            enemyMovement.SetSpeed(scaledSpeed);
        }
    }

    private Vector2 GetRandomPerimeterPosition()
    {
        int side = Random.Range(0, 4);
        float x, y;

        switch (side)
        {
            case 0: // arriba
                x = Random.Range(-arenaWidth, arenaWidth);
                y = arenaHeight;
                break;
            case 1: // abajo
                x = Random.Range(-arenaWidth, arenaWidth);
                y = -arenaHeight;
                break;
            case 2: // izquierda
                x = -arenaWidth;
                y = Random.Range(-arenaHeight, arenaHeight);
                break;
            default: // derecha
                x = arenaWidth;
                y = Random.Range(-arenaHeight, arenaHeight);
                break;
        }

        return new Vector2(x, y);
    }
}
