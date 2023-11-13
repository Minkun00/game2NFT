using System.Collections;
using System.Collections.Generic;
// using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Road1ToTown : MonoBehaviour
{
    private bool isPlayerOnPortal = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPortal = true;
        }
    }
    void MovingPortal()
    {
        if (isPlayerOnPortal && Input.GetKeyDown(KeyCode.UpArrow))
        {
            SceneManager.LoadScene("Town");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPortal = false;
        }
    }
    private void Update()
    {
        MovingPortal();
    }

    void Start()
    {
        // �̺�Ʈ �ڵ鷯 ���
        SceneManager.sceneLoaded += OnSceneLoaded;

        // �� �ε�
        MovingPortal();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ε�Ǹ� ������Ʈ�� ��ġ�� ����
        GameObject player = GameObject.Find("Player"); // ������Ʈ�� �̸��� ���� ����ϴ� ������Ʈ�� �̸����� ����
        if (player != null)
        {
            player.transform.position = new Vector3(0f, 9f, 0f);
        }

        // �̺�Ʈ �ڵ鷯 ���� (���û���)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
