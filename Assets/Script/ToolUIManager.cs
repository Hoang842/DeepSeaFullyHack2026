using UnityEngine;
using UnityEngine.UI;

public class ToolUIManager : MonoBehaviour
{
    public Image scissorIcon;
    public Image bagIcon;

    public Color selectedColor = Color.white;
    public Color normalColor = new Color(0.5f, 0.5f, 0.5f);

    private int currentTool = 0; // 1 = scissor, 2 = bag

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentTool = 1;
            UpdateUI();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentTool = 2;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        // reset
        scissorIcon.color = normalColor;
        bagIcon.color = normalColor;

        // highlight
        if (currentTool == 1)
        {
            scissorIcon.color = selectedColor;
        }
        else if (currentTool == 2)
        {
            bagIcon.color = selectedColor;
        }
    }

    public int GetCurrentTool()
    {
        return currentTool;
    }
}