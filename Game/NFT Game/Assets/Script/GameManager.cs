using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    //다른 스크립트들에서 GameManger에 접근할 수 있도록 하기 위한 정적 변수와 인스턴스
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
            //이미 인스턴트 존재시, 새로 생성된 GameManager 파괴
            Destroy(gameObject);
        }
    }

    private bool isGameOver = false;

    // 게임 오버 상태로 전환하는 메서드 
    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("GAME OVER!");

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
