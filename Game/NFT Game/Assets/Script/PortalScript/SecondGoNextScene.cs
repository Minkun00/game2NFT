using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondGoNextScene : MonoBehaviour
{
    public string NextSceneNameSecond;  // �̵��� ���� �̸�

    private PlayerMove thePlayerSecond;
    private MainCamera theCameraSecond;

    void Awake()
    {
        thePlayerSecond = PlayerMove.Instance;
        theCameraSecond = FindObjectOfType<MainCamera>();
        Debug.LogError("thePlayerSecond.playerCurrentMapSecond: " + thePlayerSecond.playerCurrentMapSecond);

    }


    private bool isPlayerOnPortalSecond = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPortalSecond = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPortalSecond = false;
        }
    }

    private void Update()
    {
        MovingPortalSecond();
    }

    void MovingPortalSecond()
    {
        if (isPlayerOnPortalSecond && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Attempting to move to next scene...");

            thePlayerSecond.playerCurrentMapSecond = NextSceneNameSecond;
            Debug.LogError("thePlayerSecond.playerCurrentMapSecond: " + thePlayerSecond.playerCurrentMapSecond);
            GlobalControl.Instance.loadingSceneNameSecond = NextSceneNameSecond;

            // �÷��̾� ������Ʈ�� ã�Ƽ� GlobalControl �ν��Ͻ��� ����
            GameObject playerObjectSecond = GameObject.FindWithTag("Player");
            Debug.Log("playerObject: " + playerObjectSecond);
            if (playerObjectSecond != null)
            {
                GlobalControl.Instance.playerObjectSecond = playerObjectSecond;
                playerObjectSecond.SetActive(false);
            }
            Debug.Log("Next Scene: " + NextSceneNameSecond);

            SceneManager.LoadScene("LoadingSecond");
        }
    }


}
