using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFT : MonoBehaviour
{
    public Color[,] baseMatrix, colorMatrix;
    public Texture2D image;
    public static readonly Vector2Int size = DrawTexture.size;

    private void Start()
    {
        ResetImage();
        ResetMatrices();
    }

    public void ResetImage()
    {
        image = new Texture2D(size.x, size.y, TextureFormat.RGBA32, false);
        image.filterMode = FilterMode.Point;
    }

    public void Update()
    {
        //reset textures
        UpdateColorMatrix();

        //draw the cursor
        DrawCursor();

        //draw the pixels
        DrawTexture.SetTexture(ref image, colorMatrix);
    }

    void DrawCursor()
    {
        DrawTexture.DrawPixelAtCursor(ref colorMatrix, Color.red);
        DrawTexture.DrawPixelAtCursor(ref colorMatrix, Color.red, 1);
        DrawTexture.DrawPixelAtCursor(ref colorMatrix, Color.red, -1);
        DrawTexture.DrawPixelAtCursor(ref colorMatrix, Color.red, 0, 1);
        DrawTexture.DrawPixelAtCursor(ref colorMatrix, Color.red, 0, -1);
    }

    public void UpdateColorMatrix()
    {
        colorMatrix = (Color[,])baseMatrix.Clone();
    }

    public void ResetMatrices()
    {
        baseMatrix = new Color[size.x, size.y];
        colorMatrix = new Color[size.x, size.y];
        for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
            {
                baseMatrix[i, j] = Color.white;
                colorMatrix[i, j] = Color.white;
            }
    }
}
