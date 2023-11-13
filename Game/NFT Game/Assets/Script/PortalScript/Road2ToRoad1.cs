using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Road2ToRoad1 : MonoBehaviour
{
    // 이동할 다음 씬의 이름
    public string nextSceneName = "CaveRoad1";
    // Player의 목적지 위치
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
            // 다음 씬을 현재 씬과 함께 로드
            SceneManager.LoadScene(nextSceneName);
            Debug.Log("LoadScene");

            successPortal = true;
            Debug.Log("successPortalTrue");

        }
    }
}
