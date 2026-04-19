using System.Collections;
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

    [Header("Animation")]
    public bool useDanceAnimation = false;
    public Animator animator;
    public float danceDuration = 2f;

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

        PlaySound();

        if (RescueManager.instance != null)
        {
            RescueManager.instance.AnimalSaved();
        }

        if (useDanceAnimation && animator != null)
        {
            StartCoroutine(PlayDanceThenFinish());
        }
        else
        {
            FinishRescue();
        }
    }

    IEnumerator PlayDanceThenFinish()
    {
        animator.SetTrigger("Dance");

        yield return new WaitForSeconds(danceDuration);

        FinishRescue();
    }

    void FinishRescue()
    {
        if (freedSprite != null && sr != null)
            sr.sprite = freedSprite;

        if (moveScript != null)
            moveScript.enabled = true;
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
        }
    }
}