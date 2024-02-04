using Newtonsoft.Json; // Json.NET ���ӽ����̽� �߰�
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
    public TextAsset jsonFile; // JSON ����
    private PortalMapping portalMap;

    private PlayerMove thePlayer;
    private MainCamera theCamera;

    public string currentLocation;
    public string nextLocationKey;

    void Start()
    {
        portalMap = JsonConvert.DeserializeObject<PortalMapping>(jsonFile.text); // Json.NET ���

        thePlayer = PlayerMove.Instance;
        theCamera = FindObjectOfType<MainCamera>();
    }

    private void Update()
    {
        UsePortal(currentLocation, nextLocationKey);
    }

    // ��Ż ��� �޼���
    public void UsePortal(string currentLocation, string nextLocationKey)
    {
        // ������ Ȯ��
        string destination = portalMap.map[currentLocation][nextLocationKey];
        

        // ���⼭ �÷��̾ 'destination'���� �̵���Ű�� ������ �����մϴ�.
        if (isPlayerOnPortal && Input.GetKeyDown(KeyCode.UpArrow))
        {
            thePlayer.playerCurrentMap = currentLocation;
            GlobalControl.Instance.loadingSceneName = destination;
            GameObject playerObject = GameObject.FindWithTag("Player");


            // �÷��̾� ������Ʈ�� ã�Ƽ� GlobalControl �ν��Ͻ��� ����
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
