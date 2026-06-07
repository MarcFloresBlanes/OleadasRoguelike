using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private ClaseSO clase;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = clase != null ? clase.velocidadBase : 5f;

        // Aplicar sprite de la clase
        if (clase != null && clase.spritePersonaje != null)
        {
            GetComponent<SpriteRenderer>().sprite = clase.spritePersonaje;
        }
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
