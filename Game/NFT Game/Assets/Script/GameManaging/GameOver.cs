using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");

        // �÷��̾� ������Ʈ�� ã�Ƽ� GlobalControl �ν��Ͻ��� ����
        if (playerObject != null)
        {
            GameManager.Instance.GameOverPlayer = playerObject;
            playerObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Town");
            //ĳ���� �ҷ����� �ʿ�

        }
    }
}
