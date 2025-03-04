using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private Transform playerWithKey = null;
    private bool keyCollected = false;
    public float followSpeed = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("PlayerBig") || other.CompareTag("PlayerSmall")) && !keyCollected)
        {
            keyCollected = true;
            playerWithKey = other.transform;
            Debug.Log("🔑 Klíč sebral hráč: " + other.name);

            // Přidání klíče jako podobjekt hráče
            transform.SetParent(playerWithKey);
        }
    }

    private void Update()
    {
        if (keyCollected && playerWithKey != null)
        {
           
            transform.position = Vector3.Lerp(transform.position, playerWithKey.position + new Vector3(0, 1f, 0), followSpeed * Time.deltaTime);
        }
    }

    public Transform GetPlayerWithKey()
    {
        return playerWithKey;
    }
}
