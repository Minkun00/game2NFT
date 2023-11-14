using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoNextScene : MonoBehaviour
{
    public string NextSceneName;  // 이동할 맵의 이름
    public string currentSceneName;
    private PlayerMove thePlayer;
    private FromPreviousScene theFromPreviousScene;

    private void Start()
    {
        thePlayer = FindAnyObjectByType<PlayerMove>();
        theFromPreviousScene = FindAnyObjectByType<FromPreviousScene>();
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
            thePlayer.playerCurrentMap = NextSceneName;
            thePlayer.playerPreviousMape = currentSceneName;
            SceneManager.LoadScene(NextSceneName);
            Debug.Log("theFromPreviousScene arriveMap: " + theFromPreviousScene.arriveMap + ", thePlayer.currentMapName: " + thePlayer.playerCurrentMap);
            Debug.Log("theFromPreviousScene previousMap: " + theFromPreviousScene.previousMap + ", thePlayer.previousMapName: " + thePlayer.playerPreviousMape);
        }
    }
}
