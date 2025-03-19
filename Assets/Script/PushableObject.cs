using UnityEngine;

public class PushableObject : MonoBehaviour
{
    public float pushForce = 10f; // S�la, kterou velk� hr�� tla�� objekt
    private Rigidbody2D rb;
    private bool isBeingPushed = false;
    private Transform smallPlayerOnTop = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Zabr�n� rotaci
        rb.drag = 5f; // V�t�� odpor pro eliminaci dokluzov�n�
    }

    private void Update()
    {
        // Pokud je mal� hr�� naho�e, nastav�me ho jako d�t� pushovateln�ho objektu
        if (smallPlayerOnTop != null)
        {
            smallPlayerOnTop.position += (Vector3)rb.velocity * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (!isBeingPushed)
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // Okam�it� zastaven� p�i uvoln�n�
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBig"))
        {
            isBeingPushed = true;
        }

        if (collision.gameObject.CompareTag("PlayerSmall"))
        {
            // Kontrola, zda mal� hr�� stoj� NA objektu, ne vedle
            if (collision.contacts[0].normal.y < -0.5f)
            {
                smallPlayerOnTop = collision.transform;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBig"))
        {
            isBeingPushed = false;
        }

        if (collision.gameObject.CompareTag("PlayerSmall") && smallPlayerOnTop == collision.transform)
        {
            smallPlayerOnTop = null;
        }
    }
}
