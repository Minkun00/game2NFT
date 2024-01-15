using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 *  아이템 인식 및 획득 관련 스크립트
 *  적용된 오브젝트: Player
 *  SerializeField 되어있는 변수들은 Player 인스펙터창에서 수정 가능함.
 */

public class ActionController : MonoBehaviour
{ 
    /**
     *  습득 가능한 최대 거리 (Editor내에서 거리 지정)
     */
    [SerializeField]
    private float range;

    /**
     *  아이템 레이어에만 반응하도록 레이어 마스크 설정 (Editor내에서 Item으로 선택해야 함)
     */
    [SerializeField]
    private LayerMask layerMask;

    /**
     *  아이템 근처에 왔을 때 안내하는 UI (Editor에서 연결)
     *  @description actionText : z키를 누르라고 안내하는 텍스트
     *  @description actionTextPanel : 텍스트의 배경 패널
     */
    [SerializeField]
    private TextMeshProUGUI actionText;
    [SerializeField]
    private Image actionTextPanel;

    /**
     *  Editor내에서 인벤토리 UI와 연결
     */
    [SerializeField]
    private Inventory theInventory;

    /**
     *  플레이어가 바라보는 방향을 조절하기 위한 변수(Update함수 및 CanPickUp함수에서 조절)
     */
    private Vector2 facingDirection = Vector2.right;


    void Update()
    {
        /**
         *  방향키가 눌릴 때, Player의 방향 업데이트
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
     *  Raycast를 이용해서 ItemManager스크립트에서 생성한 Dropped_Item을 인식함
     *  "Item"이라는 이름의 Tag와 Layer로 설정된 오브젝트에만 반응하도록 되어있음(중복 확인 필요)
     */
    private void CheckItem()
    {
        Vector3 start = transform.position + new Vector3(0, 1.0f, 0); // Y축으로 1만큼 이동
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
     *  Z키가 입력되면 CheckItem함수가 한 번 더 실행되어서 아이템의 유무를 다시 확인하고, CanPickUp함수를 실행시킴. 
     *  만약 Z를 눌렀을 때 아이템이 존재하지 않으면 Nullreference 오류 발생
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
     *  pickActivated 변수를 bool 타입으로 선언 -> 기본적으로는 false(불가능), 습득 가능할 때 true(가능)으로 전환
     *  hitInfo 변수에 Raycast를 맞은 오브젝트를 저장
     */
    private bool pickActivated = false; 
    private RaycastHit2D hitInfo;


    /**
     *  아이템 줍는 키 안내하는 UI On / Off
     *  아이템이 존재한다면(CheckItem함수 참고) pickActivated를 활성화, 반대의 경우 비활성화
     */
    private void ItemInfoAppear()
    {
        pickActivated = true;
        actionText.gameObject.SetActive(true);
        actionTextPanel.gameObject.SetActive(true);
        actionText.text = "줍기 :" + "<color=yellow>" + " (Z)" + "</color>";
    }

    private void InfoDisappear()
    {
        pickActivated = false;
        actionText.gameObject.SetActive(false);
        actionTextPanel.gameObject.SetActive(false);
    }


    /**
     *  1. 아이템이 존재해서 Z키 안내 UI가 켜지고, pickActivated가 true로 전환됨.
     *  2. Raycast를 통해 충돌체의 정보가 hitInfo에 저장되어있다면, 대상의 itemPickUp 스크립트 정보를 가져옴
     *  3. itemPickUp 스크립트가 존재한다면, 아이템의 정보인 CurItemList에서 itemInfo를 얻어와 정보를 콘솔창에 제공함.
     *  4. 인벤토리에 아이템 추가
     *  5. 아이템 오브젝트는 제거됨.
     *  6. Z키 안내 UI 꺼짐.
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
                    Debug.Log(itemPickUp.itemInfo.Adjective + " " + itemPickUp.itemInfo.ItemName + " " + itemPickUp.itemInfo.ItemPart + "를 획득했습니다 ");
                    theInventory.AcquireItem(itemPickUp.item); // 인벤토리에 아이템 추가
                    Destroy(hitInfo.transform.gameObject); // 아이템 게임 오브젝트 제거
                    InfoDisappear();
                }
            }
        }
    }
}
