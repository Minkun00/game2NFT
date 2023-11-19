using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    private Vector3 initialPosition;
    private float health;
    //private int score; //스코어 사용할 경우 

    private void Awake()
    {
        gm = this;
        initialPosition = transform.position;
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

    //플레이어 상태 초기화
    public void ResetPlayerState()
    {
        Debug.Log("reset");
        transform.position = initialPosition; //플레이어 위치 초기화
        health = 100f;
        Debug.Log(transform.position);
        Debug.Log(health);
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