using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdGoNextScene : MonoBehaviour
{
    public string NextSceneNameThird;  // �̵��� ���� �̸�
    public int currentStage;

    private PlayerMove thePlayerThird;
    private MainCamera theCameraThird;

    void Awake()
    {
        thePlayerThird = PlayerMove.Instance;
        theCameraThird = FindObjectOfType<MainCamera>();
    }


    private bool isPlayerOnPortalThird = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPortalThird = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPortalThird = false;
        }
    }

    private void Update()
    {
        MovingPortalThird();
    }

    void MovingPortalThird()
    {
        if (isPlayerOnPortalThird && Input.GetKeyDown(KeyCode.UpArrow))
        {
            GlobalControl.Instance.CurrentPhase = currentStage;

            thePlayerThird.playerCurrentMapThird = NextSceneNameThird;
            GlobalControl.Instance.loadingSceneName = NextSceneNameThird;

            // �÷��̾� ������Ʈ�� ã�Ƽ� GlobalControl �ν��Ͻ��� ����
            GameObject playerObjectThird = GameObject.FindWithTag("Player");

            if (playerObjectThird != null)
            {
                GlobalControl.Instance.playerObjectThird = playerObjectThird;
                playerObjectThird.SetActive(false);
            }
            SceneManager.LoadScene("Loading");
        }
    }


}
