using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    public Transform playerBig;
    public Transform playerSmall;
    public float moveSpeed = 5f;
    private Vector3 targetPosition;

    private void Start()
    {
        if (playerBig != null && playerSmall != null)
        {
            SpriteRenderer spriteBig = playerBig.GetComponent<SpriteRenderer>();
            SpriteRenderer spriteSmall = playerSmall.GetComponent<SpriteRenderer>();

            if (spriteBig != null && spriteSmall != null)
            {
                spriteBig.enabled = true;
                spriteSmall.enabled = true;
                Debug.Log("👀 Postavy jsou nyní viditelné.");
            }
            else
            {
                Debug.LogError("⚠️ Jeden z hráčů nemá SpriteRenderer!");
            }
        }
        else
        {
            Debug.LogError("⚠️ Odkazy na hráče nejsou nastaveny v CharacterMovement!");
        }
    }

    private void Awake()
    {
        if (FindObjectsOfType<CharacterMovement>().Length > 1)
        {
            Debug.Log("⚠️ Duplikace postavy! Tuto instanci mažu.");
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("✅ Postavy se nyní nepřepnou mezi scénami.");
        }
    }

    public void MoveCharacters(int levelIndex)
    {
        // ✅ Najde správné tlačítko levelu a nastaví cílovou pozici
        GameObject levelButton = GameObject.Find("Level" + levelIndex);
        if (levelButton != null)
        {
            targetPosition = levelButton.transform.position;
            Debug.Log("🎮 Postavy jdou k levelu " + levelIndex);
            StartCoroutine(MoveToTarget());
        }
        else
        {
            Debug.LogError("⚠️ Tlačítko levelu " + levelIndex + " nebylo nalezeno!");
        }
    }

    private IEnumerator MoveToTarget()
    {
        if (playerBig == null || playerSmall == null)
        {
            Debug.LogError("⚠️ Hráči nejsou správně přiřazeni, pohyb nebude fungovat!");
            yield break;
        }

        while (Vector3.Distance(playerBig.position, targetPosition) > 0.1f)
        {
            if (playerBig != null)
                playerBig.position = Vector3.MoveTowards(playerBig.position, targetPosition, moveSpeed * Time.deltaTime);
            if (playerSmall != null)
                playerSmall.position = Vector3.MoveTowards(playerSmall.position, targetPosition, moveSpeed * Time.deltaTime);

            yield return null;
        }

        Debug.Log("✅ Postavy dorazily na místo.");
    }
}
