using UnityEngine;

public class TrashFloat : MonoBehaviour
{
    public float floatSpeed = 2f;   // tốc độ lên xuống
    public float floatHeight = 0.3f; // biên độ (cao bao nhiêu)

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}