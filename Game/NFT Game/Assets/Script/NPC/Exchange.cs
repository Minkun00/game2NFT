using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Exchange : MonoBehaviour
{
    public UIManager uiManager;

    public GameObject exchangeUI; // Exchange UI�� �������ּ���.

    public ActionController actionController;
    public ItemManager itemManager;
    public Inventory inventory;

    public TextMeshProUGUI[] ItemListUp;
    public Button[] BtnListUP;


    public TextMeshProUGUI copiedTextUI; // ���� �˸��� ǥ���� Text UI�� �������ּ���.
    public GameObject copiedPanel; // ���� �˸� �г��� �������ּ���.


    private void Start()
    {
        copiedPanel.SetActive(false);
    }


    private void OnMouseDown()
    {
        if (exchangeUI != null && !uiManager.guidePanel.activeSelf && !uiManager.completePanel.activeSelf)
        {
            bool isActive = exchangeUI.activeSelf;
            exchangeUI.SetActive(!isActive); // UI�� Ȱ��ȭ ���¸� ����մϴ�.

            // CurItemList�� �׸���� ǥ���մϴ�.
            if (inventory.CurItemList.Count > 0)
            {
                for (int i = 0; i < ItemListUp.Length; i++)
                {
                    if (i < inventory.CurItemList.Count)
                    {
                        int number = i + 1;
                        CurrentItemList currentItem = inventory.CurItemList[i];
                        string itemInfo = "[" + currentItem.Rank + "] " + currentItem.Adjective + " " + currentItem.ItemName + " " + currentItem.ItemPart;
                        ItemListUp[i].text = number + ". " + itemInfo;
                        BtnListUP[i].onClick.RemoveAllListeners();
                        string curItemCode = currentItem.ItemCode;
                        BtnListUP[i].onClick.AddListener(() => copyCode(curItemCode, itemInfo));
                    }
                    else
                    {
                        ItemListUp[i].text = "";
                        BtnListUP[i].gameObject.SetActive(false); // ��Ȱ��ȭ ��ư
                    }
                }
            }
        }
    }


    private void Update()
    {
        GameObject player = GameObject.Find("Player");
        actionController = player.GetComponent<ActionController>();

        // ESC Ű�� ������ �� copiedPanel�� ��Ȱ��ȭ�մϴ�.
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
        copiedTextUI.text = _itemInfo + "\n�ڵ尡 ����Ǿ����ϴ�!";
        copiedPanel.SetActive(true); // ���� �˸� �г��� Ȱ��ȭ�մϴ�.
    }

    public void CloseCopiedPanel()
    {
        copiedPanel.SetActive(false); // ���� �˸� �г��� ��Ȱ��ȭ�մϴ�.
    }
}
