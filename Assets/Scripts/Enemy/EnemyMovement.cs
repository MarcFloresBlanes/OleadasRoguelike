using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    private Transform target;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("EnemyMovement: No se encontró el jugador con Tag 'Player'");
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector2 direccion = (target.position - transform.position).normalized;
        rb.linearVelocity = direccion * speed;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
