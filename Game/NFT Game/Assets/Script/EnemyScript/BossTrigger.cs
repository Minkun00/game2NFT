using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {   
            if (gameObject.CompareTag("Combat")) //currentBarrier������ ���ǰɱ�
            {
                //BossAttack2�� ������ �������� �ݺ�.
                GameObject.Find("Boss").GetComponent<Boss>().Attack2();
            }
            else if (gameObject.CompareTag("Attack"))
            {
                // ���� ���¸� true�� �����ϰ� ���� ���¸� false�� ����
                GameObject.Find("Boss").GetComponent<Boss>().Attack4();
            }
            else // Combat�� Attack�� �浹���� �ʾ��� ��s
            {
                GameObject.Find("Boss").GetComponent<Boss>().Idle();
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject bossObject = GameObject.FindWithTag("Boss");
        // Ʈ���� ������ ��� �� combat �� attacking ���¸� �缳���Ͽ� ��Ȯ�� ��ȯ.
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
