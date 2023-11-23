using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//combatmode, attackmode �ִϸ��̼� Ȱ��ȭ�� ���� ��ũ��Ʈ
public class Trigger : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        //enemy�� parent�� �ΰ� combatmode�� attackmode�� ������ enemy tag �״�� ������.
        //������ �ξ�� ��. ���ӽ�Ű�� �ȵ�.
        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Combat"))
            {
                // ���� ���¸� true�� �����ϰ� ���� ���¸� false�� ����
                GameObject.Find("Enemy").GetComponent<Enemy>().Combat();
            }
            else if (gameObject.CompareTag("Attack"))
            {
                // ���� ���¸� true�� �����ϰ� ���� ���¸� false�� ����
                GameObject.Find("Enemy").GetComponent<Enemy>().Attack();
            }
            else // Combat�� Attack�� �浹���� �ʾ��� ��
            {
                GameObject.Find("Enemy").GetComponent<Enemy>().Idle();
            }
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



//private void OnTriggerExit2D(Collider2D collision)
//{

//    // Ʈ���� ������ ��� �� combat �� attacking ���¸� �缳���Ͽ� ��Ȯ�� ��ȯ.
//    if (gameObject.CompareTag("Combat"))
//    {
//        GameObject.Find("Enemy").GetComponent<Enemy>().Idle();
//    }
//    else if (gameObject.CompareTag("Attack"))
//    {
//        GameObject.Find("Enemy").GetComponent<Enemy>().Combat();
//    }
//}