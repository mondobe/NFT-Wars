using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedBrushes : MonoBehaviour
{
    public static List<Brush> all, unlocked, available;
    public List<Brush> myAll;
    public static int price;

    // Start is called before the first frame update
    void Start()
    {
        all = myAll;
        price = 500;
        unlocked = new List<Brush>();
        available = all;
        unlocked.Add(all[0]);
        available.RemoveAt(0);
    }

    public static bool TryBuyBrush(int which)
    {
        if (PlayerBalance.money < price)
            return false;
        PlayerBalance.money -= price;
        unlocked.Add(available[which]);
        available.RemoveAt(which);
        price *= 4;
        return true;
    }
}
