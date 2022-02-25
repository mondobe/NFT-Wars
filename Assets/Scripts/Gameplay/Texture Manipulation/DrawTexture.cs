using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DrawTexture : MonoBehaviour
{
    static Vector2Int cursor;
    static Vector2 floatCursor;
    public static float cursorMoveSpeed;
    public float myCursorMoveSpeed;
    public static bool drawPressed = false;
    public static readonly Vector2Int size = new Vector2Int(200, 200);
    public static Color activeColor;
    public static int activeColorIndex;
    public Image colorPrev;

    public static NFT activeNFT;

    public static Color[] Mat2List(Color[,] matrix)
    {
        Color[] toRet = new Color[size.x * size.y];
        for (int r = 0; r < size.y; r++)
            for (int c = 0; c < size.x; c++)
                toRet[r * size.x + c] = matrix[c, r];
        return toRet;
    }

    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            drawPressed = true;
        if (ctx.canceled)
            drawPressed = false;
    }

    // Start is called before the first frame update
    void Awake()
    {
        cursorMoveSpeed = myCursorMoveSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        floatCursor = new Vector2(size.x / 2, size.y / 2);
        activeNFT = Easel.sDefaultNFT;
        activeColorIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //set cursor position
        SetCursorPos();

        activeColor = UnlockedColors.unlocked[activeColorIndex];
        SetPreviewColor();

        //draw at cursor
        if(Easel.showing)
            DrawCursorStroke(drawPressed, activeNFT.baseMatrix);
    }

    void SetPreviewColor()
    {
        colorPrev.color = activeColor;
    }

    void SetCursorPos()
    {
        cursor = RoundVec2(floatCursor);
        //Debug.Log("Cursor position is " + cursor + ", floatCursor: " + floatCursor + ", pressed: " + drawPressed);
    }

    void DrawCursorStroke(bool pressed, Color[,] matrix)
    {
        if (pressed)
            DrawPixelAtCursor(ref matrix, activeColor);
    }

    public static void SetTexture(ref Texture2D tex, Color[,] matrix)
    {
        tex.SetPixels(Mat2List(matrix));
        tex.Apply();
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

    public static void DrawPixel(ref Color[,] matrix, Vector2Int pos, Color color)
    {
        if (pos.x >= size.x || pos.y >= size.y)
        {
            //Debug.LogWarning("Tried to draw pixel outside bounds (too large)");
            return;
        }
        if (pos.x < 0 || pos.y < 0)
        {
            //Debug.LogWarning("Tried to draw pixel outside bounds (too small)");
            return;
        }
        matrix[pos.x, pos.y] = color;
    }

    public static void DrawPixelAtCursor(ref Color[,] matrix, Color color, int offsetX = 0, int offsetY = 0)
    {
        DrawPixel(ref matrix, cursor + new Vector2Int(offsetX, offsetY), color);
    }

    public void SwitchColor(int switchBy)
    {
        activeColorIndex = (activeColorIndex + UnlockedColors.unlocked.Count + switchBy) % UnlockedColors.unlocked.Count;
        Debug.Log("Switching " + switchBy);
    }

    public void OnSwitch(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<Vector2>().x > 0.3f)
            SwitchColor(1);
        else if (ctx.ReadValue<Vector2>().x < -0.3f)
            SwitchColor(-1);
    }
}
