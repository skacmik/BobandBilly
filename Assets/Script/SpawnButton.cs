using UnityEngine;

public class SpawnButton : MonoBehaviour
{
    public GameObject pushableObjectPrefab; // Prefab objektu, kter� bude spawnov�n
    public Transform spawnPoint; // M�sto, kde se objekt objev�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kontrola, jestli hr�� interaguje s tla��tkem
        if(collision.CompareTag("PlayerBig") || collision.CompareTag("PlayerSmall"))
        {
            SpawnObject(); // Zavol�n� metody na spawn
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
            Debug.LogError("PushableObjectPrefab nebo SpawnPoint nen� nastaven!");
        }
    }
}
