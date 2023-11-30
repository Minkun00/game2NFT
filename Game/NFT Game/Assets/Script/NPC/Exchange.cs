using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Exchange : MonoBehaviour
{
    public GameObject exchangeUI; // Exchange UI�� �������ּ���.
    public ActionController actionController;
    public TextMeshProUGUI[] ItemListUp;
    public Button[] BtnListUP;
    public GameObject copyUI;
    public TextMeshProUGUI copiedTextUI; // ���� �˸��� ǥ���� Text UI�� �������ּ���.
    public GameObject copiedPanel; // ���� �˸� �г��� �������ּ���.


    private void Start()
    {
        copiedPanel.SetActive(false);

        //// Panel�� ����ŭ �ݺ�
        //for (int i = 0; i < ItemListUp.Length; i++)
        //{
        //    // Panel�� ã��
        //    GameObject panel = GameObject.Find("Panel (" + (i + 1) + ")");

        //    // Panel�� �ڽ� ������Ʈ �� item�� ã�� ItemListUp�� ����
        //    ItemListUp[i] = panel.transform.Find("item").GetComponent<TextMeshProUGUI>();

        //    // Panel�� �ڽ� ������Ʈ �� CopyBtn�� ã�� BtnListUP�� ����
        //    BtnListUP[i] = panel.transform.Find("CopyBtn").GetComponent<Button>();
        //}
    }


    private void OnMouseDown()
    {
        if (exchangeUI != null)
        {
            bool isActive = exchangeUI.activeSelf;
            exchangeUI.SetActive(!isActive); // UI�� Ȱ��ȭ ���¸� ����մϴ�.

            // exchangeUI�� Ȱ��ȭ�� �� curItemNameList�� �׸���� ǥ���մϴ�.
            if (actionController.curItemNameList.Count > 0)
            {
                for (int i = 0; i < ItemListUp.Length; i++)
                {
                    if (i < actionController.curItemNameList.Count)
                    {
                        int number = i + 1;
                        string itemInfo = "[" + actionController.curItemRankList[i] + "] " + actionController.curItemModifyList[i] + " " + actionController.curItemNameList[i];
                        ItemListUp[i].text = number + ". " + itemInfo;
                        BtnListUP[i].onClick.RemoveAllListeners();
                        string curItemCode = actionController.curItemCodeList[i];
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
