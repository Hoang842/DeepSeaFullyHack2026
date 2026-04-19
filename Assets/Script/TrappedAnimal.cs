using UnityEngine;

public class TrappedAnimal : MonoBehaviour
{
    public enum AnimalType
    {
        Turtle,
        Octopus,
        Default
    }

    public AnimalType animalType;

    [Header("Sprites")]
    public Sprite trappedSprite;
    public Sprite freedSprite;

    [Header("Movement")]
    public MonoBehaviour moveScript;

    private SpriteRenderer sr;
    private bool isFreed = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (trappedSprite != null)
            sr.sprite = trappedSprite;

        if (moveScript != null)
            moveScript.enabled = false;
    }

    public void Rescue()
    {
        if (isFreed) return;

        isFreed = true;

        if (freedSprite != null)
            sr.sprite = freedSprite;

        PlaySound();

        if (moveScript != null)
            moveScript.enabled = true;

        if (RescueManager.instance != null)
        {
            RescueManager.instance.AnimalSaved();
        }
    }

    void PlaySound()
    {
        if (AudioManager.instance == null) return;

        switch (animalType)
        {
            case AnimalType.Turtle:
                AudioManager.instance.PlayTurtleSound();
                break;

            case AnimalType.Octopus:
                AudioManager.instance.PlayOctoSound();
                break;

            default:
                break;
        }
    }
}