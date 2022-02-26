using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BrushPalette : Interactable
{
    public TextMeshProUGUI priceGUI;

    // Update is called once per frame
    public new void Update()
    {
        base.Update();
        priceGUI.enabled = close;
        priceGUI.text = "Buy " + UnlockedBrushes.available[0] + " brush: $" + UnlockedBrushes.price;
    }

    public override void OnInteract()
    {
        UnlockedBrushes.TryBuyBrush(0);
        if (UnlockedBrushes.available.Count == 0)
        {
            Destroy(priceGUI.gameObject);
            Destroy(gameObject);
        }
    }
}
