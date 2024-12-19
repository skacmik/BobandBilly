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
        // Pokud se dotkne velk� hr��, odemkne pohyb objektu
        if (collision.gameObject.CompareTag("PlayerBig"))
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // Pokud na objekt vstoup� mal� hr��, zajisti pohyb spolu s objektem
        if (collision.gameObject.CompareTag("PlayerSmall"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Jakmile velk� hr�� p�estane tla�it, okam�it� zastav� objekt
        if (collision.gameObject.CompareTag("PlayerBig"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.velocity = Vector2.zero; // Zastav� pohyb objektu
        }

        // Pokud mal� hr�� opust� objekt, odstra� ho z rodi�ovstv�
        if (collision.gameObject.CompareTag("PlayerSmall"))
        {
            collision.transform.SetParent(null);
        }
    }
}
