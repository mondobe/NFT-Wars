using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DrawTexture : MonoBehaviour
{
    public static Color[,] baseMatrix, colorMatrix;
    static Vector2Int cursor;
    static Vector2 floatCursor;
    public static Texture2D debug;
    public static readonly Vector2Int size = new Vector2Int(200, 200);
    public float myCursorMoveSpeed;
    public static float cursorMoveSpeed;

    Color[] Mat2List(Color[,] matrix)
    {
        Color[] toRet = new Color[size.x * size.y];
        for (int r = 0; r < size.y; r++)
            for (int c = 0; c < size.x; c++)
                toRet[r * size.x + c] = matrix[c, r];
        return toRet;
    }

    // Start is called before the first frame update
    void Awake()
    {
        cursorMoveSpeed = myCursorMoveSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        floatCursor = new Vector2(size.x / 2, size.y / 2);
        debug = new Texture2D(size.x, size.y, UnityEngine.TextureFormat.RGBA32, true, false);
        baseMatrix = new Color[size.x, size.y];
        colorMatrix = new Color[size.x, size.y];
        for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++) 
            {
                baseMatrix[i, j] = Color.white; 
                colorMatrix[i, j] = Color.white; 
            }
    }

    // Update is called once per frame
    void Update()
    {
        debug.SetPixels(Mat2List(colorMatrix));
        debug.Apply();
        //Vector2Int randVec = new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));
        DrawPixelAtCursor(ref colorMatrix, Color.red);
        Debug.Log("Cursor position is " + cursor + ", floatCursor: " + floatCursor);
        cursor = RoundVec2(floatCursor);
    }

    public static void MoveCursor(Vector2 moveVal)
    {
        floatCursor += moveVal * cursorMoveSpeed;
        floatCursor = new Vector2(Mathf.Clamp(floatCursor.x, 0, size.x - 1), Mathf.Clamp(floatCursor.y, 0, size.y - 1));
    }

    public static Vector2Int GetCursorPos()
    {
        return cursor;
    }    

    public static Vector2Int RoundVec2(Vector2 vec2)
    {
        return new Vector2Int(Mathf.RoundToInt(vec2.x), Mathf.RoundToInt(vec2.y));
    }

    void DrawPixel(ref Color[,] matrix, Vector2Int pos, Color color)
    {
        if (pos.x >= size.x || pos.y >= size.y)
        {
            Debug.LogWarning("Tried to draw pixel outside bounds (too large)");
            return;
        }
        if (pos.x < 0 || pos.y < 0)
        {
            Debug.LogWarning("Tried to draw pixel outside bounds (too small)");
            return;
        }
        matrix[pos.x, pos.y] = color;
    }

    void DrawPixelAtCursor(ref Color[,] matrix, Color color, int offsetX = 0, int offsetY = 0)
    {
        DrawPixel(ref matrix, cursor + new Vector2Int(offsetX, offsetY), color);
    }
}
