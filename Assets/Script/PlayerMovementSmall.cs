using UnityEngine;

public class PlayerMovementSmall : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded;

    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public LayerMask playerLayer; // Vrstva vìtšího hráèe

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
    }

    void FixedUpdate()
    {
        // Kontrola, zda je postava na zemi nebo na hlavì hráèe
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer | playerLayer);
        isGrounded = collider != null;
    }

    private void Movement()
    {
        // Pohyb doleva a doprava
        if (Input.GetKey(leftKey))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if (Input.GetKey(rightKey))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Skákání
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}
