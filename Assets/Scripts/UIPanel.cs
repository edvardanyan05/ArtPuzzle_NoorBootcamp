using UnityEngine;
using DG.Tweening;

public class UIPanel : MonoBehaviour
{
    [Header("Animation Settings")]
    public float animationDuration = 0.3f;
    public Ease openEase = Ease.OutBack;
    public Ease closeEase = Ease.InBack;

    [Header("Timer Control")]
    public bool pauseTimerOnOpen = true;

    private BoardManager boardManager;

    private void Awake()
    {
        boardManager = FindObjectOfType<BoardManager>();
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, animationDuration)
            .SetEase(openEase);

        if (pauseTimerOnOpen && boardManager != null)
            boardManager.PauseTimer();
    }

    public void Close()
    {
        transform.DOScale(Vector3.zero, animationDuration)
            .SetEase(closeEase)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);

                if (pauseTimerOnOpen && boardManager != null)
                    boardManager.ResumeTimer();
            });
    }
}