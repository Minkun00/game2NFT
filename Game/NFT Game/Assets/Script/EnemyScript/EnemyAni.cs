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
                Debug.Log("������ �浹��");
                // ���� ���¸� true�� �����ϰ� ���� ���¸� false�� ����
                GameObject.Find("Enemy").GetComponent<Enemy>().Combat();
            }
            else if (gameObject.CompareTag("Attack")||gameObject.CompareTag("Damage"))
            {
                Debug.Log("���ݰ� �浹��");
                // ���� ���¸� true�� �����ϰ� ���� ���¸� false�� ����
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
    // Ʈ���� ������ ��� �� combat �� attacking ���¸� �缳���Ͽ� ��Ȯ�� ��ȯ.
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



