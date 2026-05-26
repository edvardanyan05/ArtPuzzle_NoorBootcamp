using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridWidth = 2;
    public int gridHeight = 2;

    [Header("Piece Position in Grid")]
    public int row = 0;
    public int col = 0;

    void Start()
    {
        ApplyTexture();
    }

    void OnValidate()
    {
        ApplyTexture();
    }

    void ApplyTexture()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend == null) return;

        float scaleX = 1f / gridWidth;
        float scaleY = 1f / gridHeight;

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        rend.GetPropertyBlock(block);
        block.SetVector("_MainTex_ST", new Vector4(scaleX, scaleY, col * scaleX, row * scaleY));
        rend.SetPropertyBlock(block);
    }
}