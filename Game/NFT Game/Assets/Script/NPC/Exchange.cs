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

    public GameObject exchangeGrid;
    public TextMeshProUGUI[] ItemListUp;
    public Button[] BtnListUP;


    public TextMeshProUGUI copiedTextUI; // ���� �˸��� ǥ���� Text UI�� �������ּ���.
    public GameObject copiedPanel; // ���� �˸� �г��� �������ּ���.


    private void Start()
    {
        copiedPanel.SetActive(false);

        for (int i = 0; i < exchangeGrid.transform.childCount; i++)
        {
            // Panel (i) ������Ʈ�� ã���ϴ�.
            GameObject panel = exchangeGrid.transform.GetChild(i).gameObject;

            // item ������Ʈ (TextMeshProUGUI ������Ʈ�� ���� ������Ʈ)�� ã�� ItemListUp�� �Ҵ��մϴ�.
            TextMeshProUGUI itemText = panel.transform.Find("item").GetComponent<TextMeshProUGUI>();
            if (itemText != null && i < ItemListUp.Length)
            {
                ItemListUp[i] = itemText;
            }

            // �ش� Panel ���� Button ������Ʈ�� ã�� BtnListUP�� �Ҵ��մϴ�.
            // ���� ���, ��ư�� "button"�̶�� �̸����� Panel ���� �ִٰ� �����մϴ�.
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
            exchangeUI.SetActive(!isActive); // UI�� Ȱ��ȭ ���¸� ����մϴ�.

            // CurItemList�� ũ�⿡ ���� UI ��Ҹ� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ�մϴ�.
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
                    ItemListUp[i].gameObject.SetActive(false); // ��Ȱ��ȭ
                    BtnListUP[i].gameObject.SetActive(false); // ��Ȱ��ȭ
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
