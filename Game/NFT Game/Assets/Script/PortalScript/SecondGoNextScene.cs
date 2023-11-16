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
            thePlayerSecond.playerCurrentMapSecond = NextSceneNameSecond;
            GlobalControl.Instance.loadingSceneNameSecond = NextSceneNameSecond;

            // �÷��̾� ������Ʈ�� ã�Ƽ� GlobalControl �ν��Ͻ��� ����
            GameObject playerObjectSecond = GameObject.FindWithTag("Player");
            Debug.Log("thePlayerSecond.playerCurrentMapSecond: " + thePlayerSecond.playerCurrentMapSecond);

            if (playerObjectSecond != null)
            {
                GlobalControl.Instance.playerObjectSecond = playerObjectSecond;
                playerObjectSecond.SetActive(false);
                Debug.Log("thePlayerSecond.playerCurrentMapSecond: " + thePlayerSecond.playerCurrentMapSecond);
            }
            SceneManager.LoadScene("LoadingSecond");
        }
    }


}
