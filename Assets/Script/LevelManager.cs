using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Menu"); // Ujisti se, �e sc�na hlavn�ho menu se jmenuje "Menu"
    }
}
