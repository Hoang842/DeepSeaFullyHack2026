using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameUI gameUI;

    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Boundary")]
    public float maxY = 7.282786f;   // mặt nước, không cho đi qua

    [Header("Oxygen")]
    public float maxOxygen = 100f;
    public float currentOxygen = 50f;
    public float oxygenDrainPerSecond = 5f;
    public float oxygenGainPerSecond = 15f;

    [Header("Game Over UI")]
    public GameObject loseUI;   // kéo UI thua vào đây nếu có

    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer sr;

    private bool touchingSurface = false;
    private bool isGameOver = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.freezeRotation = true;
        }
    }

    void Update()
    {
        if (isGameOver) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if (sr != null)
        {
            if (movement.x > 0.01f)
                sr.flipX = false;
            else if (movement.x < -0.01f)
                sr.flipX = true;
        }

        if (touchingSurface)
            currentOxygen += oxygenGainPerSecond * Time.deltaTime;
        else
            currentOxygen -= oxygenDrainPerSecond * Time.deltaTime;

        currentOxygen = Mathf.Clamp(currentOxygen, 0f, maxOxygen);

        if (currentOxygen <= 0f)
        {
            GameOver();
        }
    }

    void FixedUpdate()
    {
        if (isGameOver) return;
        if (rb == null) return;

        rb.linearVelocity = movement * moveSpeed;

        Vector2 pos = rb.position;

        if (pos.y >= maxY)
        {
            pos.y = maxY;
            rb.position = pos;

            if (rb.linearVelocity.y > 0f)
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

    void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        currentOxygen = 0f;

        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        if (gameUI != null)
            gameUI.ShowLoseUI();

        Time.timeScale = 0f;
    }
}