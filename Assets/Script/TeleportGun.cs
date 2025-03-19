using UnityEngine;
using System.Collections;

public class TeleportGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float maxRange = 5f;
    public float cooldownTime = 2f;

    private bool canShoot = true;
    private Transform storedPlayer = null; // Hráč, který se teleportuje
    private int facingDirection = 1; // 1 = doprava, -1 = doleva

    void Update()
    {
        if (firePoint == null)
        {
            Debug.LogError("❌ FirePoint není přiřazen ke zbrani: " + gameObject.name);
            return;
        }

        // Kontrola směru podle rotace hráče
        if (transform.parent.localScale.x < 0)
            facingDirection = -1;
        else
            facingDirection = 1;

        if (transform.parent.CompareTag("PlayerBig") && Input.GetKeyDown(KeyCode.Q) && canShoot)
        {
            Shoot();
        }
        if (transform.parent.CompareTag("PlayerSmall") && Input.GetKeyDown(KeyCode.Keypad2) && canShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        StartCoroutine(ShootCooldown());

        // Vytvoření kulky a otočení podle směru
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.transform.localScale = new Vector3(facingDirection, 1, 1);

        TeleportBullet bulletScript = bullet.GetComponent<TeleportBullet>();

        if (bulletScript != null)
        {
            bulletScript.Initialize(this, bulletSpeed * facingDirection, maxRange, transform.parent);
            Debug.Log("💥 Kulka vystřelena z " + firePoint.position + " ve směru " + facingDirection);
        }
        else
        {
            Debug.LogError("❌ Kulka nemá připojený skript TeleportBullet!");
        }
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }

    public void StorePlayer(Transform player)
    {
        storedPlayer = player;
        player.gameObject.SetActive(false);
        Debug.Log("🎯 Hráč " + player.name + " byl zachycen do zbraně!");
    }

    public void ReleasePlayer(Vector3 position)
    {
        if (storedPlayer != null)
        {
            storedPlayer.position = position;
            storedPlayer.gameObject.SetActive(true);
            storedPlayer = null;
            Debug.Log("🚀 Hráč byl vystřelen na " + position);
        }
    }
}
