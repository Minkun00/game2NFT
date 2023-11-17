using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FourthGoNextScene : MonoBehaviour
{
    public string NextSceneNameFourth;  // �̵��� ���� �̸�

    private PlayerMove thePlayerFourth;
    private MainCamera theCameraFourth;

    void Awake()
    {
        thePlayerFourth = PlayerMove.Instance;
        theCameraFourth = FindObjectOfType<MainCamera>();
    }


    private bool isPlayerOnPortalFourth = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPortalFourth = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPortalFourth = false;
        }
    }

    private void Update()
    {
        MovingPortalFourth();
    }

    void MovingPortalFourth()
    {
        if (isPlayerOnPortalFourth && Input.GetKeyDown(KeyCode.UpArrow))
        {
            thePlayerFourth.playerCurrentMapFourth = NextSceneNameFourth;
            GlobalControl.Instance.loadingSceneName = NextSceneNameFourth;

            // �÷��̾� ������Ʈ�� ã�Ƽ� GlobalControl �ν��Ͻ��� ����
            GameObject playerObjectFourth = GameObject.FindWithTag("Player");

            if (playerObjectFourth != null)
            {
                GlobalControl.Instance.playerObjectFourth = playerObjectFourth;
                playerObjectFourth.SetActive(false);
            }
            SceneManager.LoadScene("Loading");
        }
    }


}
