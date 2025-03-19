using UnityEngine;
using TMPro;  // ✅ Podpora pro TextMeshPro
using System.Collections;

public class GameStats : MonoBehaviour
{
    public TMP_Text completedLevelsText;
    public TMP_Text playTimeText;
    public TMP_Text deathsText;

    private float playTime;
    private bool isTrackingTime = true;

    private void Start()
    {
        // ✅ Načte čas při startu
        playTime = PlayerPrefs.GetFloat("TotalPlayTime", 0);
        UpdateStatsUI();
        StartCoroutine(UpdatePlayTime());  // ✅ Spuštění časovače
    }

    private IEnumerator UpdatePlayTime()
    {
        while (isTrackingTime)  // ✅ Ujistíme se, že čas běží i mezi scénami
        {
            playTime += Time.deltaTime;
            PlayerPrefs.SetFloat("TotalPlayTime", playTime);
            PlayerPrefs.Save();
            yield return null;
        }
    }

    public static void AddCompletedLevel()
    {
        int completedLevels = PlayerPrefs.GetInt("CompletedLevels", 0);
        completedLevels++;
        PlayerPrefs.SetInt("CompletedLevels", completedLevels);
        PlayerPrefs.Save();
        Debug.Log("🏆 Level dokončen! Celkový počet dokončených levelů: " + completedLevels);
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
    }
}
