using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{

    public UIManager uiManager;

    public GameObject guidePanel; // ���̵� �г��� �������ּ���.


    private void OnMouseDown()
    {
        if (guidePanel != null && !uiManager.exchangeUI.activeSelf && !uiManager.completePanel.activeSelf)
        {
            bool isActive = guidePanel.activeSelf;
            guidePanel.SetActive(!isActive); // UI�� Ȱ��ȭ ���¸� ����մϴ�.
        }
    }

    public void guidePanelOff()
    {
        guidePanel.SetActive(false);
    }
}
