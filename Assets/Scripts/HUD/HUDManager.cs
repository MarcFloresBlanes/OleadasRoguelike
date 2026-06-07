using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private Image barraVidaRelleno;
    [SerializeField] private TextMeshProUGUI textVida; // opcional

    [Header("Oleada y Timer")]
    [SerializeField] private TextMeshProUGUI textOleada;
    [SerializeField] private TextMeshProUGUI textTimer;

    [Header("Referencias")]
    [SerializeField] private WaveManager waveManager;

    private PlayerHealth playerHealth;

    void Start()
    {
        // Buscar al jugador por tag
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogWarning("HUDManager: No se encontró el jugador");
        }
    }

    void Update()
    {
        ActualizarVida();
        ActualizarOleadaYTimer();
    }

    private void ActualizarVida()
    {
        if (playerHealth == null) return;

        float porcentaje = (float)playerHealth.GetCurrentHealth() / playerHealth.GetMaxHealth();
        barraVidaRelleno.fillAmount = porcentaje;

        // Cambiar color según vida restante
        if (porcentaje > 0.5f)
            barraVidaRelleno.color = Color.green;
        else if (porcentaje > 0.25f)
            barraVidaRelleno.color = Color.yellow;
        else
            barraVidaRelleno.color = Color.red;

        // Actualizar texto de vida (opcional)
        if (textVida != null)
            textVida.text = playerHealth.GetCurrentHealth() + "/" + playerHealth.GetMaxHealth();
    }

    private void ActualizarOleadaYTimer()
    {
        if (waveManager == null) return;

        textOleada.text = "Oleada " + waveManager.GetCurrentWave();

        float timer = waveManager.GetWaveTimer();
        textTimer.text = Mathf.CeilToInt(timer) + "s";
    }
}
