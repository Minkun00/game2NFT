using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Exchange : MonoBehaviour
{
    public UIManager uiManager;

    public GameObject exchangeUI; // Exchange UI를 연결해주세요.

    public ActionController actionController;
    public ItemManager itemManager;

    public TextMeshProUGUI[] ItemListUp;
    public Button[] BtnListUP;


    public TextMeshProUGUI copiedTextUI; // 복사 알림을 표시할 Text UI를 연결해주세요.
    public GameObject copiedPanel; // 복사 알림 패널을 연결해주세요.


    private void Start()
    {
        copiedPanel.SetActive(false);
    }


    private void OnMouseDown()
    {
        if (exchangeUI != null && !uiManager.guidePanel.activeSelf && !uiManager.completePanel.activeSelf)
        {
            bool isActive = exchangeUI.activeSelf;
            exchangeUI.SetActive(!isActive); // UI의 활성화 상태를 토글합니다.

            // exchangeUI가 활성화될 때 curItemNameList의 항목들을 표시합니다.
            if (itemManager.itemList.Count > 0)
            {
                for (int i = 0; i < ItemListUp.Length; i++)
                {
                    if (i < itemManager.itemList.Count)
                    {
                        int number = i + 1;
                        string itemInfo = "[" + itemManager.itemList[i].Rank + "] " + itemManager.itemList[i].Adjective + " " + itemManager.itemList[i].ItemName + " " + itemManager.itemList[i].ItemPart;
                        ItemListUp[i].text = number + ". " + itemInfo;
                        BtnListUP[i].onClick.RemoveAllListeners();
                        string curItemCode = itemManager.itemList[i].ItemCode;
                        BtnListUP[i].onClick.AddListener(() => copyCode(curItemCode, itemInfo));
                    }
                    else
                    {
                        ItemListUp[i].text = "";
                    }
                }
            }
        }
    }

    private void Update()
    {
        GameObject player = GameObject.Find("Player");
        actionController = player.GetComponent<ActionController>();

        // ESC 키를 눌렀을 때 copiedPanel을 비활성화합니다.
        if (Input.GetKeyDown(KeyCode.Escape) && copiedPanel.activeSelf)
        {
            copiedPanel.SetActive(false);
        }
    }

    public void ExitExchange()
    {
        exchangeUI.SetActive(false);
    }

    public void copyCode(string _code, string _itemInfo)
    {
        GUIUtility.systemCopyBuffer = _code;
        copiedTextUI.text = _itemInfo + "\n코드가 복사되었습니다!";
        copiedPanel.SetActive(true); // 복사 알림 패널을 활성화합니다.
    }

    public void CloseCopiedPanel()
    {
        copiedPanel.SetActive(false); // 복사 알림 패널을 비활성화합니다.
    }
}
