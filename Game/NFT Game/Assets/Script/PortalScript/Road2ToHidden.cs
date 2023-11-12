using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Road2ToHidden : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾ ��Ż�� �����ϸ� Hidden ������ �̵�
            SceneManager.LoadScene("Hidden");
        }
    }
}
