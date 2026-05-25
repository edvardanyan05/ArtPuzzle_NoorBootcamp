using UnityEngine;

public class UIPanelButton : MonoBehaviour
{
    public UIPanel targetPanel;

    public void Open()
    {
        if (targetPanel != null)
            targetPanel.Open();
    }

    public void Close()
    {
        if (targetPanel != null)
            targetPanel.Close();
    }
}