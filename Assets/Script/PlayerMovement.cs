using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float crouchSpeed = 2.5f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isCrouching;
    private float lastDirection = 1f; // Uchování posledního smìru pohybu

    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode crouchKey;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private BoxCollider2D boxCollider;
    private Vector2 originalColliderSize;
    private Vector2 crouchColliderSize;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        originalColliderSize = boxCollider.size;
        crouchColliderSize = new Vector2(originalColliderSize.x, originalColliderSize.y / 2);
    }

    void Update()
    {
        Movement();
    }

    void FixedUpdate()
    {
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isGrounded = collider != null;
    }

    private void Movement()
    {
        float moveSpeed = isCrouching ? crouchSpeed : speed;

        if (Input.GetKey(leftKey))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            FlipCharacter(-1);
        }
        else if (Input.GetKey(rightKey))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            FlipCharacter(1);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKeyDown(crouchKey))
        {
            isCrouching = true;
            boxCollider.size = crouchColliderSize;
        }
        else if (Input.GetKeyUp(crouchKey))
        {
            isCrouching = false;
            boxCollider.size = originalColliderSize;
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
