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
            Debug.Log("enemy");
            collision.GetComponent<Enemy>().Hurt(10f);
        }
        //boss hurt by player
        else if (collision.CompareTag("Boss"))
        {
            Debug.Log("barrier");
            collision.GetComponent<Boss>().BarrierHurt(7f);
        }
        else if (collision.CompareTag("Boss") && GameObject.Find("Barrier") == null)
        {
            Debug.Log("boss");
            collision.GetComponent<Boss>().Hurt(5f);
        }

    }
}

