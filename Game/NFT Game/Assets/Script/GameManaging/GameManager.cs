using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Vector3 initialPosition;
    private float health;
    //private int score; //���ھ� ����� ��� 


    // ������ �ڵ�
    public TextAsset txt;
    string[,] ItemCode;
    int lineSize, rowSize;

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

    public void Start()
    {
        string currentText = txt.text.Substring(0, txt.text.Length - 1);
        string[] line = currentText.Split('\n');
        lineSize = line.Length;
        rowSize = line[0].Split('\t').Length;
        ItemCode = new string[lineSize, rowSize];

        for(int i = 0; i < lineSize; i++)
        {
            string[] row = line[i].Split("\t");
            for(int j = 0; j < rowSize; j++)
            {
                ItemCode[i, j] = row[j];
                print(i + ", " + j + ", " + ItemCode[i, j]);
            }
        }

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