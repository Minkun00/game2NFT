using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Road2ToRoad1 : MonoBehaviour
{
    // �̵��� ���� ���� �̸�
    public string nextSceneName = "CaveRoad1";
    // Player�� ������ ��ġ
    public Vector3 playerDestination;
    public Scene scene;

    private bool isPlayerOnPortal = false;
    public bool successPortal = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPortal = true;
            Debug.Log("inPlayer");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPortal = false;
            Debug.Log("outPlayer");

        }
    }

    void Update()
    {
        movePortal();
    }

    public void movePortal()
    {
        if (isPlayerOnPortal && Input.GetKeyDown(KeyCode.UpArrow))
        {
            // ���� ���� ���� ���� �Բ� �ε�
            SceneManager.LoadScene(nextSceneName);
            Debug.Log("LoadScene");

            successPortal = true;
            Debug.Log("successPortalTrue");

        }
    }
}
