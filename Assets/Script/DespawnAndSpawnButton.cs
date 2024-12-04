using UnityEngine;

public class DespawnAndSpawnButton : MonoBehaviour
{
    public GameObject objectToSpawn; // Prefab objektu, který bude spawnován
    public Transform spawnPoint; // Místo, kde se objekt objeví
    public GameObject currentObject; // Odkaz na aktuálnì existující objekt


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kontrola, jestli hráè interaguje s tlaèítkem
        if (collision.CompareTag("PlayerBig") || collision.CompareTag("PlayerSmall"))
        {
            DespawnAndRespawn();
        }
    }

    private void DespawnAndRespawn()
    {
        // Despawn aktuálního objektu
        if (currentObject != null)
        {
            Destroy(currentObject); // Znièí aktuální objekt
        }

        // Spawn nového objektu
        if (objectToSpawn != null && spawnPoint != null)
        {
            currentObject = Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Prefab nebo SpawnPoint není nastaven!");
        }
    }
}
