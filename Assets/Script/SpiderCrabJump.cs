using UnityEngine;

public class SpiderCrabMover : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 4f;
    public float moveDistance = 3f;
    public float jumpCooldown = 1f;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Vector3 startPos;
    private int direction = 1;
    private float jumpTimer;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        jumpTimer = jumpCooldown;
        UpdateFacing();
    }

    void Update()
    {
        jumpTimer -= Time.deltaTime;

        isGrounded = false;

        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(
                groundCheck.position,
                groundCheckRadius,
                groundLayer
            );
        }

        if (Mathf.Abs(transform.position.x - startPos.x) >= moveDistance)
        {
            direction *= -1;
            startPos = transform.position;
            UpdateFacing();
        }
    }

    void FixedUpdate()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

            if (jumpTimer <= 0f)
            {
                rb.linearVelocity = new Vector2(direction * moveSpeed, jumpForce);
                jumpTimer = jumpCooldown;
            }
        }
    }

    void UpdateFacing()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}