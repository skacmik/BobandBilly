using UnityEngine;

public class PushableBlock : MonoBehaviour
{
    public float pushForce = 2.0f; // S�la pro tla�en� blok� hr��em `Player1`
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Billy"))
        {
            // Umo�n� tla�en� bloku hr��em `Player1`
            Vector2 pushDirection = collision.contacts[0].normal * -1;
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Force);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Bob"))
        {
            // Zastav� jak�koliv pohyb bloku p�i kontaktu s `Player2`
            rb.velocity = Vector2.zero;       // Zru�� pohyb bloku
            rb.angularVelocity = 0f;          // Zru�� rotaci bloku
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Po opu�t�n� kolize s `Player2` uvoln� pozici bloku, aby `Player1` mohl blok op�t tla�it
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bob"))
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
