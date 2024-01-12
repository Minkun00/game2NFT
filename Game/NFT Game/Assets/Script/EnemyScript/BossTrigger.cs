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
                //BossAttack2�� ������ �������� �ݺ�.
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
    //    // Ʈ���� ������ ��� �� combat �� attacking ���¸� �缳���Ͽ� ��Ȯ�� ��ȯ.
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
