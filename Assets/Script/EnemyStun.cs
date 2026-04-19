using UnityEngine;

public class EnemyStun : MonoBehaviour
{
    public float minStunDuration = 2f;
    public float maxStunDuration = 3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                float randomStun = Random.Range(minStunDuration, maxStunDuration);
                player.StunPlayer(randomStun);
            }

            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlayStunSound();
            }
        }
    }
}