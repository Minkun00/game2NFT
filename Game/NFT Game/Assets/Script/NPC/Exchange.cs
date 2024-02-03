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
    public Inventory inventory;

    public GameObject exchangeGrid;
    public TextMeshProUGUI[] ItemListUp;
    public Button[] BtnListUP;


    public TextMeshProUGUI copiedTextUI; // 복사 알림을 표시할 Text UI를 연결해주세요.
    public GameObject copiedPanel; // 복사 알림 패널을 연결해주세요.


    private void Start()
    {
        copiedPanel.SetActive(false);

        for (int i = 0; i < exchangeGrid.transform.childCount; i++)
        {
            // Panel (i) 오브젝트를 찾습니다.
            GameObject panel = exchangeGrid.transform.GetChild(i).gameObject;

            // item 오브젝트 (TextMeshProUGUI 컴포넌트를 가진 오브젝트)를 찾아 ItemListUp에 할당합니다.
            TextMeshProUGUI itemText = panel.transform.Find("item").GetComponent<TextMeshProUGUI>();
            if (itemText != null && i < ItemListUp.Length)
            {
                ItemListUp[i] = itemText;
            }

            // 해당 Panel 내의 Button 컴포넌트를 찾아 BtnListUP에 할당합니다.
            // 예를 들어, 버튼이 "button"이라는 이름으로 Panel 내에 있다고 가정합니다.
            Button button = panel.transform.Find("CopyBtn").GetComponent<Button>();
            if (button != null && i < BtnListUP.Length)
            {
                BtnListUP[i] = button;
            }
        }
    }


    private void OnMouseDown()
    {
        if (exchangeUI != null && !uiManager.guidePanel.activeSelf && !uiManager.completePanel.activeSelf)
        {
            bool isActive = exchangeUI.activeSelf;
            exchangeUI.SetActive(!isActive); // UI의 활성화 상태를 토글합니다.

            // CurItemList의 크기에 따라 UI 요소를 활성화 또는 비활성화합니다.
            for (int i = 0; i < inventory.CurItemList.Count; i++)
            {
                if (i < inventory.CurItemList.Count)
                {
                    CurrentItemList currentItem = inventory.CurItemList[i];
                    string itemInfo = "[" + currentItem.Rank + "] " + currentItem.Adjective + " " + currentItem.ItemName + " " + currentItem.ItemPart;
                    ItemListUp[i].text = (i + 1) + ". " + itemInfo;

                    BtnListUP[i].onClick.RemoveAllListeners();
                    string curItemCode = currentItem.ItemCode;
                    BtnListUP[i].onClick.AddListener(() => copyCode(curItemCode, itemInfo));
                }
                else
                {
                    ItemListUp[i].gameObject.SetActive(false); // 비활성화
                    BtnListUP[i].gameObject.SetActive(false); // 비활성화
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
