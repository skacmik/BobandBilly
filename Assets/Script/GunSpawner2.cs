using UnityEngine;

public class GunSpawner2 : MonoBehaviour
{
    public GameObject gunPrefab; // Prefab zbraně
    private bool bigPlayerGunSpawned = false; // Zda už velký hráč má zbraň
    private bool smallPlayerGunSpawned = false; // Zda už malý hráč má zbraň

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBig") && !bigPlayerGunSpawned)
        {
            SpawnGun(collision.transform);
            bigPlayerGunSpawned = true;
        }
        else if (collision.CompareTag("PlayerSmall") && !smallPlayerGunSpawned)
        {
            SpawnGun(collision.transform);
            smallPlayerGunSpawned = true;
        }
    }

    private void SpawnGun(Transform player)
    {
        if (player == null)
        {
            Debug.LogError("❌ Chyba: Hráč nebyl nalezen při spawnování zbraně!");
            return;
        }

        // Spawn zbraně jako dítě hráče
        GameObject gun = Instantiate(gunPrefab, player);
        gun.transform.localPosition = new Vector3(0.5f, 0.2f, 0); // Offset zbraně
        gun.transform.localScale = new Vector3(0.3f, 0.3f, 1); // Oprava velikosti


        // ✅ Ošetření chybné pozice nebo velikosti
        if (float.IsInfinity(gun.transform.localPosition.x) || float.IsNaN(gun.transform.localPosition.x))
        {
            Debug.LogWarning("⚠️ Pozice zbraně byla nesprávná, resetuji na (0,0,0)");
            gun.transform.localPosition = Vector3.zero;
        }
        if (float.IsInfinity(gun.transform.localScale.x) || float.IsNaN(gun.transform.localScale.x))
        {
            Debug.LogWarning("⚠️ Měřítko zbraně bylo nesprávné, resetuji na (1,1,1)");
            gun.transform.localScale = Vector3.one;
        }

        Debug.Log("🔫 Zbraň přidána k " + player.name);
    }
}
