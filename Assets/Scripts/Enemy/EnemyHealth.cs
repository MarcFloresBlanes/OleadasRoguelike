using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 30;
    private int currentHealth;
    private bool healthInitialized = false;

    void Start()
    {
        if (!healthInitialized)
        {
            currentHealth = maxHealth;
        }
    }

    public void SetMaxHealth(int health)
    {
        maxHealth = health;
        currentHealth = health;
        healthInitialized = true;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Vida antes: " + currentHealth + " | Daño: " + damage);
        currentHealth -= damage;
        Debug.Log("Vida después: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " ha muerto");
        Destroy(gameObject);
    }
}
