using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTex : MonoBehaviour
{
    public RawImage raw;

    // Start is called before the first frame update
    void Start()
    {
        raw.texture = DrawTexture.debug;
    }

    // Update is called once per frame
    void Update()
    {

    }
}