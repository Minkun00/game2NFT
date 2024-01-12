using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {   
            if (gameObject.CompareTag("Combat")) //currentBarrier값으로 조건걸기
            {
                //BossAttack2만 랜덤한 간격으로 반복.
                GameObject.Find("Boss").GetComponent<Boss>().Attack2();
            }
            else if (gameObject.CompareTag("Attack"))
            {
                // 공격 상태를 true로 설정하고 전투 상태를 false로 설정
                GameObject.Find("Boss").GetComponent<Boss>().Attack4();
            }
            else // Combat도 Attack도 충돌하지 않았을 때s
            {
                GameObject.Find("Boss").GetComponent<Boss>().Idle();
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject bossObject = GameObject.FindWithTag("Boss");
        // 트리거 영역을 벗어날 때 combat 및 attacking 상태를 재설정하여 정확한 전환.
        if (bossObject != null && bossObject.activeSelf)
        {
            Boss bossState = bossObject.GetComponent<Boss>();
            if (gameObject.CompareTag("Combat"))
            {
                bossState.Idle();
            }
            else if (gameObject.CompareTag("Attack"))
            {
                bossState.Attack2();
            }
        }
    }
}
