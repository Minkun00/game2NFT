using UnityEngine;
using UnityEngine.SceneManagement;

public class TownToRoad1 : MonoBehaviour
{
    private bool isPlayerOnPortal = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPortal = true;
            Debug.Log("Player");
        }
    }

    void MovingPortal()
    {
        if (isPlayerOnPortal && Input.GetKeyDown(KeyCode.UpArrow))
        {
            SceneManager.LoadScene("CaveRoad1");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPortal = false;
            Debug.Log("PlayerOut");
        }
    }

    private void Update()
    {
        MovingPortal();
    }
    void Start()
    {
        // 이벤트 핸들러 등록
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 씬 로드
        MovingPortal();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드되면 오브젝트의 위치를 변경
        GameObject player = GameObject.Find("Player"); // 오브젝트의 이름을 실제 사용하는 오브젝트의 이름으로 변경
        if (player != null)
        {
            player.transform.position = new Vector3(0f, 9f, 0f);
        }

        // 이벤트 핸들러 제거 (선택사항)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
