using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Exchange : MonoBehaviour
{
    public GameObject exchangeUI; // Exchange UI를 연결해주세요.
    public ActionController actionController;
    public TextMeshProUGUI[] ItemListUp;
    public GameObject copyUI;

    private void OnMouseDown()
    {
        if (exchangeUI != null)
        {
            bool isActive = exchangeUI.activeSelf;
            exchangeUI.SetActive(!isActive); // UI의 활성화 상태를 토글합니다.

            // exchangeUI가 활성화될 때 curItemNameList의 항목들을 표시합니다.
            if (actionController.curItemNameList.Count > 0)
            {
                for (int i = 0; i < ItemListUp.Length; i++)
                {
                    if (i < actionController.curItemNameList.Count)
                    {
                        int number = i + 1;
                        string itemInfo = number + ". [" + actionController.curItemRankList[i] + "] " + actionController.curItemModifyList[i] + " " + actionController.curItemNameList[i];
                        ItemListUp[i].text = itemInfo;
                    }
                    else
                    {
                        ItemListUp[i].text = "";
                    }
                }
            }
        }
    }


    public void ExitExchange()
    {
        exchangeUI.SetActive(false);
    }

    public void copyCode(string _copyText)
    {
        GUIUtility.systemCopyBuffer = _copyText;
    }
}
