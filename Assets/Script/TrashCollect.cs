using UnityEngine;

public class TrashCollect : MonoBehaviour
{
    public bool isCollected = false;

    public void Collect()
    {
        if (isCollected) return;

        isCollected = true;

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayBagUseSound();
        }

        if (TrashManager.instance != null)
        {
            TrashManager.instance.AddTrash();
        }

        Destroy(gameObject);
    }
}