using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PaintBucket : Interactable
{
    public TextMeshProUGUI priceGUI;

    // Update is called once per frame
    public new void Update()
    {
        base.Update();
        priceGUI.enabled = close;
        priceGUI.text = "Buy: $" + UnlockedColors.price;
        spriteRenderers[0].color = UnlockedColors.available[0];
    }

    public override void OnInteract()
    {
        UnlockedColors.TryBuyColor(0);
        if (UnlockedColors.available.Count == 0)
        {
            Destroy(priceGUI.gameObject);
            Destroy(gameObject);
        }
    }
}
