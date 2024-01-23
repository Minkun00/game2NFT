using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Item item; // 획득한 아이템.
    public int itemCount; // 획득한 아이템의 개수.
    public Image itemImage; // 아이템의 이미지.
    public Image itemInvenBack; // 아이템의 이미지.
    public Image itemInvenRank; // 아이템의 이미지.


    // 아이템 정보를 표시할 Text UI
    [SerializeField]
    private GameObject itemInfoPanel;
    [SerializeField]
    private TextMeshProUGUI itemInfoText;
    [SerializeField]
    private TextMeshProUGUI itemNameText;
    [SerializeField]
    private Image itemExplainImage;
    [SerializeField]
    private Image itemBackImage;
    [SerializeField]
    private Image itemRankImage;

    // 필요한 컴포넌트.
    [SerializeField]
    private TextMeshProUGUI text_Count;
    [SerializeField]
    private GameObject go_CountImage;


    // 아이템 획득
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;

        itemImage.sprite = item.EquipmentImage;
        itemInvenBack.sprite = item.ColorImage;
        itemInvenRank.sprite = item.RankImage;


        if (item.itemType != Item.ItemType.Equipment)
        {
            go_CountImage.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            go_CountImage.SetActive(false);
        }

        SetColor(1);
    }

    // 아이템 개수 조정.
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // 슬롯 초기화.
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    // 이미지의 투명도 조절.
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        Color colorBack = itemInvenBack.color;
        Color colorRank = itemInvenRank.color;

        color.a = _alpha;
        colorBack.a = _alpha;
        colorRank.a = _alpha;


        itemImage.color = color;
        itemInvenBack.color = color;
        itemInvenRank.color = color;
    }

    private float lastClickTime = 0f; // 마지막 클릭 시간을 저장
    private const float doubleClickTime = 0.2f; // 더블 클릭 간격 (초)
    public ItemSwap itemSwap; // Player 캐릭터 참조



    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && item != null)
        {
            if ((Time.time - lastClickTime) < doubleClickTime)
            {
                if (item.itemType == Item.ItemType.Equipment)
                {
                    itemSwap.Equip(item.EquipmentImage, item.ItemPart, item.ColorImage); // 아이템 장착
                }

                else
                {
                    Debug.Log(item.Adjective + item.ItemName + item.ItemPart + " 을 사용했습니다");
                    // Heal 메소드 짜야할 듯. -> HP스크립트에서 작업.
                    SetSlotCount(-1);
                }
            }
            lastClickTime = Time.time;
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);

            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {

        if (DragSlot.instance.dragSlot != null)
            ChangeSlot();
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;
        int _tempItemCount = itemCount;

        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if (_tempItem != null)
            DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        else
            DragSlot.instance.dragSlot.ClearSlot();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            itemInfoPanel.SetActive(true);
            itemNameText.text = item.Adjective + " " + item.ItemName + item.ItemPart;
            itemExplainImage.sprite = item.EquipmentImage;
            itemBackImage.sprite = item.ColorImage;
            itemRankImage.sprite = item.RankImage;


            // 이미지 크기 조절
            itemExplainImage.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 110);
            itemBackImage.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);
            itemRankImage.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);

            itemInfoText.text =
                "등급 : " + item.Rank + "\n" +
                "공격력 : " + item.itemAttack + "\n" +
                "방어력 : " + item.itemDefence + "\n" +
                "Code : " + item.ItemCode;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoPanel.SetActive(false);
        itemInfoText.text = "";
    }

}