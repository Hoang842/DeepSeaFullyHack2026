using UnityEngine;

public class FishSwim : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 3f;

    private Vector3 startPos;
    private int direction = 1; // 1 = phải, -1 = trái

    void Start()
    {
        startPos = transform.position;

        // FIX hướng ban đầu
        Flip();
    }

    void Update()
    {
        // di chuyển
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        // kiểm tra vượt khoảng cách
        if (Vector2.Distance(startPos, transform.position) >= moveDistance)
        {
            direction *= -1; // đảo hướng

            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}