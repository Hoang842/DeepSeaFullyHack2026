using UnityEngine;

public class PlayerToolSwitcher : MonoBehaviour
{
    private Animator animator;
    private int currentTool = 0; // 0 = none, 1 = scissor, 2 = bag

    public float interactRadius = 1.2f;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator != null)
        {
            animator.SetInteger("Tool", currentTool);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentTool = 1;

            if (animator != null)
            {
                animator.SetInteger("Tool", currentTool);
            }

            Debug.Log("Selected Scissor");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentTool = 2;

            if (animator != null)
            {
                animator.SetInteger("Tool", currentTool);
            }

            Debug.Log("Selected Bag");
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentTool = 0;

            if (animator != null)
            {
                animator.SetInteger("Tool", currentTool);
            }

            Debug.Log("Selected None");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTool == 1)
            {
                if (animator != null)
                {
                    animator.SetTrigger("Cut");
                }

                TryRescueAnimal();
            }
            else if (currentTool == 2)
            {
                if (animator != null)
                {
                    animator.SetTrigger("BagUse");
                }

                TryCollectTrash();
            }
        }
    }

    void TryRescueAnimal()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRadius);

        foreach (Collider2D hit in hits)
        {
            TrappedAnimal trapped = hit.GetComponent<TrappedAnimal>();

            if (trapped != null)
            {
                trapped.Rescue();
                break;
            }

            AnimalRescue animal = hit.GetComponent<AnimalRescue>();

            if (animal != null)
            {
                animal.Rescue();

                if (AudioManager.instance != null)
                {
                    AudioManager.instance.PlayPortalSound();
                }

                break;
            }
        }
    }

    void TryCollectTrash()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRadius);

        foreach (Collider2D hit in hits)
        {
            TrashCollect trash = hit.GetComponent<TrashCollect>();

            if (trash != null)
            {
                trash.Collect();
                break;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}