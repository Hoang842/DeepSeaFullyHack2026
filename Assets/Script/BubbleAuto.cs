using UnityEngine;

public class BubbleAuto : MonoBehaviour
{
    public float moveSpeed = 1.5f;
    public float lifeTime = 2f;
    public float respawnDelay = 1.5f;
    public float oxygenAmount = 20f;

    private Vector3 spawnPosition;
    private bool isCollected = false;

    void Start()
    {
        spawnPosition = transform.position;
        Invoke(nameof(DestroySelf), lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    void DestroySelf()
    {
        if (isCollected) return;

        GameObject newBubble = Instantiate(gameObject, spawnPosition, Quaternion.identity);
        newBubble.SetActive(false);
        newBubble.GetComponent<BubbleAuto>().StartRespawn(respawnDelay);

        Destroy(gameObject);
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

            GameObject newBubble = Instantiate(gameObject, spawnPosition, Quaternion.identity);
            newBubble.SetActive(false);
            newBubble.GetComponent<BubbleAuto>().StartRespawn(respawnDelay);

            Destroy(gameObject);
        }
    }
}