using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public Sprite openDoorSprite; // Sprite pro otevřené dveře
    private SpriteRenderer spriteRenderer;
    private bool doorUnlocked = false;
    private int playersAtDoor = 0;

    private Collider2D playerBigAtDoor;
    private Collider2D playerSmallAtDoor;
    private KeyPickup key; // Odkaz na klíč

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        key = FindObjectOfType<KeyPickup>(); // Najdeme klíč ve scéně
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBig") || other.CompareTag("PlayerSmall"))
        {
            if (PlayerHasKey(other))
            {
                doorUnlocked = true;
                spriteRenderer.sprite = openDoorSprite; // Změna textury dveří
                DestroyKey(); // Zničíme klíč
            }

            // Uložíme hráče, kteří jsou u dveří
            if (other.CompareTag("PlayerBig"))
                playerBigAtDoor = other;
            if (other.CompareTag("PlayerSmall"))
                playerSmallAtDoor = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBig"))
        {
            playerBigAtDoor = null;
        }
        if (other.CompareTag("PlayerSmall"))
        {
            playerSmallAtDoor = null;
        }
    }

    private void Update()
    {
        if (!doorUnlocked) return; // Pokud nejsou dveře odemčené, nedělej nic

        // Pokud je hráč u dveří a zmáčkne klávesu, vstoupí nebo vyjde
        if (playerBigAtDoor != null && Input.GetKeyDown(KeyCode.E))
        {
            TogglePlayerState(playerBigAtDoor);
        }
        if (playerSmallAtDoor != null && Input.GetKeyDown(KeyCode.Keypad1))
        {
            TogglePlayerState(playerSmallAtDoor);
        }
    }

    private void TogglePlayerState(Collider2D player)
    {
        SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
        bool entering = playerSprite.enabled; // Pokud je viditelný, znamená to, že vstupuje

        playerSprite.enabled = !entering; // Přepne viditelnost hráče
        playersAtDoor += entering ? 1 : -1; // Přičte/odečte hráče od dveří

        Debug.Log(player.name + (entering ? " VEŠEL do dveří" : " ODEŠEL ze dveří") + " (" + playersAtDoor + "/2)");

        if (playersAtDoor >= 2)
        {
            CompleteLevel();
        }
    }

    private bool PlayerHasKey(Collider2D player)
    {
        return key != null && key.GetPlayerWithKey() == player.transform;
    }

    private void DestroyKey()
    {
        if (key != null)
        {
            Debug.Log("🔑 Klíč byl použit a zmizel!");
            Destroy(key.gameObject); // Zničíme klíč
        }
    }

    private void CompleteLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("SelectedLevel");
        LevelUnlock.UnlockNextLevel(currentLevel);
        GameStats.AddCompletedLevel();
        SceneManager.LoadScene("Levels");
    }
}
