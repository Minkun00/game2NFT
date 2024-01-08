using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    bool isHurt;
    bool isknockback = false;
    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //무기와 충돌시 피격(rigidbody2D)
    //    if (collision.gameObject.tag == "dfd")
    //    {
    //        Hurt(damage, collision.transform.position);
    //    }
    //}
    

    //player script와 겹침. 한 곳에 정리?
    public void Hurt(float damage, Vector2 pos)
    {
        if (!isHurt)
        {
            float currentHealth = HP.health;
            isHurt = true;
            currentHealth -= damage;
            HP.health = currentHealth;

            if (currentHealth <= 0)
            {
                anim.SetBool("isDied", true);
                gameObject.SetActive(false);
            }
            else
            {
                float x = transform.position.x - pos.x;
                float y = transform.position.y - pos.y;

                if (x < 0)
                    x = 1;
                else
                    x = -1;

                if (y < 0)
                    y = 1;
                else
                    y = -1;

                StartCoroutine(Knockback(x, y));
                StartCoroutine(HurtRoutine());
                StartCoroutine(alphablink());
            }
        }
    }

    IEnumerator Knockback(float x_dir, float y_dir) // 피격 시 밀쳐지는 효과
    {
        isknockback = true;
        Vector2 knockbackVelocity = new Vector2(x_dir, y_dir) * 0.2f;

        rigid.velocity = knockbackVelocity;

        float ctime = 0;

        while (ctime < 0.2f)
        {
            ctime += Time.deltaTime;
            yield return null;
        }
        rigid.velocity = Vector2.zero;
        isknockback = false;
    }

    IEnumerator alphablink() //피격 시 깜빡이는 효과
    {
        while (isHurt)
        {
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = halfA;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = fullA;
        }
    }

    IEnumerator HurtRoutine() //피격시 3초 동안 무적
    {
        yield return new WaitForSeconds(3f);
        isHurt = false;
    }

}
