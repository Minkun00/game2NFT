using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossToInCave : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾ ��Ż�� �����ϸ� InCave ������ �̵�
            SceneManager.LoadScene("InCave");
        }
    }
}
