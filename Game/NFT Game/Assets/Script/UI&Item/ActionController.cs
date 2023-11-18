using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;  // 습득 가능한 최대 거리

    private bool pickActivated = false;  // 습득 가능할 시 true

    private RaycastHit2D hitInfo;  // 충돌체 정보 저장

    // 아이템 레이어에만 반응하도록 레이어 마스크 설정.
    [SerializeField]
    private LayerMask layerMask;

    // 필요한 컴포넌트.
    [SerializeField]
    public TextMeshProUGUI actionText;

    [SerializeField]
    public Image actionTextPanel;

    void Update()
    {
        CheckItem();
        TryAction();   
    }

    private void TryAction()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CanPickUp()
    {
        if(pickActivated)
        {
            if(hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득했습니다 ");
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }

    private void CheckItem()
    {
        Vector2 direction;

        if (transform.localScale.x > 0)
        {
            direction = Vector2.right;  // 오른쪽 방향
        }
        else
        {
            direction = Vector2.left;  // 왼쪽 방향
        }

        Debug.DrawRay(transform.position, direction, new Color(0, 1, 0));
        hitInfo = Physics2D.Raycast(transform.position, direction, range, layerMask);
        if (hitInfo)
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
            InfoDisappear();
    }

    private void InfoDisappear()
    {
        pickActivated = false;
        actionText.gameObject.SetActive(false);
        actionTextPanel.gameObject.SetActive(false);
    }

    private void ItemInfoAppear()
    {
        pickActivated = true;
        actionText.gameObject.SetActive(true);
        actionTextPanel.gameObject.SetActive(true);
        actionText.text = "[" + hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "] Pick up " + "<color=yellow>" + "(Z)" + "</color>";
    }
}
