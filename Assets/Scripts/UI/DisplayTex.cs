using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DisplayTex : MonoBehaviour
{
    public RawImage raw;
    Collider2D box;

    // Start is called before the first frame update
    void Start()
    {
        box = gameObject.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        raw.texture = DrawTexture.activeNFT.image;
        raw.enabled = Easel.showing;
    }
}
