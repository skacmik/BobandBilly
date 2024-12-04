using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float crouchSpeed = 2.5f; // Rychlost pøi pøikrèení
    public float jumpForce = 5f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isCrouching;

    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;
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

        // Uložení pùvodní velikosti a výpoètu velikosti pro pøikrèení
        originalColliderSize = boxCollider.size;
        crouchColliderSize = new Vector2(originalColliderSize.x, originalColliderSize.y / 2);
    }

    void Update()
    {
        Movement();
    }

    void FixedUpdate()
    {
        // Kontrola, zda je postava na zemi
        Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        isGrounded = collider != null;
    }

    private void Movement()
    {
        // Nastavení rychlosti podle pøikrèení
        float moveSpeed = isCrouching ? crouchSpeed : speed;

        // Pohyb doleva a doprava
        if (Input.GetKey(leftKey))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(rightKey))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Skákání
        if (Input.GetKeyDown(jumpKey) && isGrounded && !isCrouching)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        // Pøikrèení
        if (Input.GetKeyDown(crouchKey))
        {
            isCrouching = true;
            boxCollider.size = crouchColliderSize; // Zmenšení kolizního boxu
        }
        else if (Input.GetKeyUp(crouchKey))
        {
            isCrouching = false;
            boxCollider.size = originalColliderSize; // Obnovení pùvodní velikosti
        }
    }
}

