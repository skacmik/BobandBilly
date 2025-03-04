using UnityEngine;
using TMPro;  // ✅ Přidání podpory pro TextMeshPro
using System.Collections;

public class GameStats : MonoBehaviour
{
    public TMP_Text completedLevelsText;
    public TMP_Text playTimeText;
    public TMP_Text deathsText;

    private float playTime;

    private void Start()
    {
        UpdateStatsUI();
        StartCoroutine(UpdatePlayTime());  // ✅ Spuštění časovače hry
    }

    public static void AddCompletedLevel()
    {
        int completedLevels = PlayerPrefs.GetInt("CompletedLevels", 0);
        completedLevels++;
        PlayerPrefs.SetInt("CompletedLevels", completedLevels);
        PlayerPrefs.Save();
        Debug.Log("🏆 Level dokončen! Celkový počet dokončených levelů: " + completedLevels);
    }
private IEnumerator UpdatePlayTime()
    {
        while (true)
        {
            playTime += Time.deltaTime;
            PlayerPrefs.SetFloat("TotalPlayTime", playTime);
            yield return null;
        }
    }

    public void UpdateStatsUI()
    {
        int completedLevels = PlayerPrefs.GetInt("CompletedLevels", 0);
        float totalPlayTime = PlayerPrefs.GetFloat("TotalPlayTime", 0);
        int deaths = PlayerPrefs.GetInt("TotalDeaths", 0);

        completedLevelsText.text = "Dokončené levely: " + completedLevels;
        playTimeText.text = "Celkový čas hraní: " + FormatTime(totalPlayTime);
        deathsText.text = "Počet smrtí: " + deaths;
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return minutes + "m " + seconds + "s";
    } // ✅ Tato závorka tu nesměla chybět!
} // ✅ Hlavní závorka třídy
