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
        // JSON ���ϰ� �� ���� Ȯ��
        if (jsonFile == null)
        {
            Debug.LogError("JSON ������ �Ҵ���� �ʾҽ��ϴ�.");
            return;
        }
        if (portalMap == null)
        {
            Debug.LogError("portalMap�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return;
        }
        if (portalMap.map == null)
        {
            Debug.LogError("portalMap�� map�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return;
        }

        // Ű ��ȿ�� �˻�
        if (!portalMap.map.ContainsKey(currentLocation))
        {
            Debug.LogError("currentLocation Ű�� �ʿ� �������� �ʽ��ϴ�: " + currentLocation);
            return;
        }
        if (!portalMap.map[currentLocation].ContainsKey(nextLocationKey))
        {
            Debug.LogError("nextLocationKey Ű�� currentLocation �ʿ� �������� �ʽ��ϴ�: " + nextLocationKey);
            return;
        } 

        // ������ Ȯ��
        string destination = portalMap.map[currentLocation][nextLocationKey];
        Debug.Log("������: " + destination);

        // ���⼭ �÷��̾ 'destination'���� �̵���Ű�� ������ �����մϴ�.
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
