using UnityEngine;

public class MovingLaser : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 3f;
    public LayerMask shieldLayer; // Vrstva štítu

    private bool movingToB = true;
    private Vector3 originalScale;
    private GameManager gameManager; // Odkaz na GameManager

    private void Start()
    {
        originalScale = transform.localScale;

        // Najdi GameManager ve scéně
        gameManager = FindObjectOfType<GameManager>();

        if (gameManager == null)
        {
            Debug.LogError("❌ GameManager nebyl nalezen ve scéně!");
        }
    }

    void Update()
    {
        MoveBetweenPoints();
        AdjustLaserLength();
    }

    private void MoveBetweenPoints()
    {
        if (movingToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, speed * Time.deltaTime);
            if (transform.position == pointB.position) movingToB = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, speed * Time.deltaTime);
            if (transform.position == pointA.position) movingToB = true;
        }
    }

    private void AdjustLaserLength()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, originalScale.x, shieldLayer);

        if (hit.collider != null)
        {
            float newLength = hit.distance;
            transform.localScale = new Vector3(newLength, originalScale.y, originalScale.z);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * 5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBig") || other.CompareTag("PlayerSmall"))
        {
            Debug.Log("🔥 " + other.gameObject.name + " byl zasažen pohybujícím se laserem!");

            // Zavolej resetování levelu přes GameManager
            if (gameManager != null)
            {
                gameManager.KillPlayers();
            }
            else
            {
                Debug.LogError("❌ GameManager není přiřazen! Nelze restartovat level.");
            }
        }
    }
}
