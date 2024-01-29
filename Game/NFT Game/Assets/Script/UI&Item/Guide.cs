using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public GameObject guidePanel; // ���̵� �г��� �������ּ���.


    private void OnMouseDown()
    {
        if (guidePanel != null)
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
