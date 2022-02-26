using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Brush
{
    public Vector2Int[] pixels;
    public string name;

    public void DrawAtCursor(ref Color[,] matrix)
    {
        foreach (Vector2Int v in pixels)
            DrawTexture.DrawPixelAtCursor(ref matrix, DrawTexture.activeColor, v.x, v.y);
    }

    public override string ToString()
    {
        return name;
    }
}
