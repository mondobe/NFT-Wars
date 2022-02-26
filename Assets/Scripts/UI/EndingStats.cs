using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingStats : MonoBehaviour
{
    public TextMeshProUGUI statsText;

    // Update is called once per frame
    void Update()
    {
        statsText.text = "Money - $" + StaticStats.maxMoney;
    }
}
