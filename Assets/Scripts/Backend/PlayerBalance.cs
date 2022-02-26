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
}
