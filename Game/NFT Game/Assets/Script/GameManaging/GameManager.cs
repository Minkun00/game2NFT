using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private Vector3 initialPosition;
    private float health;
    //private int score; //스코어 사용할 경우 


    // 아이템 코드
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
    // 게임 오버 상태로 전환하는 메서드 
    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            SceneManager.LoadScene("GameOver");
            //여기에 게임 오버시 처리할 작업 나중에 추가
        }
    }

    //게임 종료 메서드
    public void EndGame()
    {
        Debug.Log("THE END");
        //여기에 게임 종료 시 처리할 작업 추가

        Application.Quit(); //빌드된 게임에서만 동작, 에디터에서는 동작안함.
        //위의 함수 호출 시 현재 실행중인 unity어플리케이션이나 게임이 종료됨.
        //게임 완전 종료시 사용

    }


}