using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement instance;
    public Transform playerBig;
    public Transform playerSmall;
    public float moveSpeed = 5f;
    private Vector3 targetPosition;
    private bool movingToLevel = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Levels")
        {
            LoadCharacterPosition();
        }
    }

    private void LoadCharacterPosition()
    {
        if (playerBig == null || playerSmall == null)
        {
            Debug.LogError("❌ Postavy nejsou správně nastavené! Připoj je zpět.");
            return;
        }

        if (PlayerPrefs.HasKey("LastPosBig_X"))
        {
            float bigX = PlayerPrefs.GetFloat("LastPosBig_X");
            float bigY = PlayerPrefs.GetFloat("LastPosBig_Y");
            float smallX = PlayerPrefs.GetFloat("LastPosSmall_X");
            float smallY = PlayerPrefs.GetFloat("LastPosSmall_Y");

            playerBig.position = new Vector3(bigX, bigY, 0);
            playerSmall.position = new Vector3(smallX, smallY, 0);
            Debug.Log("✅ Postavy obnoveny na poslední uloženou pozici.");
        }
        else
        {
            ResetCharacterPosition();
        }
    }

    private void ResetCharacterPosition()
    {
        playerBig.position = new Vector3(-7, 4, 0);
        playerSmall.position = new Vector3(-5, 4, 0);
        Debug.Log("🔄 Postavy resetovány na startovní pozici.");
    }

    public void SelectLevel(int levelIndex)
    {
        GameObject levelButton = GameObject.Find("Level" + levelIndex);
        if (levelButton != null)
        {
            targetPosition = levelButton.transform.position;
            StartCoroutine(MoveToLevel(levelIndex));
        }
        else
        {
            Debug.LogError("Tlačítko levelu " + levelIndex + " nebylo nalezeno!");
        }
    }

    private IEnumerator MoveToLevel(int levelIndex)
    {
        movingToLevel = true;
        Debug.Log("🚶‍♂️ Postavy se pohybují k levelu " + levelIndex);

        // ✅ Zajistí, že se pohyb neukončí, dokud postavy skutečně nedorazí
        while (true)
        {
            float bigDistance = Vector3.Distance(playerBig.position, targetPosition);
            float smallDistance = Vector3.Distance(playerSmall.position, targetPosition);

            if (bigDistance <= 0.1f && smallDistance <= 0.1f)
            {
                break; // ✅ Obě postavy jsou dostatečně blízko → ukončíme smyčku
            }

            playerBig.position = Vector3.MoveTowards(playerBig.position, targetPosition, moveSpeed * Time.deltaTime);
            playerSmall.position = Vector3.MoveTowards(playerSmall.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        Debug.Log("✅ Postavy dorazily na tlačítko levelu " + levelIndex);

        // ✅ Počkej 1 sekundu, aby bylo vidět, že dorazily
        yield return new WaitForSeconds(1f);

        // ✅ Uložíme pozici hráčů
        PlayerPrefs.SetFloat("LastPosBig_X", playerBig.position.x);
        PlayerPrefs.SetFloat("LastPosBig_Y", playerBig.position.y);
        PlayerPrefs.SetFloat("LastPosSmall_X", playerSmall.position.x);
        PlayerPrefs.SetFloat("LastPosSmall_Y", playerSmall.position.y);
        PlayerPrefs.Save();

        movingToLevel = false;

        // ✅ Počkej 0.5 sekundy, aby nebylo načtení příliš náhlé
        yield return new WaitForSeconds(0.5f);

        // ✅ Načti level
        SceneManager.LoadScene("Level" + levelIndex);
    }
}
