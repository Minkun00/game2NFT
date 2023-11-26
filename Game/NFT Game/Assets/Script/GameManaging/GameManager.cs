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

    public List<Item> itemList; // 아이템 리스트
    public float minX, maxX, minY, maxY; // 아이템을 드롭할 수 있는 영역

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


        SpawnRandomItem();


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

    void SpawnRandomItem()
    {
        // 아이템 리스트에서 무작위로 아이템을 선택
        if (itemList.Count > 0)
        {
            Item newItem = itemList[Random.Range(0, itemList.Count)];

            // 아이템의 위치를 결정
            Vector3 dropPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);

            // 아이템을 인스턴스화
            if (newItem.itemPrefab != null)
            {
                ItemPickUp droppedItem = Instantiate(newItem.itemPrefab, dropPosition, Quaternion.identity).GetComponent<ItemPickUp>();
                droppedItem.item = newItem;
            }
            else
            {
                Debug.LogError("Item prefab is null. Please check the item prefab.");
            }
        }
        else
        {
            Debug.LogError("Item list is empty. Please add items to the item list.");
        }
    }



}