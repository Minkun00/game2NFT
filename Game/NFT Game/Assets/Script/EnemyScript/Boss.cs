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
    
    void Awake()
    {
        anim = GetComponent <Animator>();
        rigid = GetComponent<Rigidbody2D>();

        currentBarrier = max;
        currentHP = max;

        anim.SetBool("BossWalk", false);
        StartCoroutine("Attack1");
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

    IEnumerator Attack1()
    {
        anim.SetBool("BossAttack1", true);
        Invoke("Wait", 1);
        yield return new WaitForSeconds(4f);
        StartCoroutine("Attack1");
    }

    void Wait()
    {
        anim.SetBool("BossAttack1",false);
    }
}
