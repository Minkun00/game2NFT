using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Slider slider;
    float maxHP = 100f;
    public float currentHP;
    public float damage = 10f;
    public int nextMove;
    
    Animator anim;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHP = maxHP;
        Invoke("Think", 5);
        anim.SetBool("isRunning", false);
    }

    void FixedUpdate()
    {
        //도적의 움직임제어
        //if문은 트리거 2개에 모두 충돌하지 않을 때
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
        //속도0으로 움직임X
        else
        {
            rigid.velocity = Vector2.zero;
        }
        slider.value = currentHP / maxHP;

    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);

        //방향
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;
        //벡터값 0일때 애니메이션이 작동중이라면 중지
        if (nextMove == 0)
        {
            if (anim.GetBool("isRunning") == true)
                anim.SetBool("isRunning", false);
        }
        //벡터값이 0이 아닐때는 애니메이션 작동
        else
        {
            anim.SetBool("isRunning", true);
        }

        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
    }

    void Turn()
    {
        nextMove = nextMove * -1;
        CancelInvoke();
        Think();
        spriteRenderer.flipX = nextMove == 1;

    }

    bool isHurt;
    bool isknockback = false;
    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    //enemy hurt
    public void Hurt(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            anim.SetTrigger("E_Death");
            Destroy(gameObject, 4);
        }
    }

    //For Trigger Script
    public void Combat()
    {
        anim.SetBool("isCombating", true);
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", false);
    }

    public void Attack()
    {
        anim.SetBool("isAttacking", true);
        anim.SetBool("isRunning", false);
        anim.SetBool("isCombating", false);

    }

    public void Idle()
    {
        if (nextMove == 0)
            anim.SetBool("isRunning", false);
        else
            anim.SetBool("isRunning", true);

        anim.SetBool("isCombating", false);
        anim.SetBool("isAttacking", false);

    }

}
