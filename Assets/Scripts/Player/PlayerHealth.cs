using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private ClaseSO clase;
    [SerializeField] private float invincibilityDuration = 0.5f;

    private int maxHealth;
    private int currentHealth;
    private bool isInvincible = false;

    void Start()
    {
        maxHealth = clase != null ? clase.vidaBase : 100;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        StartCoroutine(InvincibilityCoroutine());
    }

    private System.Collections.IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    private void Die()
    {
        GameOverManager gameOverManager = FindAnyObjectByType<GameOverManager>();
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }
        gameObject.SetActive(false);
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
}
