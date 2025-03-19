using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    public GameObject gunPrefab; // Prefab zbraně
    private bool gunSpawned = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gunSpawned && collision.CompareTag("PlayerBig"))
        {
            // Spawn zbraně jako dítě hráče
            GameObject gun = Instantiate(gunPrefab, collision.transform);
            gun.transform.localPosition = new Vector3(0.5f, 0.2f, 0); // Offset zbraně
            gunSpawned = true;

            Debug.Log("🔫 Zbraň přidána k " + collision.name);
        }
    }
}
