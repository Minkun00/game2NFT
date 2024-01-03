using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text coin;
    public static int coinAmount;

    void Start()
    {
        coin.text = "0";

    }

    void Update()
    {
        coin.text = coinAmount.ToString();
    }
}
