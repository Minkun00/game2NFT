using UnityEngine;
using UnityEngine.SceneManagement;

public class Road1ToRoad2 : MonoBehaviour
{
    private bool isPlayerOnPortal = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPortal = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPortal = false;
        }
    }

    private void Update()
    {
        MovingPortal();
    }

    void MovingPortal()
    {
        if (isPlayerOnPortal && Input.GetKeyDown(KeyCode.UpArrow))
        {
            SceneManager.LoadScene("CaveRoad1");
        }
    }
    void Start()
    {
        // 이벤트 핸들러 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 이벤트 핸들러 제거
        SceneManager.sceneLoaded -= OnSceneLoaded;
        // 씬이 로드되면 오브젝트의 위치를 변경
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            player.transform.position = new Vector3(0f, 0f, 0f);
        }

    }
}
