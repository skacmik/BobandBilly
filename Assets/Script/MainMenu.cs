using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject statisticsPanel; // Přetáhni do Inspectoru

    public void PlayGame()
    {
        SceneManager.LoadScene("Levels");
    }
    public void OpenSettings()
    {
        Debug.Log("📌 Otevření nastavení (zatím neimplementováno)");
        // Zde můžeš načíst scénu s nastavením, pokud ji vytvoříš
        // SceneManager.LoadScene("Settings");
    }
    public void OpenStatistics()
    {
        if (statisticsPanel != null)
        {
            statisticsPanel.SetActive(true);
            Debug.Log("📊 Statistiky otevřeny.");
        }
        else
        {
            Debug.LogError("⚠️ Statistiky panel není nastaven v Inspectoru!");
        }
    }

    public void CloseStatistics()
    {
        if (statisticsPanel != null)
        {
            statisticsPanel.SetActive(false);
            Debug.Log("📊 Statistiky zavřeny.");
        }
    }

    public void QuitGame()
    {
        Debug.Log("👋 Hra se ukončuje.");
        Application.Quit();
    }
}
