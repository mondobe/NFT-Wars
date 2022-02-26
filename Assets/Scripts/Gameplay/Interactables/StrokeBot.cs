using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StrokeBot : Interactable
{
    public bool placed;
    public NFT currentNFT, sampleNFT;
    public Color[,] stroke;
    public StrokeBot next, last;
    public bool available;
    public float NFTimer;
    public bool strokeChanged;
    public Texture2D lastMade;
    public GameObject prev, newPrev;
    public GameObject worldCanvas;
    public RawImage prevRaw;
    public TextMeshProUGUI valueText;
    public int lastValue;

    // Start is called before the first frame update
    void Awake()
    {
        placed = false;
        stroke = new Color[NFT.size.x, NFT.size.y];
        sampleNFT.ResetMatrices();
        strokeChanged = false;
        lastMade = new Texture2D(NFT.size.x, NFT.size.y);
    }

    new void Start()
    {
        base.Start();
        CharMove.placing = true;
        worldCanvas = GameObject.Find("World Canvas");
        newPrev = Instantiate(prev, transform.position + new Vector3(1.08f, 1f), Quaternion.identity, worldCanvas.transform);
        prevRaw = newPrev.GetComponent<RawImage>();
        valueText = newPrev.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        lastValue = 0;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        if (NFTimer < 0 || NFTimer > 1)
            newPrev.transform.position = transform.position + new Vector3(1.08f, 1f);

        NFTimer -= Time.deltaTime;

        available = strokeChanged && currentNFT == null && NFTimer <= 0 && !(Easel.showing && DrawTexture.currentBot == this);

        prevRaw.enabled = NFTimer > 0;
        prevRaw.texture = lastMade;
        valueText.enabled = NFTimer > 0 && next == null;
        valueText.text = "$" + lastValue;

        if (!placed)
        {
            available = false;
            CharMove.place = this;
            transform.position = CharMove.player.transform.position + Vector3.up * 3;

            return;
        }

        if (currentNFT != null)
        {
            ApplyStroke();
            HandOff();
        }

        CheckForNew();
    }

    public void CheckForNew()
    {
        if (last != null || !available)
            return;
        
        currentNFT = gameObject.AddComponent<NFT>();
        currentNFT.ResetMatrices();
    }

    public void HandOff()
    {
        if (next == null)
        {
            int soldValue = currentNFT.value;
            lastValue = soldValue;
            PlayerBalance.money += soldValue;
            NFTimer = 3;
            DrawTexture.SetTexture(ref lastMade, currentNFT.baseMatrix);
            Debug.Log("new NFT " + (currentNFT.image == null) + " sold by strokebot for $" + soldValue);
            currentNFT.ResetImage();
            Destroy(currentNFT);
            currentNFT = null;
            return;
        }
        if (!next.available)
            return;
        DrawTexture.SetTexture(ref lastMade, currentNFT.baseMatrix);
        next.currentNFT = currentNFT;
        lastMade = Instantiate(currentNFT.image);
        currentNFT = null;
        NFTimer = 2;
    }

    public override void OnInteract()
    {
        if (!placed)
        {
            Place();
            return;
        }
        strokeChanged = true;
        stroke = new Color[NFT.size.x, NFT.size.y];
        DrawTexture.hideOnRelease = true;
        sampleNFT.ResetMatrices();
        DrawTexture.activeNFT = sampleNFT;
        DrawTexture.currentBot = this;
        Easel.showing = true;
    }

    public void UpdateStroke()
    {
        for(int x = 0; x < NFT.size.x; x++)
            for (int y = 0; y < NFT.size.y; y++)
            {
                if (sampleNFT.baseMatrix[x, y] != Color.white)
                    stroke[x, y] = sampleNFT.baseMatrix[x, y];
            }
    }

    public void ApplyStroke()
    {
        if (currentNFT == null)
            return;
        for (int x = 0; x < NFT.size.x; x++)
            for (int y = 0; y < NFT.size.y; y++)
            {
                if (stroke[x, y].a == 1)
                    DrawTexture.DrawPixel(ref currentNFT.baseMatrix, new Vector2Int(x, y), stroke[x, y]);
            }
    }

    public void Place()
    {
        placed = true;
        transform.Translate(Vector3.down * 3);
        CharMove.player.transform.Translate(Vector3.right * 2);
        CharMove.placing = false;
    }
}
