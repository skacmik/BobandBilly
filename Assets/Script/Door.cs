using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private bool doorUnlocked = false;
    private int playersAtDoor = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("🚪 Hráč vstoupil do dveří: " + other.name);

        if (other.CompareTag("PlayerBig") || other.CompareTag("PlayerSmall"))
        {
            if (PlayerHasKey(other))
            {
                doorUnlocked = true;
                Debug.Log("🔑 Hráč s klíčem přišel ke dveřím! Dveře odemknuty.");
            }

            if (doorUnlocked)
            {
                playersAtDoor++;
                Debug.Log(other.name + " prošel dveřmi! (" + playersAtDoor + "/2)");

                if (playersAtDoor >= 2) // Oba hráči musí projít
                {
                    Debug.Log("✅ Level dokončen!");
                    CompleteLevel();
                }
            }
        }
    }

    private bool PlayerHasKey(Collider2D player)
    {
        KeyPickup key = FindObjectOfType<KeyPickup>();

        if (key == null)
        {
            Debug.Log("❌ Klíč nebyl nalezen ve scéně.");
            return false;
        }

        bool hasKey = key.GetPlayerWithKey() == player.transform;

        Debug.Log("🔍 Kontrola klíče: " + player.name + " má klíč? " + hasKey);
        return hasKey;
    }

    private void CompleteLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("SelectedLevel");

        // ✅ Zavoláme UnlockNextLevel, abychom odemkli další level
        LevelUnlock.UnlockNextLevel(currentLevel);

        // ✅ Načteme počet odemčených levelů a vypíšeme ho do konzole
        int unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels", 1);
        Debug.Log("🔓 Aktuálně odemčené levely: " + unlockedLevels);

        GameStats.AddCompletedLevel();

        // ✅ Přesměrování na výběr levelů
        SceneManager.LoadScene("Levels");
    }


}
