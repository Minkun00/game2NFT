using Newtonsoft.Json; // Json.NET 네임스페이스 추가
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PortalMapping
{
    public Dictionary<string, Dictionary<string, string>> map;
}

public class Portal : MonoBehaviour
{
    public TextAsset jsonFile; // JSON 파일
    private PortalMapping portalMap;

    private PlayerMove thePlayer;
    private MainCamera theCamera;

    public string currentLocation;
    public string nextLocationKey;

    void Start()
    {
        portalMap = JsonConvert.DeserializeObject<PortalMapping>(jsonFile.text); // Json.NET 사용

        thePlayer = PlayerMove.Instance;
        theCamera = FindObjectOfType<MainCamera>();
    }

    private void Update()
    {
        UsePortal(currentLocation, nextLocationKey);
    }

    // 포탈 사용 메서드
    public void UsePortal(string currentLocation, string nextLocationKey)
    {
        // 목적지 확인
        string destination = portalMap.map[currentLocation][nextLocationKey];
        

        // 여기서 플레이어를 'destination'으로 이동시키는 로직을 구현합니다.
        if (isPlayerOnPortal && Input.GetKeyDown(KeyCode.UpArrow))
        {
            thePlayer.playerCurrentMap = currentLocation;
            GlobalControl.Instance.loadingSceneName = destination;
            GameObject playerObject = GameObject.FindWithTag("Player");


            // 플레이어 오브젝트를 찾아서 GlobalControl 인스턴스에 저장
            if (playerObject != null)
            {
                GlobalControl.Instance.playerObject = playerObject;
                playerObject.SetActive(false);

            }
            SceneManager.LoadScene("Loading");
        }
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
}
