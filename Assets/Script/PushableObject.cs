using UnityEngine;

public class PushableBlock : MonoBehaviour
{
    public float pushForce = 2.0f; // Síla pro tlaèení blokù hráèem `Player1`
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Billy"))
        {
            // Umožní tlaèení bloku hráèem `Player1`
            Vector2 pushDirection = collision.contacts[0].normal * -1;
            rb.AddForce(pushDirection * pushForce, ForceMode2D.Force);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Bob"))
        {
            // Zastaví jakýkoliv pohyb bloku pøi kontaktu s `Player2`
            rb.velocity = Vector2.zero;       // Zruší pohyb bloku
            rb.angularVelocity = 0f;          // Zruší rotaci bloku
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Po opuštìní kolize s `Player2` uvolní pozici bloku, aby `Player1` mohl blok opìt tlaèit
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bob"))
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
