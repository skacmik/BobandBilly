using UnityEngine;

public class PushableObject : MonoBehaviour
{
    public float pushForce = 10f; // Síla, kterou velký hráè tlaèí objekt
    private Rigidbody2D rb;
    private bool isBeingPushed = false;
    private Transform smallPlayerOnTop = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Zabrání rotaci
        rb.drag = 5f; // Vìtší odpor pro eliminaci dokluzování
    }

    private void Update()
    {
        // Pokud je malý hráè nahoøe, nastavíme ho jako dítì pushovatelného objektu
        if (smallPlayerOnTop != null)
        {
            smallPlayerOnTop.position += (Vector3)rb.velocity * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (!isBeingPushed)
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // Okamžité zastavení pøi uvolnìní
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
            // Kontrola, zda malý hráè stojí NA objektu, ne vedle
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
