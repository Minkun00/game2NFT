using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Slider barrierslider;
    public Slider hpslider;
    public float currentHP;
    public float currentBarrier;
    
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

        anim.SetBool("BossWalk", false);
        StartCoroutine("Attack1");
        razer.GetComponent<BoxCollider>().enabled = false;
    }

    void Update()
    {
        barrierslider.value = currentBarrier / max;
        hpslider.value = currentHP / max;
        
        if(currentHP<=0)
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
            anim.SetTrigger("BossDied");
            //Show Game End Scene 
        }
    }

    public void Idle()
    {
        StopCoroutine("Razer");
    }

    public void outOfRange()
    {
        StopCoroutine("ShortRazer");
    }

    IEnumerator Attack1()
    {
        anim.SetTrigger("BossAttack1");
        yield return new WaitForSeconds(3f);
        StartCoroutine("Attack1");
    }

    public void Attack2()
    {
        StartCoroutine(Razer());
    }

    IEnumerator Razer()
    {
        Debug.Log("razer");
        anim.SetTrigger("BossAttack2");
        razer.GetComponent<BoxCollider>().enabled = true;
        yield return new WaitForSeconds(10f);
        StartCoroutine("Razer");
    }

    public void Attack4()
    {
        StartCoroutine(ShortRazer());
    }

    IEnumerator ShortRazer()
    {
        anim.SetTrigger("BossAttack4");
        yield return new WaitForSeconds(5f);
        StartCoroutine("ShortRazer");
    }
}
