using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Easel : Interactable
{
    public static bool showing;
    public static Texture2D currentNFT;
    public NFT defaultNFT;
    public static NFT sDefaultNFT;

    public void Awake()
    {
        sDefaultNFT = defaultNFT;
        showing = false;
        currentNFT = defaultNFT.image;
    }

    public override void OnInteract()
    {
        showing = true;
    }

    public void OnLeave()
    {
        showing = false;
    }
}
