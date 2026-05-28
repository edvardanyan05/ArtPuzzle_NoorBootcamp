using UnityEngine;
using DG.Tweening;

public class SettingsPanel : MonoBehaviour
{
    public float duration = 0.4f;
    public float hiddenX = 800f;

    private RectTransform rectTransform;
    private BoardManager boardManager;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        boardManager = FindFirstObjectByType<BoardManager>();
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        rectTransform.anchoredPosition = new Vector2(hiddenX, rectTransform.anchoredPosition.y);
        rectTransform.DOAnchorPosX(0f, duration).SetEase(Ease.OutCubic);
        boardManager?.PauseTimer();
    }

    public void Close()
    {
        rectTransform.DOAnchorPosX(hiddenX, duration)
            .SetEase(Ease.InCubic)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                boardManager?.ResumeTimer();
            });
    }
}