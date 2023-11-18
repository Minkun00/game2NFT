using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;  // ���� ������ �ִ� �Ÿ�

    private bool pickActivated = false;  // ���� ������ �� true

    private RaycastHit2D hitInfo;  // �浹ü ���� ����

    // ������ ���̾�� �����ϵ��� ���̾� ����ũ ����.
    [SerializeField]
    private LayerMask layerMask;

    // �ʿ��� ������Ʈ.
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
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ���߽��ϴ� ");
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
            direction = Vector2.right;  // ������ ����
        }
        else
        {
            direction = Vector2.left;  // ���� ����
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
