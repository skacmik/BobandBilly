using UnityEngine;

public class TeleportBullet : MonoBehaviour
{
    private TeleportGun gun;
    private float speed;
    private float maxDistance;
    private Vector3 startPosition;
    private Transform shooter; // Hráč, který kulku vystřelil

    public void Initialize(TeleportGun gun, float speed, float maxDistance, Transform shooter)
    {
        this.gun = gun;
        this.speed = speed;
        this.maxDistance = maxDistance;
        this.shooter = shooter; // Uložíme střelce
        startPosition = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            gun.ReleasePlayer(transform.position);
            Debug.Log("🏁 Kulka dosáhla max vzdálenosti a teleportuje hráče!");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignorovat střelce (hráč se nemůže sám vtáhnout do kulky)
        if (collision.transform == shooter)
        {
            Debug.Log("🚫 Kulka ignorovala střelce: " + shooter.name);
            return;
        }

        if (collision.CompareTag("PlayerSmall") || collision.CompareTag("PlayerBig"))
        {
            gun.StorePlayer(collision.transform);
            Debug.Log("✅ Hráč " + collision.name + " byl vtáhnut do zbraně!");
            Destroy(gameObject);
        }
    }
}
