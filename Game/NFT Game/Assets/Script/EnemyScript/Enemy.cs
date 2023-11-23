using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 10f;
    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public int nextMove;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 5);
        anim.SetBool("isRunning", false);
        Debug.Log(nextMove);
        Debug.Log(anim.GetBool("isRunning"));
    }

    void FixedUpdate()
    {
        //������ ����������
        //if���� Ʈ���� 2���� ��� �浹���� ���� ��
        if (anim.GetBool("isCombating") == false && anim.GetBool("isAttacking") == false)
        {
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

            Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
            Debug.DrawRay(frontVec, Vector3.down * 2, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Ground"));
            if (rayHit.collider == null)
            {
                Turn();
            }

        }
        //�ӵ�0���� ������X
        else
        {
            rigid.velocity = Vector2.zero;
        }

    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);
        Debug.Log(nextMove);

        //����
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;
        //���Ͱ� 0�϶� �ִϸ��̼��� �۵����̶�� ����
        if (nextMove == 0)
        {
            if (anim.GetBool("isRunning") == true)
                anim.SetBool("isRunning", false);
        }
        //���Ͱ��� 0�� �ƴҶ��� �ִϸ��̼� �۵�
        else
        {
            anim.SetBool("isRunning", true);
        }

        float nextThinkTime = Random.Range(2f, 5f);
        Debug.Log(nextThinkTime);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove = nextMove * -1;
        CancelInvoke();
        Think();
        spriteRenderer.flipX = nextMove == 1;

    }

    public void Combat()
    {
        //anim.SetInteger("RunSpeed", 0);
        anim.SetBool("isCombating", true);
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", false);
    }

    public void Attack()
    {
        //anim.SetInteger("RunSpeed", 0);
        anim.SetBool("isAttacking", true);
        anim.SetBool("isRunning", false);
        anim.SetBool("isCombating", false);

    }

    public void Idle()
    {
        //anim.SetInteger("RunSpeed", nextMove);
        if (nextMove == 0)
            anim.SetBool("isRunning", false);
        else
            anim.SetBool("isRunning", true);

        anim.SetBool("isCombating", false);
        anim.SetBool("isAttacking", false);

    }
}
