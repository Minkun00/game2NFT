using NUnit.Framework.Internal;
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
    private TextMeshProUGUI actionText;
    [SerializeField]
    private Image actionTextPanel;

    [SerializeField]
    private Inventory theInventory;

    public List<string> curItemModifyList = new List<string>();
    public List<string> curItemNameList = new List<string>();
    public List<string> curItemRankList = new List<string>();
    public List<string> curItemCodeList = new List<string>();
    public List<Sprite> curItemImageList = new List<Sprite>();


    private void Start()
    {
        actionText.gameObject.SetActive(false);
        actionTextPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CheckItem();
            CanPickUp();
            AddItemList();
        }
    }

    public void AddItemList()
    {
        curItemNameList.Add(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName);
        curItemModifyList.Add(hitInfo.transform.GetComponent<ItemPickUp>().item.itemModify);
        curItemRankList.Add(hitInfo.transform.GetComponent<ItemPickUp>().item.itemRank);
        curItemCodeList.Add(hitInfo.transform.GetComponent<ItemPickUp>().item.itemCode);
        curItemImageList.Add(hitInfo.transform.GetComponent<ItemPickUp>().item.itemImage);
    }

    private void CanPickUp()
    {
        if (pickActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ���߽��ϴ� ");
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
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

    private void ItemInfoAppear()
    {
        pickActivated = true;
        actionText.gameObject.SetActive(true);
        actionTextPanel.gameObject.SetActive(true);
        actionText.text = "[" + hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "]" + " Pick it up" + "<color=yellow>" + " (Z)" + "</color>";
    }

    private void InfoDisappear()
    {
        pickActivated = false;
        actionText.gameObject.SetActive(false);
        actionTextPanel.gameObject.SetActive(false);
    }

    
}
