using UnityEngine;
using TMPro;
using DG.Tweening;

public class BoardManager : MonoBehaviour
{
    [Header("Pieces")]
    public Piece[] allPieces;

    [Header("Timer")]
    public float timeLimit = 30f;
    public TextMeshProUGUI timerText;

    [Header("Win Animation")]
    public int gridWidth = 2;
    public int gridHeight = 2;
    public float pieceSize = 1f;
    public float animationDuration = 0.6f;

    private float currentTime;
    private bool hasWon = false;
    private bool hasLost = false;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    void Start()
    {
        currentTime = timeLimit;
    }

    void Update()
    {
        if (hasWon || hasLost) return;

        currentTime -= Time.deltaTime;
        timerText.text = Mathf.Ceil(currentTime).ToString();

        if (currentTime <= 0f)
            Lose();
    }

    public void CheckWin()
    {
        if (hasWon || hasLost) return;

        foreach (Piece piece in allPieces)
            if (!piece.IsLocked()) return;

        hasWon = true;
        Win();
    }

    void Win()
    {
        foreach (Piece piece in allPieces)
            piece.DisableInteraction();

        Renderer planeRenderer = allPieces[0].plane.GetComponent<Renderer>();
        float autoSizeX = planeRenderer.bounds.size.x;
        float autoSizeZ = planeRenderer.bounds.size.z;

        Vector3 center = Vector3.zero;
        foreach (Piece piece in allPieces)
            center += piece.plane.position;
        center /= allPieces.Length;

        Sequence seq = DOTween.Sequence();

        foreach (Piece piece in allPieces)
        {
            float targetX = center.x + (piece.correctCol - (gridWidth - 1) / 2f) * autoSizeX;
            float targetZ = center.z + (piece.correctRow - (gridHeight - 1) / 2f) * autoSizeZ;
            Vector3 targetPos = new Vector3(targetX, center.y, targetZ);

            seq.Join(
                piece.plane
                    .DOMove(targetPos, animationDuration)
                    .SetEase(Ease.InOutQuad)
            );
        }

        seq.OnComplete(() =>
        {
            Debug.Log("Анимация завершена — показываем Win UI!");
            // Здесь добавишь показ Win панели
        });
    }

    void Lose()
    {
        hasLost = true;
        Debug.Log("YOU LOSE!");
    }
}