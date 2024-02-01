using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Vector3 initialPosition;
    private float health;
    public GameObject GameOverPlayer;

    //private int score; //���ھ� ����� ��� 

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        initialPosition = transform.position;
    }

    private bool isGameOver = false;
    // ���� ���� ���·� ��ȯ�ϴ� �޼��� 
    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            SceneManager.LoadScene("GameOver");
            //���⿡ ���� ������ ó���� �۾� ���߿� �߰�
        }
    }

    //���� ���� �޼���
    public void EndGame()
    {
        Debug.Log("THE END");
        //���⿡ ���� ���� �� ó���� �۾� �߰�

        Application.Quit(); //����� ���ӿ����� ����, �����Ϳ����� ���۾���.
        //���� �Լ� ȣ�� �� ���� �������� unity���ø����̼��̳� ������ �����.
        //���� ���� ����� ���

    }
}