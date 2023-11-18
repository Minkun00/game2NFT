using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondGoNextScene : MonoBehaviour
{
    public string NextSceneNameSecond;  // 이동할 맵의 이름
    public int currentStage;

    private PlayerMove thePlayerSecond;
    private MainCamera theCameraSecond;
    private ActionController theActionControllerSecond;

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
            GlobalControl.Instance.CurrentPhase = currentStage;

            thePlayerSecond.playerCurrentMapSecond = NextSceneNameSecond;
            GlobalControl.Instance.loadingSceneName = NextSceneNameSecond;

            // 플레이어 오브젝트를 찾아서 GlobalControl 인스턴스에 저장
            GameObject playerObjectSecond = GameObject.FindWithTag("Player");

            if (playerObjectSecond != null)
            {
                GlobalControl.Instance.playerObjectSecond = playerObjectSecond;
                playerObjectSecond.SetActive(false);
            }
            SceneManager.LoadScene("Loading");
        }
    }


}
