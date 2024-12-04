using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    public GameObject pushableObjectPrefab; // Prefab objektu, který bude spawnován
    public Transform spawnPoint; // Místo, kde se objekt objeví

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kontrola, jestli hráè interaguje s tlaèítkem
        if(collision.CompareTag("PlayerBig") || collision.CompareTag("PlayerSmall"))
        {
            SpawnObject(); // Zavolání metody na spawn
        }
    }

    private void SpawnObject()
    {
        if (pushableObjectPrefab != null && spawnPoint != null)
        {
            Instantiate(pushableObjectPrefab, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("PushableObjectPrefab nebo SpawnPoint není nastaven!");
        }
    }
}
