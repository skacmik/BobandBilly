using UnityEngine;
using UnityEngine.UI;

public class LevelUnlock : MonoBehaviour
{
    public Button[] levelButtons; // Seznam tlačítek levelů

    private void Start()
    {
        // ✅ Ověření, zda jsou tlačítka správně přiřazena
        if (levelButtons == null || levelButtons.Length == 0)
        {
            Debug.LogError("⚠️ Chyba: Pole `levelButtons` není přiřazené! Přidej tlačítka v Inspektoru.");
            return; // Zabrání pádu hry
        }

        int unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1); // Výchozí je Level 1

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (levelButtons[i] == null)
            {
                Debug.LogError($"⚠️ Chyba: Tlačítko levelu {i} není přiřazené v Inspektoru!");
                continue; // Přeskočíme tento level, aby hra nespadla
            }

            if (i < unlockedLevels)
            {
                levelButtons[i].interactable = true; // Odemčené levely
            }
            else
            {
                levelButtons[i].interactable = false; // Zamčené levely
                Text buttonText = levelButtons[i].GetComponentInChildren<Text>();
                if (buttonText != null)
                {
                    buttonText.text = "🔒"; // Zámek na tlačítku
                }
            }
        }

        Debug.Log("✅ Levely načteny správně. Aktuálně odemčené: " + unlockedLevels);
    }

    // ✅ Přidáváme metodu UnlockNextLevel, aby ji mohl Door.cs použít
    public static void UnlockNextLevel(int levelIndex)
    {
        int currentUnlocked = PlayerPrefs.GetInt("UnlockedLevels", 1);
        if (levelIndex >= currentUnlocked)
        {
            PlayerPrefs.SetInt("UnlockedLevels", levelIndex + 1);
            PlayerPrefs.Save();
        }
    }
}
