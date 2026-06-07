using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private TextMeshProUGUI textOleadaFinal;
    [SerializeField] private WaveManager waveManager;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        HidePanel();
    }

    private void HidePanel()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void ShowPanel()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void ShowGameOver()
    {
        // Pausar el juego
        Time.timeScale = 0f;
        ShowPanel();

        // Mostrar el panel
        gameObject.SetActive(true);

        // Mostrar oleada alcanzada
        if (textOleadaFinal != null && waveManager != null)
        {
            textOleadaFinal.text = "Llegaste a la oleada " + waveManager.GetCurrentWave();
        }
    }

    public void OnReintentarPressed()
    {
        // Restaurar tiempo antes de recargar
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnMenuPressed()
    {
        // De momento no hace nada, no tenemos menú aún
        Time.timeScale = 1f;
        Debug.Log("Menú principal: pendiente de implementar");
    }
}
