using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 *  ������ �ν� �� ȹ�� ���� ��ũ��Ʈ
 *  ����� ������Ʈ: Player
 *  SerializeField �Ǿ��ִ� �������� Player �ν�����â���� ���� ������.
 */

public class ActionController : MonoBehaviour
{ 
    /**
     *  ���� ������ �ִ� �Ÿ� (Editor������ �Ÿ� ����)
     */
    [SerializeField]
    private float range;

    /**
     *  ������ ���̾�� �����ϵ��� ���̾� ����ũ ���� (Editor������ Item���� �����ؾ� ��)
     */
    [SerializeField]
    private LayerMask layerMask;

    /**
     *  ������ ��ó�� ���� �� �ȳ��ϴ� UI (Editor���� ����)
     *  @description actionText : zŰ�� ������� �ȳ��ϴ� �ؽ�Ʈ
     *  @description actionTextPanel : �ؽ�Ʈ�� ��� �г�
     */
    [SerializeField]
    private TextMeshProUGUI actionText;
    [SerializeField]
    private Image actionTextPanel;

    /**
     *  Editor������ �κ��丮 UI�� ����
     */
    [SerializeField]
    private Inventory theInventory;

    /**
     *  �÷��̾ �ٶ󺸴� ������ �����ϱ� ���� ����(Update�Լ� �� CanPickUp�Լ����� ����)
     */
    private Vector2 facingDirection = Vector2.right;


    void Update()
    {
        /**
         *  ����Ű�� ���� ��, Player�� ���� ������Ʈ
         */
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            facingDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            facingDirection = Vector2.right;
        }

        
        CheckItem();
        TryAction();
    }

    /**
     *  Raycast�� �̿��ؼ� ItemManager��ũ��Ʈ���� ������ Dropped_Item�� �ν���
     *  "Item"�̶�� �̸��� Tag�� Layer�� ������ ������Ʈ���� �����ϵ��� �Ǿ�����(�ߺ� Ȯ�� �ʿ�)
     */
    private void CheckItem()
    {
        Vector3 start = transform.position + new Vector3(0, 1.0f, 0); // Y������ 1��ŭ �̵�
        Debug.DrawRay(start, facingDirection, new Color(0, 1, 0));

        hitInfo = Physics2D.Raycast(start, facingDirection, range, layerMask);

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


    /**
     *  ZŰ�� �ԷµǸ� CheckItem�Լ��� �� �� �� ����Ǿ �������� ������ �ٽ� Ȯ���ϰ�, CanPickUp�Լ��� �����Ŵ. 
     *  ���� Z�� ������ �� �������� �������� ������ Nullreference ���� �߻�
     */
    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CheckItem();
            CanPickUp();
        }
    }

    /**
     *  pickActivated ������ bool Ÿ������ ���� -> �⺻�����δ� false(�Ұ���), ���� ������ �� true(����)���� ��ȯ
     *  hitInfo ������ Raycast�� ���� ������Ʈ�� ����
     */
    private bool pickActivated = false; 
    private RaycastHit2D hitInfo;


    /**
     *  ������ �ݴ� Ű �ȳ��ϴ� UI On / Off
     *  �������� �����Ѵٸ�(CheckItem�Լ� ����) pickActivated�� Ȱ��ȭ, �ݴ��� ��� ��Ȱ��ȭ
     */
    private void ItemInfoAppear()
    {
        pickActivated = true;
        actionText.gameObject.SetActive(true);
        actionTextPanel.gameObject.SetActive(true);
        actionText.text = "�ݱ� :" + "<color=yellow>" + " (Z)" + "</color>";
    }

    private void InfoDisappear()
    {
        pickActivated = false;
        actionText.gameObject.SetActive(false);
        actionTextPanel.gameObject.SetActive(false);
    }


    /**
     *  1. �������� �����ؼ� ZŰ �ȳ� UI�� ������, pickActivated�� true�� ��ȯ��.
     *  2. Raycast�� ���� �浹ü�� ������ hitInfo�� ����Ǿ��ִٸ�, ����� itemPickUp ��ũ��Ʈ ������ ������
     *  3. itemPickUp ��ũ��Ʈ�� �����Ѵٸ�, �������� ������ CurItemList���� itemInfo�� ���� ������ �ܼ�â�� ������.
     *  4. �κ��丮�� ������ �߰�
     *  5. ������ ������Ʈ�� ���ŵ�.
     *  6. ZŰ �ȳ� UI ����.
     */
    private void CanPickUp()
    {
        if (pickActivated)
        {
            if (hitInfo.transform != null)
            {
                ItemPickUp itemPickUp = hitInfo.transform.GetComponent<ItemPickUp>();
                if (itemPickUp != null)
                {
                    Debug.Log(itemPickUp.itemInfo.Adjective + " " + itemPickUp.itemInfo.ItemName + " " + itemPickUp.itemInfo.ItemPart + "�� ȹ���߽��ϴ� ");
                    theInventory.AcquireItem(itemPickUp.item); // �κ��丮�� ������ �߰�
                    Destroy(hitInfo.transform.gameObject); // ������ ���� ������Ʈ ����
                    InfoDisappear();
                }
            }
        }
    }
}
