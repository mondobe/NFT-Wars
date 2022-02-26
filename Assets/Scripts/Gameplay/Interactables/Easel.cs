using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Easel : Interactable
{
    public static bool showing;
    public static Texture2D currentNFT;
    public NFT defaultNFT;
    public static NFT sDefaultNFT;
    public static bool sellPhase;
    public RawImage sellPrev;
    public static float sellTimer;
    public TextMeshProUGUI priceText;
    public static int prospectiveValue;

    public void Awake()
    {
        sDefaultNFT = defaultNFT;
        showing = false;
        currentNFT = defaultNFT.image;
        sellPhase = false;
    }

    public override void OnInteract()
    {
        if(!sellPhase)
            showing = true;
    }

    public new void Update()
    {
        base.Update();

        sellPrev.enabled = sellPhase;
        priceText.enabled = sellPhase;
        sellTimer -= Time.deltaTime;

        if(DrawTexture.activeNFT != null)
            currentNFT = DrawTexture.activeNFT.image;

        sellPrev.texture = currentNFT;

        if (sellPhase)
        {
            if (sellTimer > 0)
            {
                prospectiveValue = (int)(DrawTexture.soldValue + Mathf.Cos(sellTimer * 1.6f) * 700f * sellTimer);
                priceText.text = "$" + prospectiveValue;
            }
            else if (sellTimer > -3)
                priceText.text = "$" + DrawTexture.soldValue.ToString();
            else
            {
                sellPhase = false;
                PlayerBalance.money += DrawTexture.soldValue;
                Destroy(DrawTexture.activeNFT);
                DrawTexture.activeNFT = null;
            }
        }
    }

    public void OnLeave()
    {
        showing = false;
    }
}
