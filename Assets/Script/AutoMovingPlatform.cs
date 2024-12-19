using UnityEngine;

public class AutoMovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f; // Rychlost pohybu plo�iny
    public float moveDistance = 5f; // Jak daleko se plo�ina pohybuje nahoru a dol�

    private Vector3 initialPosition; // V�choz� pozice plo�iny
    private Vector3 targetPosition; // C�lov� pozice plo�iny
    private bool movingUp = true; // Sleduje, zda plo�ina jede nahoru

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + new Vector3(0, moveDistance, 0);
    }

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        if (movingUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, initialPosition) < 0.01f)
            {
                movingUp = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kdy� hr�� vstoup� na plo�inu, p�i�a� ho jako d�t� plo�iny
        if (collision.gameObject.CompareTag("PlayerBig") || collision.gameObject.CompareTag("PlayerSmall"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Kdy� hr�� opust� plo�inu, odstra� ho z hierarchie plo�iny
        if (collision.gameObject.CompareTag("PlayerBig") || collision.gameObject.CompareTag("PlayerSmall"))
        {
            collision.transform.SetParent(null);
        }
    }
}
