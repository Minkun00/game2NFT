using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public GameObject guidePanel; // 가이드 패널을 연결해주세요.


    private void OnMouseDown()
    {
        if (guidePanel != null)
        {
            bool isActive = guidePanel.activeSelf;
            guidePanel.SetActive(!isActive); // UI의 활성화 상태를 토글합니다.
        }
    }

    public void guidePanelOff()
    {
        guidePanel.SetActive(false);
    }
}
