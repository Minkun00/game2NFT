using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Website : MonoBehaviour
{
    public UIManager uiManager;

    public GameObject confirmPanel; // 확인 알림 패널을 연결해주세요.
    public GameObject loadingPanel; // 예 눌렀을 때 나오는 화면
    public GameObject completePanel;

    public Slider loadingSlider;

    public string url = "https://minkun00.github.io/game2NFT/";
    private void Start()
    {
        confirmPanel.SetActive(false);
        loadingPanel.SetActive(false);
        completePanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && completePanel.activeSelf)
        {
            completePanel.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if ((confirmPanel != null) && !uiManager.exchangeUI.activeSelf && !uiManager.guidePanel.activeSelf)
        {
            bool isActive = confirmPanel.activeSelf;
            confirmPanel.SetActive(!isActive); // UI의 활성화 상태를 토글합니다.
        }
    }

    public void ExitExchange()
    {
        confirmPanel.SetActive(false);
    }

    IEnumerator DelayedOpenURL(string url, float delay)
    {
        float elapsedTime = 0;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            if (loadingSlider != null)
            {
                loadingSlider.value = elapsedTime / delay;
            }
            yield return null;
        }

        Application.OpenURL(url);
        loadingPanel.SetActive(false);
        completePanel.SetActive(true);
    }


    public void loadingPanelOn()
    {
        if (loadingSlider != null)
        {
            loadingSlider.value = 0; // 슬라이더를 초기화합니다.
        }

        StartCoroutine(DelayedOpenURL(url, 0.8f));
        confirmPanel.SetActive(false);
        loadingPanel.SetActive(true);
    }


    public void CompleteInfoPanelOff()
    {
        completePanel.SetActive(false);
    }
}
