using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticStats : MonoBehaviour
{
    public static int maxMoney;

    // Start is called before the first frame update
    void Start()
    {
        maxMoney = 0;
        DontDestroyOnLoad(this.gameObject);
    }
}
