using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{
    public void SelectLevelButton(int levelIndex)
    {
        // ✅ Uloží vybraný level do PlayerPrefs
        PlayerPrefs.SetInt("SelectedLevel", levelIndex);
        PlayerPrefs.Save();

        // ✅ Pohne postavičkami ke zvolenému levelu
        FindObjectOfType<CharacterMovement>().SelectLevel(levelIndex);

        // ✅ Načte scénu s daným levelem
        string levelName = "Level" + levelIndex;
        Debug.Log("📌 Načítání levelu: " + levelName);
        SceneManager.LoadScene(levelName);
    }
}

