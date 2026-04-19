using UnityEngine;

public class AnimalRescue : MonoBehaviour
{
    public Sprite trashSprite;
    public Sprite safeSprite;

    private SpriteRenderer sr;
    private bool isSaved = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (trashSprite != null)
        {
            sr.sprite = trashSprite;
        }
    }

    public void Rescue()
    {
        if (isSaved) return;

        isSaved = true;

        if (safeSprite != null)
        {
            sr.sprite = safeSprite;
        }

        if (RescueManager.instance != null)
        {
            RescueManager.instance.AnimalSaved();
        }

        Debug.Log("Animal Saved!");
    }
}