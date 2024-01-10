using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rigid;
    
    void Awake()
    {
        anim = GetComponent <Animator>();
        rigid = GetComponent<Rigidbody2D>();

    }

    //void Hurt_Barrier()
    //{
    //    if (collision.gameObject.tag == "sword")
    //    {
    //        Barrier.health -= 10f; //can change damage
    //    }
    //}
}
