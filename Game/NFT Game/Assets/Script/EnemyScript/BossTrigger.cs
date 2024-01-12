using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    Animator anim;
    float nextTime;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {   
            if (gameObject.CompareTag("Combat"))
            {
                //BossAttack2만 랜덤한 간격으로 반복.
                StartCoroutine(Razer());
            }
        }

        IEnumerator Razer()
        {
            anim.SetBool("BossAttack2", true);
            //next();
            yield return new WaitForSeconds(6f);
            StartCoroutine("Razer");
        }

    }


    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    GameObject bossObject = GameObject.FindWithTag("Boss");
    //    // 트리거 영역을 벗어날 때 combat 및 attacking 상태를 재설정하여 정확한 전환.
    //    if (bossObject != null && bossObject.activeSelf)
    //    {
    //        Enemy bossState = bossObject.GetComponent<Enemy>();
    //        if (gameObject.CompareTag("Combat"))
    //        {
    //            bossState.Idle();
    //        }
    //        else if (gameObject.CompareTag("Attack"))
    //        {
    //            bossState.Combat();
    //        }
    //    }
    //}
}
