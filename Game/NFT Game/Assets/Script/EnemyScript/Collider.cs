using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider : MonoBehaviour
{
    bool attack2, attack4_1, attack4_2;

    void Update()
    {
        attack2 = GameObject.Find("Boss").GetComponent<Boss>().Isattacking2;
        attack4_1 = GameObject.Find("Boss").GetComponent<Boss>().Isattacking4_1;
        attack4_2 = GameObject.Find("Boss").GetComponent<Boss>().Isattacking4_2;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (attack2 == true)
            {
                GameObject.Find("Player").GetComponent<PlayerMove>().Hurt0(15f);
            }
            else if(attack4_1 == true)
            {
                GameObject.Find("Player").GetComponent<PlayerMove>().Hurt0(15f);

            }
            else if (attack4_2 == true)
            {
                GameObject.Find("Player").GetComponent<PlayerMove>().Hurt0(15f);

            }

        }
    }
}
