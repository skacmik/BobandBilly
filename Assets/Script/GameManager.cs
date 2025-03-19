using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Transform playerBigStart;
    public Transform playerSmallStart;
    public GameObject playerBig;
    public GameObject playerSmall;
    public GameObject loadingScreen; // Reference na UI panel s loading screenem

    public void KillPlayers()
    {
        // P�i�ti smrt do statistik
        PlayerPrefs.SetInt("deaths", PlayerPrefs.GetInt("deaths", 0) + 1);

        // Spustit restart levelu s loading screenem
        StartCoroutine(RestartLevel());
    }

    private IEnumerator RestartLevel()
    {
        // Aktivovat loading screen
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }

        yield return new WaitForSeconds(5f); // Po�kat 5 sekund na loading screen

        playerBig.transform.position = playerBigStart.position;
        playerSmall.transform.position = playerSmallStart.position;
        // Na��st aktu�ln� sc�nu znovu (resetuje cel� level)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}



