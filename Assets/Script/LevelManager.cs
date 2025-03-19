using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu"); // Ujisti se, že scéna hlavního menu se jmenuje "Menu"
    }
}
