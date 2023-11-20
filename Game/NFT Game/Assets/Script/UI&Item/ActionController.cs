//using System;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class ActionController : MonoBehaviour
//{
//    [SerializeField]
//    private float range;  // ���� ������ �ִ� �Ÿ�

//    private bool pickActivated = false;  // ���� ������ �� true

//    private RaycastHit2D hitInfo;  // �浹ü ���� ����

//    // ������ ���̾�� �����ϵ��� ���̾� ����ũ ����.
//    [SerializeField]
//    private LayerMask layerMask;

//    // ��Ż ���̾�� �����ϵ��� ���̾� ����ũ ����.
//    [SerializeField]
//    private LayerMask layerMaskPortal;

//    // �ʿ��� ������Ʈ.
//    [SerializeField]
//    public TextMeshProUGUI actionText;
//    [SerializeField]
//    public Image actionTextPanel;

//    [SerializeField]
//    public TextMeshProUGUI actionTextPortal;
//    [SerializeField]
//    public Image actionTextPanelPortal;

//    [SerializeField]
//    private Inventory theInventory;

    void Update()
    {
        CheckItem();
        TryAction();

        CheckPortal();
    }
//    void Update()
//    {
//        CheckItem();
//        //CheckPortal();
//        TryAction();
//    }

    private void CheckPortal()
    {
        Vector2 direction;
//    /*
//    #region Portal_UI
//    private void CheckPortal()
//    {
//        Vector2 direction;

//        if (transform.localScale.x > 0)
//        {
//            direction = Vector2.right;  // ������ ����
//        }
//        else
//        {
//            direction = Vector2.left;  // ���� ����
//        }

//        Debug.DrawRay(transform.position, direction, new Color(0, 1, 0));
//        hitInfo = Physics2D.Raycast(transform.position, direction, range, layerMaskPortal);
//        if (hitInfo)
//        {
//            if (hitInfo.transform.tag == "Portal")
//            {
//                PortalInfoAppear();
//            }
//        }
//        else
//            PortalInfoDisappear();
//    }

    private void PortalInfoDisappear()
    {
        actionTextPortal.gameObject.SetActive(false);
        actionTextPanelPortal.gameObject.SetActive(false);
    }
//    public void PortalInfoDisappear()
//    {
//        actionTextPortal.gameObject.SetActive(false);
//        actionTextPanelPortal.gameObject.SetActive(false);
//    }

    private void PortalInfoAppear()
    {
        actionTextPortal.gameObject.SetActive(true);
        actionTextPanelPortal.gameObject.SetActive(true);
        actionTextPortal.text = " Move : " + "<color=orange>" +  "UpArrow " + "</color>";
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
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }

    private void CheckItem()
    {
        Vector2 direction;
//    public void PortalInfoAppear()
//    {
//        actionTextPortal.gameObject.SetActive(true);
//        actionTextPanelPortal.gameObject.SetActive(true);
//        actionTextPortal.text = " Move : " + "<color=orange>" +  "UpArrow " + "</color>";
//    }
//    #endregion
//    */

//    #region Item_UI
//    private void CheckItem()
//    {
//        Vector2 direction;

//        if (transform.localScale.x > 0)
//        {
//            direction = Vector2.right;  // ������ ����
//        }
//        else
//        {
//            direction = Vector2.left;  // ���� ����
//        }

//        Debug.DrawRay(transform.position, direction, new Color(0, 1, 0));
//        hitInfo = Physics2D.Raycast(transform.position, direction, range, layerMask);
//        if (hitInfo)
//        {
//            if (hitInfo.transform.tag == "Item")
//            {
//                ItemInfoAppear();
//            }
//        }
//        else
//            InfoDisappear();
//    }

//    private void InfoDisappear()
//    {
//        pickActivated = false;
//        actionText.gameObject.SetActive(false);
//        actionTextPanel.gameObject.SetActive(false);
//    }

    private void ItemInfoAppear()
    {
        pickActivated = true;
        actionText.gameObject.SetActive(true);
        actionTextPanel.gameObject.SetActive(true);
        actionText.text = "[" + hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "] Pick up " + "<color=yellow>" + "(Z)" + "</color>";
    }
}
//    private void ItemInfoAppear()
//    {
//        pickActivated = true;
//        actionText.gameObject.SetActive(true);
//        actionTextPanel.gameObject.SetActive(true);
//        actionText.text = "[" + hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "] Pick up " + "<color=yellow>" + "(Z)" + "</color>";
//    }
//    #endregion

//    private void TryAction()
//    {
//        if(Input.GetKeyDown(KeyCode.Z))
//        {
//            CheckItem();
//            CanPickUp();
//        }
//    }

//    private void CanPickUp()
//    {
//        if(pickActivated)
//        {
//            if(hitInfo.transform != null)
//            {
//                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " ȹ���߽��ϴ� ");
//                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
//                Destroy(hitInfo.transform.gameObject);
//                InfoDisappear();
//            }
//        }
//    }

    
//}
