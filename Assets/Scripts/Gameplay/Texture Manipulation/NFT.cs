using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
        if(DrawTexture.activeBrush != null)
            DrawTexture.activeBrush.DrawAtCursor(ref colorMatrix);
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

    public int value
    {
        get
        {
            float minConRange = 1;
            float maxConRange = 0;

            int toRet = 0;

            List<Color> contained = new List<Color>();

            for(int x = 1; x < size.x; x++)
                for(int y = 1; y < size.y; y++)
                {
                    Color current = baseMatrix[x, y];
                    Color nextTo = baseMatrix[x - 1, y - 1];
                    float nextGS = nextTo.grayscale;
                    float currGS = current.grayscale;
                    float GSChange = Mathf.Abs(nextGS - currGS);
                    if (GSChange < minConRange && GSChange != 0)
                        minConRange = GSChange;
                    if (GSChange > maxConRange)
                        maxConRange = GSChange;

                    if (Random.Range(0f, 1f) < 0.3f)
                        toRet += (int)(GSChange * 3) * contained.Count;

                    if (!contained.Contains(current))
                        contained.Add(nextTo);
                }

            toRet = (int)Mathf.Pow(toRet, 0.7f);

            if (Random.Range(0f, 1f) < 0.002f)
                toRet += Random.Range(-200, 100000);
            toRet += (int)(200 * Mathf.Sqrt(contained.Count) * (maxConRange - minConRange) * Random.Range(0.9f, 1.2f));

            return toRet;
        }
    }
}
