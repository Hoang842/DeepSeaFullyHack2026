using UnityEngine;

public class BubbleAuto : MonoBehaviour
{
    [Header("Movement")]
    public float minSpeed = 1f;
    public float maxSpeed = 2f;

    [Header("Lifetime")]
    public float minLifeTime = 1f;
    public float maxLifeTime = 3f;
    public float respawnDelay = 1.5f;

    [Header("Oxygen")]
    public float oxygenAmount = 20f;

    private float moveSpeed;
    private Vector3 spawnPosition;
    private bool isCollected = false;

    void Start()
    {
        spawnPosition = transform.position;

        // random speed
        moveSpeed = Random.Range(minSpeed, maxSpeed);

        // random lifetime
        float randomLife = Random.Range(minLifeTime, maxLifeTime);
        Invoke(nameof(DestroySelf), randomLife);
    }

    void Update()
    {
        // bay lên
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    void DestroySelf()
    {
        if (isCollected) return;

        RespawnBubble();
        Destroy(gameObject);
    }

    void RespawnBubble()
    {
        GameObject newBubble = Instantiate(gameObject, spawnPosition, Quaternion.identity);

        newBubble.SetActive(false);
        newBubble.GetComponent<BubbleAuto>().StartRespawn(respawnDelay);
    }

    public void StartRespawn(float delay)
    {
        Invoke(nameof(Activate), delay);
    }

    void Activate()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected) return;

        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.currentOxygen += oxygenAmount;
                player.currentOxygen = Mathf.Clamp(player.currentOxygen, 0f, player.maxOxygen);
            }

            isCollected = true;

            RespawnBubble();
            Destroy(gameObject);
        }
    }
}