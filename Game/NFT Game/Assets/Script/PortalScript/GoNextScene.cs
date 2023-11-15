using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoNextScene : MonoBehaviour
{
    public string NextSceneName;  // 이동할 맵의 이름

    private PlayerMove thePlayer;
    private MainCamera theCamera;

    void Start()
    {
        thePlayer = PlayerMove.Instance;
        theCamera = FindObjectOfType<MainCamera>();
    }


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
            Debug.Log("Attempting to move to next scene...");

            Debug.Log("thePlayer: " + thePlayer);
            thePlayer.playerCurrentMap = NextSceneName;
            Debug.Log("GlobalControl.Instance: " + GlobalControl.Instance);
            GlobalControl.Instance.loadingSceneName = NextSceneName;

            // 플레이어 오브젝트를 찾아서 GlobalControl 인스턴스에 저장
            GameObject playerObject = GameObject.FindWithTag("Player");
            Debug.Log("playerObject: " + playerObject);
            if (playerObject != null)
            {
                GlobalControl.Instance.playerObject = playerObject;
                playerObject.SetActive(false);
            }

            SceneManager.LoadScene("Loading");
        }
    }


}
