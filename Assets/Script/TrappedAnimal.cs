using UnityEngine;

public class TrappedAnimal : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite trappedSprite;
    public Sprite freedSprite;

    [Header("Movement")]
    public MonoBehaviour moveScript;

    [Header("Audio")]
    public AudioClip rescueSound;

    private SpriteRenderer sr;
    private bool isFreed = false;
    private AudioSource audioSource;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if (trappedSprite != null)
        {
            sr.sprite = trappedSprite;
        }

        if (moveScript != null)
        {
            moveScript.enabled = false;
        }
    }

    public void Rescue()
    {
        if (isFreed) return;

        isFreed = true;

        if (freedSprite != null)
        {
            sr.sprite = freedSprite;
        }

        if (rescueSound != null)
        {
            if (audioSource != null)
            {
                audioSource.PlayOneShot(rescueSound);
            }
        }
        else if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayPortalSound();
        }

        if (moveScript != null)
        {
            moveScript.enabled = true;
        }

        Debug.Log("Animal rescued and now moving.");
    }
}