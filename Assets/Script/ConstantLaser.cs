using UnityEngine;

public class ConstantLaser : MonoBehaviour
{
    public Transform raycastOrigin; // GameObject odkud se vypouští raycast
    public LayerMask shieldLayer; // Vrstva štítu
    public LayerMask playerLayer; // Vrstva hráče
    public float maxLaserLength = 10f; // Maximální délka laseru

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private float originalScaleX;
    private GameManager gameManager; // Reference na GameManager

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalScaleX = transform.localScale.x;
        gameManager = FindObjectOfType<GameManager>(); // Najde GameManager ve scéně
    }

    private void Update()
    {
        AdjustLaserLength();
    }

    private void AdjustLaserLength()
    {
        // Raycast směřující doprava pro detekci štítu
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, transform.right, maxLaserLength, shieldLayer);

        if (hit.collider != null)
        {
            // Výpočet nové délky laseru
            float newLength = hit.distance / maxLaserLength;
            transform.localScale = new Vector3(originalScaleX * newLength, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // Pokud není štít, vrátí se laser do původní délky
            transform.localScale = new Vector3(originalScaleX, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Pokud hráč vstoupí do laseru
        if (other.CompareTag("PlayerSmall") || other.CompareTag("PlayerBig"))
        {
            // Raycast mezi laserem a hráčem pro kontrolu, zda je krytý štítem
            Vector2 directionToPlayer = other.transform.position - raycastOrigin.position;
            RaycastHit2D shieldCheck = Physics2D.Raycast(raycastOrigin.position, directionToPlayer, directionToPlayer.magnitude, shieldLayer);

            if (shieldCheck.collider == null)
            {
                Debug.Log(other.gameObject.name + " byl zasažen laserem!");
                gameManager.KillPlayers(); // Zavolá reset levelu místo zničení hráče
            }
            else
            {
                Debug.Log(other.gameObject.name + " byl chráněn štítem!");
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (raycastOrigin == null) return; // Ověří, že GameObject existuje

        Gizmos.color = Color.blue; // Barva pro vizualizaci raycastu
        Gizmos.DrawLine(raycastOrigin.position, raycastOrigin.position + transform.right * maxLaserLength);
    }
}
