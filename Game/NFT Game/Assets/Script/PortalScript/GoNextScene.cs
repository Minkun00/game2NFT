using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoNextScene : MonoBehaviour
{
    public string NextSceneName;  // 이동할 맵의 이름

    private PlayerMove thePlayer;
    private MainCamera theCamera;

    private void Start()
    {
        thePlayer = FindAnyObjectByType<PlayerMove>();
        theCamera = FindAnyObjectByType<MainCamera>();
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
            thePlayer.loadingSceneName = NextSceneName;
            SceneManager.LoadScene("Loading");
        }
    }
}
