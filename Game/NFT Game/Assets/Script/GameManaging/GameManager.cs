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

    public List<Item> itemList; // ������ ����Ʈ
    public float minX, maxX, minY, maxY; // �������� ����� �� �ִ� ����

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

    void SpawnRandomItem()
    {
        // ������ ����Ʈ���� �������� �������� ����
        if (itemList.Count > 0)
        {
            Item newItem = itemList[Random.Range(0, itemList.Count)];

            // �������� ��ġ�� ����
            Vector3 dropPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);

            // �������� �ν��Ͻ�ȭ
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