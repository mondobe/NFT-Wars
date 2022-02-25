using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockedColors : MonoBehaviour
{
    public static List<Color> all, unlocked, available;
    public List<Color> myAll;
    public static int price;

    // Start is called before the first frame update
    void Start()
    {
        all = myAll;
        price = 100;
        unlocked = new List<Color>();
        available = all;
        unlocked.Add(all[0]);
        available.RemoveAt(0);
    }

    public bool TryBuyColor(int which)
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
