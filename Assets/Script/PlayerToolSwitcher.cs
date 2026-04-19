using UnityEngine;

public class PlayerToolSwitcher : MonoBehaviour
{
    private Animator animator;
    private int currentTool = 0; // 0 = none, 1 = scissor, 2 = bag

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentTool = 1;
            Debug.Log("Selected Scissor");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentTool = 2;
            Debug.Log("Selected Bag");
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentTool = 0;
            Debug.Log("Selected None");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentTool == 1)
            {
                animator.SetTrigger("Cut");
            }
            else if (currentTool == 2)
            {
                animator.SetTrigger("BagUse");
            }
        }
    }

    public int GetCurrentTool()
    {
        return currentTool;
    }
}