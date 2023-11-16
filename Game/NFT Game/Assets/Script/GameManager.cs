using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    //�ٸ� ��ũ��Ʈ�鿡�� GameManger�� ������ �� �ֵ��� �ϱ� ���� ���� ������ �ν��Ͻ�
    private static GameManager instance;

    public static GameManager Instance 
    {  
        get 
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        } 
    }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;   

            DontDestroyOnLoad(gameObject);

        }
        else if (instance != this)
        {
            //�̹� �ν���Ʈ �����, ���� ������ GameManager �ı�
            Destroy(gameObject);
        }
    }

    private bool isGameOver = false;

    // ���� ���� ���·� ��ȯ�ϴ� �޼��� 
    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("GAME OVER!");

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
