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
                //BossAttack2�� ������ �������� �ݺ�.
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
        // Ʈ���� ������ ��� �� combat �� attacking ���¸� �缳���Ͽ� ��Ȯ�� ��ȯ.
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
