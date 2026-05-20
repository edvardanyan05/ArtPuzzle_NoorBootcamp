using UnityEngine;

public class Piece : MonoBehaviour
{
    [Header("Board Position")]
    public Transform currentPos;

    [Header("Visual")]
    public Transform visual;

    [Header("Settings")]
    public float liftAmount = 0.3f;

    public float swapDistance = 2f;

    private Vector3 offset;
    private bool isDragging = false;

    void OnMouseDown()
    {
        isDragging = true;

        visual.localPosition = Vector3.up * liftAmount;

        offset = transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        if (!isDragging)
            return;

        transform.position = GetMouseWorldPosition() + offset;
    }

    void OnMouseUp()
    {
        isDragging = false;

        visual.localPosition = Vector3.zero;

        Piece nearestPiece = GetNearestPiece();

        if (nearestPiece != null)
        {
            Swap(nearestPiece);
        }
        else
        {
            ReturnToPosition();
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = 10f;

        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    Piece GetNearestPiece()
    {
        Piece[] allPieces = FindObjectsOfType<Piece>();

        Piece nearest = null;

        float shortestDistance = Mathf.Infinity;

        foreach (Piece piece in allPieces)
        {
            if (piece == this)
                continue;

            float distance = Vector3.Distance(
                transform.position,
                piece.transform.position
            );

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearest = piece;
            }
        }

        if (shortestDistance <= swapDistance)
            return nearest;

        return null;
    }

    void Swap(Piece other)
    {
        Transform tempPos = currentPos;

        currentPos = other.currentPos;
        other.currentPos = tempPos;

        transform.position = currentPos.position;
        other.transform.position = other.currentPos.position;
    }

    void ReturnToPosition()
    {
        transform.position = currentPos.position;
    }
}