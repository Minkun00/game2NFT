using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAni : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Combat"))
            {
                Debug.Log("전투와 충돌함");
                // 전투 상태를 true로 설정하고 공격 상태를 false로 설정
                GameObject.Find("Enemy").GetComponent<Enemy>().Combat();
            }
            else if (gameObject.CompareTag("Attack")||gameObject.CompareTag("Damage"))
            {
                Debug.Log("공격과 충돌함");
                // 공격 상태를 true로 설정하고 전투 상태를 false로 설정
                GameObject.Find("Enemy").GetComponent<Enemy>().Attack();
            }
            else
            {
                GameObject.Find("Enemy").GetComponent<Enemy>().Idle();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    // 트리거 영역을 벗어날 때 combat 및 attacking 상태를 재설정하여 정확한 전환.
        if (gameObject.CompareTag("Combat"))
        {
            GameObject.Find("Enemy").GetComponent<Enemy>().Idle();
        }
        else if (gameObject.CompareTag("Attack"))
        {
            GameObject.Find("Enemy").GetComponent<Enemy>().Combat();
        }
    }
}



