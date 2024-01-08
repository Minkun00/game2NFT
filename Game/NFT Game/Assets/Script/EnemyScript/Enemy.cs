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
    //    //����� �浹�� �ǰ�(rigidbody2D)
    //    if (collision.gameObject.tag == "dfd")
    //    {
    //        Hurt(damage, collision.transform.position);
    //    }
    //}
    

    //player script�� ��ħ. �� ���� ����?
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

    IEnumerator Knockback(float x_dir, float y_dir) // �ǰ� �� �������� ȿ��
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

    IEnumerator alphablink() //�ǰ� �� �����̴� ȿ��
    {
        while (isHurt)
        {
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = halfA;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = fullA;
        }
    }

    IEnumerator HurtRoutine() //�ǰݽ� 3�� ���� ����
    {
        yield return new WaitForSeconds(3f);
        isHurt = false;
    }

}
