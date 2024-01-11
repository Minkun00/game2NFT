using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    void Update()
    {
        if (gameObject.CompareTag("Combat") && )
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            if (gameObject.CompareTag("Combat"))
            {
                //BossAttack2만 랜덤한 간격으로 반복.
                StartCoroutine("Razer");
            }
        }

        IEnumerator Razer()
        {
            float nextTime = Random.Range(1f, 8f);
            anim.SetTrigger("BossAttack2");
            yield return new WaitForSeconds(nextTime);
            StartCoroutine("Razer");
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
