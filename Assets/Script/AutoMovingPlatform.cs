using UnityEngine;

public class AutoMovingPlatform : MonoBehaviour
{
    public float moveSpeed = 2f; // Rychlost pohybu plošiny
    public float moveDistance = 5f; // Jak daleko se plošina pohybuje nahoru a dolù

    private Vector3 initialPosition; // Výchozí pozice plošiny
    private Vector3 targetPosition; // Cílová pozice plošiny
    private bool movingUp = true; // Sleduje, zda plošina jede nahoru

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
        // Když hráè vstoupí na plošinu, pøiøaï ho jako dítì plošiny
        if (collision.gameObject.CompareTag("PlayerBig") || collision.gameObject.CompareTag("PlayerSmall"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Když hráè opustí plošinu, odstraò ho z hierarchie plošiny
        if (collision.gameObject.CompareTag("PlayerBig") || collision.gameObject.CompareTag("PlayerSmall"))
        {
            collision.transform.SetParent(null);
        }
    }
}
