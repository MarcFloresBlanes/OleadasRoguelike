using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;  // velocidad ajustable desde el Inspector

    private Rigidbody2D rb;
    private Vector2 moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = 0f;
        if (Input.GetKey(KeyCode.A)) moveInput.x = -1f;
        if (Input.GetKey(KeyCode.D)) moveInput.x = 1f;

        moveInput.y = 0f;
        if (Input.GetKey(KeyCode.S)) moveInput.y = -1f;
        if (Input.GetKey(KeyCode.W)) moveInput.y = 1f;

        // Normalizar para que en diagonal no vaya más rápido
        moveInput = moveInput.normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }
}
