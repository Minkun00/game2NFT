using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public btnType currentType;
    public Transform btnScale;
    Vector3 defaultScale;

    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;

    void Start()
    {
        defaultScale = btnScale.localScale;
    }

    public void OnBtnClick()  // OnClick으로 연결
    {
        switch(currentType)
        {
            case btnType.New:
                SceneManager.LoadScene("Town");
                Debug.Log("New");
                break;
            case btnType.Continue:
                SceneManager.LoadScene("Town");
                Debug.Log("Continue");
                break;
            case btnType.Token:
                Debug.Log("Token");
                break;
            case btnType.Quit:
                Application.Quit();
                Debug.Log("Quit");
                break;
            case btnType.Option:
                CanvasGroupOn(optionGroup);
                CanvasGroupOff(mainGroup);
                break;
            case btnType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(optionGroup);
                break;
            case btnType.Sound:
                break;
        }
    }

    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        btnScale.localScale = defaultScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        btnScale.localScale = defaultScale;
    }
}
