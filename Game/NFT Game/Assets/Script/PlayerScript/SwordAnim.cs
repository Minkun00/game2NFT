using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnim : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //enemy hurt by player
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().Hurt(10f);
        }
        //boss hurt by player
        else if (collision.CompareTag("Barrier"))
        {
            GameObject.Find("Boss").GetComponent<Boss>().BarrierHurt(7f);

        }
        else if (collision.CompareTag("Boss") && GameObject.Find("Barrier") == null)
        {
            collision.GetComponent<Boss>().Hurt(5f);
        }

    }
}

