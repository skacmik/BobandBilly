using UnityEngine;

public class PlayerMovementSmall : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private float lastDirection = 1f; // Uchování posledního smìru

    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public LayerMask playerLayer;

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
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer | playerLayer);
        isGrounded = collider != null;
    }

    private void Movement()
    {
        if (Input.GetKey(leftKey))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            FlipCharacter(-1);
        }
        else if (Input.GetKey(rightKey))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            FlipCharacter(1);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void FlipCharacter(float direction)
    {
        if (direction != lastDirection)
        {
            lastDirection = direction;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);
        }
    }

}
