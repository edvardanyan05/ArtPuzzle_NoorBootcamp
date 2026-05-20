using UnityEngine;
using TMPro;
public class BoardManager : MonoBehaviour
{
    [Header("Pieces")]
    public Piece[] allPieces;

    [Header("Timer")]
    public float timeLimit = 30f;

    public TextMeshProUGUI timerText;

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
        if (hasWon || hasLost)
            return;

        currentTime -= Time.deltaTime;
        timerText.text = Mathf.Ceil(currentTime).ToString();

        if (currentTime <= 0f)
        {
            Lose();
        }
    }

    public void CheckWin()
    {
        if (hasWon || hasLost)
            return;

        foreach (Piece piece in allPieces)
        {
            if (!piece.IsLocked())
            {
                return;
            }
        }

        hasWon = true;

        Win();
    }

    void Win()
    {
        Debug.Log("YOU WIN!");
    }

    void Lose()
    {
        hasLost = true;

        Debug.Log("YOU LOSE!");
    }
}