using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public Slider barrierslider;
    public Slider hpslider;
    public float currentHP;
    public float currentBarrier;
    public bool Isattacking2;
    public bool Isattacking4_1;
    public bool Isattacking4_2;

    float max = 100f;

    Animator anim;
    Rigidbody2D rigid;
    Transform razer;
    
    void Awake()
    {
        anim = GetComponent <Animator>();
        rigid = GetComponent<Rigidbody2D>();
        razer = transform.GetChild(2);

        currentBarrier = max;
        currentHP = max;

        Isattacking2 = false;
        Isattacking4_2 = false;
        Isattacking4_1 = false;

        anim.SetBool("BossWalk", false);
        StartCoroutine("Attack1");

    }

    void Update()
    {
        barrierslider.value = currentBarrier / max;
        hpslider.value = currentHP / max;
        if (currentHP <= 0)
        {
            StopCoroutine("Attack1");
        }
    }
        
    public void BarrierHurt(float damage)
    {
        currentBarrier -= damage;
        if(currentBarrier <= 0)
        {
            Destroy(GameObject.Find("Barrier"));
        }
    }

    public void Hurt(float damage)
    {
        currentHP -= damage;
        anim.SetTrigger("BossAttacked");
        if(currentHP <= 0)
        {
            StopCoroutine("Attack1");
            StopCoroutine("Attack2");
            StopCoroutine("Attack4");
            anim.SetBool("BossDied",true);
            Destroy(GameObject.Find("Bullet"));
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            Invoke("DestroyBoss", 2f);
            //Show Game End Scene 
        }

    }

    void DestroyBoss()
    {
        SceneManager.LoadScene("EndScene");
    }

    IEnumerator Attack1()
    {
        anim.SetTrigger("BossAttack1");
        Invoke("Bullet", 0.67f);
        yield return new WaitForSeconds(2f);
        StartCoroutine("Attack2");
    }

    void Bullet()
    {
        GameObject.Find("Bullet").GetComponent<Bullet>().Fire();
    }

    IEnumerator Attack2()
    {
        Isattacking2 = true;
        anim.SetTrigger("BossAttack2");
        yield return new WaitForSeconds(1.5f);
        Isattacking2 = false;
        StartCoroutine("Attack4");
    }


    IEnumerator Attack4()
    {
        Isattacking4_1 = true;
        anim.SetTrigger("BossAttack4");
        Invoke("wait", 0.07f);
        yield return new WaitForSeconds(3f);
        Isattacking4_2 = false;
        StartCoroutine("Attack1");
    }

    void wait()
    {
        Isattacking4_1 = false;
        Isattacking4_2 = true;
    }

}
