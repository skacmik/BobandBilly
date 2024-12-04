using UnityEngine;

public class DespawnAndSpawnButton : MonoBehaviour
{
    public GameObject objectToSpawn; // Prefab objektu, kter� bude spawnov�n
    public Transform spawnPoint; // M�sto, kde se objekt objev�
    public GameObject currentObject; // Odkaz na aktu�ln� existuj�c� objekt


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kontrola, jestli hr�� interaguje s tla��tkem
        if (collision.CompareTag("PlayerBig") || collision.CompareTag("PlayerSmall"))
        {
            DespawnAndRespawn();
        }
    }

    private void DespawnAndRespawn()
    {
        // Despawn aktu�ln�ho objektu
        if (currentObject != null)
        {
            Destroy(currentObject); // Zni�� aktu�ln� objekt
        }

        // Spawn nov�ho objektu
        if (objectToSpawn != null && spawnPoint != null)
        {
            currentObject = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab nebo SpawnPoint nen� nastaven!");
        }
    }
}
