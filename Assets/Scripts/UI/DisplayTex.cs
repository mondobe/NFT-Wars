using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DisplayTex : MonoBehaviour
{
    public RawImage raw;
    public GameObject colorPrevGO;
    public TextMeshProUGUI wallet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(DrawTexture.activeNFT != null)
            raw.texture = DrawTexture.activeNFT.image;
        raw.enabled = Easel.showing;
        if(colorPrevGO.activeSelf != Easel.showing)
            colorPrevGO.SetActive(Easel.showing);
        wallet.text = "$" + PlayerBalance.money;
    }
}
