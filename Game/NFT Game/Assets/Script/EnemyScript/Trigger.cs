using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//combatmode, attackmode 애니메이션 활성화를 위한 스크립트
public class Trigger : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        //enemy를 parent로 두고 combatmode와 attackmode를 넣으면 enemy tag 그대로 가져감.
        //별개로 두어야 함. 종속시키면 안됨.
        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Combat"))
            {
                // 전투 상태를 true로 설정하고 공격 상태를 false로 설정
                GameObject.Find("Enemy").GetComponent<Enemy>().Combat();
            }
            else if (gameObject.CompareTag("Attack"))
            {
                // 공격 상태를 true로 설정하고 전투 상태를 false로 설정
                GameObject.Find("Enemy").GetComponent<Enemy>().Attack();
            }
            else // Combat도 Attack도 충돌하지 않았을 때
            {
                GameObject.Find("Enemy").GetComponent<Enemy>().Idle();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject enemyObject = GameObject.FindWithTag("Enemy");
        // 트리거 영역을 벗어날 때 combat 및 attacking 상태를 재설정하여 정확한 전환.
        if (enemyObject != null && enemyObject.activeSelf)
        {
            Enemy enemystate = enemyObject.GetComponent<Enemy>();
            if (gameObject.CompareTag("Combat"))
            {
                enemystate.Idle();
            }
            else if (gameObject.CompareTag("Attack"))
            {
                enemystate.Combat();
            }
        }
    }
}



//private void OnTriggerExit2D(Collider2D collision)
//{

//    // 트리거 영역을 벗어날 때 combat 및 attacking 상태를 재설정하여 정확한 전환.
//    if (gameObject.CompareTag("Combat"))
//    {
//        GameObject.Find("Enemy").GetComponent<Enemy>().Idle();
//    }
//    else if (gameObject.CompareTag("Attack"))
//    {
//        GameObject.Find("Enemy").GetComponent<Enemy>().Combat();
//    }
//}