using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalance : MonoBehaviour
{
    public static int money;
    public int startingMoney;

    private void Start()
    {
        money = startingMoney;
    }

    public void Update()
    {
        if (money > StaticStats.maxMoney)
            StaticStats.maxMoney = money;
    }
}
