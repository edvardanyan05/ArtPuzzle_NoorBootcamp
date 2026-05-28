using UnityEngine;
using DG.Tweening;

public class MenuPanels : MonoBehaviour
{
    public RectTransform levelPanel;
    public float duration = 0.5f;

    public void Show()
    {
        levelPanel.DOAnchorPosX(0f, duration).SetEase(Ease.OutCubic);
    }

    public void Hide()
    {
        levelPanel.DOAnchorPosX(-80f, duration).SetEase(Ease.OutCubic);

    }

    public void HideInLevel()
    {
        levelPanel.DOAnchorPosX(-2000f, duration).SetEase(Ease.OutCubic);

    }
}
