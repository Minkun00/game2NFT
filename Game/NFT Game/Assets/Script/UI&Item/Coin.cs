using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.name == "Player")
        {
            Destroy(gameObject);
            Score.coinAmount += 1;

        }
    }
}
