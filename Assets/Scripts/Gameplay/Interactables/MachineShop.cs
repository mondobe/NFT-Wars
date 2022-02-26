using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MachineShop : Interactable
{
    public TextMeshProUGUI priceGUI;
    public static int price = 2000;
    public GameObject botFab;

    // Update is called once per frame
    public new void Update()
    {
        base.Update();
        priceGUI.enabled = close;
        priceGUI.text = "Buy: $" + price;
    }

    public override void OnInteract()
    {
        if (PlayerBalance.money < price)
            return;
        PlayerBalance.money -= price;
        Instantiate(botFab);
        price = (price * 4) + 1000;
    }
}
