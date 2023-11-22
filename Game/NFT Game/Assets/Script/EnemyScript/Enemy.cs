using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 10f;
    Animator anim;
    Rigidbody2D rigid;
    public int nextMove;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent < Rigidbody2D> ();

        Invoke("Think", 5);
    }
    
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down,1,LayerMask.GetMask("Ground"));
        if(rayHit.collider == null)
        {
            nextMove = nextMove * 1;
            CancelInvoke();
            Invoke("Think", 5);
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);

        Invoke("Think", 5);

    }

    public void Combat()
    {
        anim.SetBool("isCombating", true);
        anim.SetBool("isAttacking", false);
    }

    public void Attack()
    {           
        anim.SetBool("isAttacking", true);
        anim.SetBool("isCombating", false);
    }

    public void Idle()
    {
        anim.SetBool("isCombating", false);
        anim.SetBool("isAttacking", false);
    }
}
