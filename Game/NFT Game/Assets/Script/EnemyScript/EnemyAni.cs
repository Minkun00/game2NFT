using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAni : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("0");        
        anim.SetBool("isCombating", true);
        anim.SetBool("isAttacking", false);

        if (collision.CompareTag("Combat"))
        {
            Debug.Log("1");
            anim.SetBool("isCombating", true);
        }
        else if (collision.CompareTag("Attack"))
        {
            Debug.Log("2");
            anim.SetBool("isAttacking", true);
        }


    }
}
