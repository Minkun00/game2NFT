using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoNextScene : MonoBehaviour
{
    public string NextSceneName;  // �̵��� ���� �̸�

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

            // �÷��̾� ������Ʈ�� ã�Ƽ� GlobalControl �ν��Ͻ��� ����
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
