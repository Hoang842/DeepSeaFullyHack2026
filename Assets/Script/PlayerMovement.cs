using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Boundary")]
    public float maxY = 7.282786f; // mặt nước

    [Header("Oxygen")]
    public float maxOxygen = 100f;
    public float currentOxygen = 50f;
    public float oxygenDrainPerSecond = 5f;
    public float oxygenGainPerSecond = 15f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer sr;

    private bool touchingSurface;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // lấy sprite (kể cả nằm trong child)
        sr = GetComponentInChildren<SpriteRenderer>();

        // setup Rigidbody cho underwater
        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        // input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        // 🎯 Flip player
        if (movement.x > 0.01f)
            sr.flipX = false;
        else if (movement.x < -0.01f)
            sr.flipX = true;

        // 💧 Oxygen xử lý
        if (touchingSurface)
        {
            currentOxygen += oxygenGainPerSecond * Time.deltaTime;
        }
        else
        {
            currentOxygen -= oxygenDrainPerSecond * Time.deltaTime;
        }

        currentOxygen = Mathf.Clamp(currentOxygen, 0f, maxOxygen);
    }

    void FixedUpdate()
    {
        // movement
        rb.linearVelocity = movement * moveSpeed;

        Vector2 pos = rb.position;

        // 🚫 chặn không cho vượt quá mặt nước
        if (pos.y >= maxY)
        {
            pos.y = maxY;
            rb.position = pos;

            // chặn velocity đi lên
            if (rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            }

            touchingSurface = true;
        }
        else
        {
            touchingSurface = false;
        }
    }
}