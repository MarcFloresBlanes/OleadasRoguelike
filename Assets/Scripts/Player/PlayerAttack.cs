using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float attackCooldown = 0.4f;
    [SerializeField] private float projectileOffset = 0.5f;

    private float lastAttackTime = 0f;

    void Update()
    {
        if (Time.time < lastAttackTime + attackCooldown) return;

        if (Input.GetKey(KeyCode.UpArrow))
            Shoot(Vector2.up);
        else if (Input.GetKey(KeyCode.DownArrow))
            Shoot(Vector2.down);
        else if (Input.GetKey(KeyCode.LeftArrow))
            Shoot(Vector2.left);
        else if (Input.GetKey(KeyCode.RightArrow))
            Shoot(Vector2.right);
    }

    private void Shoot(Vector2 direction)
    {
        lastAttackTime = Time.time;

        Vector2 spawnPosition = (Vector2)transform.position + direction * projectileOffset;
        GameObject projectileGO = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        Projectile projectile = projectileGO.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.SetDirection(direction);
        }
    }
}
