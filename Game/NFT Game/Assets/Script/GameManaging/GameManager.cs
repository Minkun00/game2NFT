using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    private Vector3 initialPosition;
    private float health;
    //private int score; //���ھ� ����� ��� 

    private void Awake()
    {
        gm = this;
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

    //�÷��̾� ���� �ʱ�ȭ
    public void ResetPlayerState()
    {
        Debug.Log("reset");
        transform.position = initialPosition; //�÷��̾� ��ġ �ʱ�ȭ
        health = 100f;
        Debug.Log(transform.position);
        Debug.Log(health);
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