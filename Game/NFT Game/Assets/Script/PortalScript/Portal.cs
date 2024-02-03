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
        // JSON 파일과 맵 참조 확인
        if (jsonFile == null)
        {
            Debug.LogError("JSON 파일이 할당되지 않았습니다.");
            return;
        }
        if (portalMap == null)
        {
            Debug.LogError("portalMap이 초기화되지 않았습니다.");
            return;
        }
        if (portalMap.map == null)
        {
            Debug.LogError("portalMap의 map이 초기화되지 않았습니다.");
            return;
        }

        // 키 유효성 검사
        if (!portalMap.map.ContainsKey(currentLocation))
        {
            Debug.LogError("currentLocation 키가 맵에 존재하지 않습니다: " + currentLocation);
            return;
        }
        if (!portalMap.map[currentLocation].ContainsKey(nextLocationKey))
        {
            Debug.LogError("nextLocationKey 키가 currentLocation 맵에 존재하지 않습니다: " + nextLocationKey);
            return;
        } 

        // 목적지 확인
        string destination = portalMap.map[currentLocation][nextLocationKey];
        Debug.Log("목적지: " + destination);

        // 여기서 플레이어를 'destination'으로 이동시키는 로직을 구현합니다.
        if (isPlayerOnPortal && Input.GetKeyDown(KeyCode.UpArrow))
        {
            SceneManager.LoadScene(destination);
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
