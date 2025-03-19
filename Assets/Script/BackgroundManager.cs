using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    public SpriteRenderer backgroundRenderer;
    public Sprite[] levelBackgrounds; // Pole s obrázky pro každý level

    private void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("SelectedLevel", 1) - 1; // Získání aktuálního levelu
        if (currentLevel >= 0 && currentLevel < levelBackgrounds.Length)
        {
            backgroundRenderer.sprite = levelBackgrounds[currentLevel];
        }
        else
        {
            Debug.LogWarning("❌ Chybí pozadí pro tento level!");
        }
    }
}
