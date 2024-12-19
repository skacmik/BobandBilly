using UnityEngine;

public class PushableObject : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Pokud se dotkne velký hráè, odemkne pohyb objektu
        if (collision.gameObject.CompareTag("PlayerBig"))
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // Pokud na objekt vstoupí malý hráè, zajisti pohyb spolu s objektem
        if (collision.gameObject.CompareTag("PlayerSmall"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Jakmile velký hráè pøestane tlaèit, okamžitì zastaví objekt
        if (collision.gameObject.CompareTag("PlayerBig"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.velocity = Vector2.zero; // Zastaví pohyb objektu
        }

        // Pokud malý hráè opustí objekt, odstraò ho z rodièovství
        if (collision.gameObject.CompareTag("PlayerSmall"))
        {
            collision.transform.SetParent(null);
        }
    }
}
