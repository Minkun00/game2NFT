using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Item item; // ȹ���� ������.
    public int itemCount; // ȹ���� �������� ����.
    public Image itemImage; // �������� �̹���.
    public Image itemInvenBack; // �������� �̹���.
    public Image itemInvenRank; // �������� �̹���.


    // ������ ������ ǥ���� Text UI
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

    // �ʿ��� ������Ʈ.
    [SerializeField]
    private TextMeshProUGUI text_Count;
    [SerializeField]
    private GameObject go_CountImage;
    

    // �̹����� ���� ����.
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

    // ������ ȹ��
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;

        itemImage.sprite = item.itemImage;
        itemInvenBack.sprite = item.colorImage;
        itemInvenRank.sprite = item.rankedImage;

        //itemImage.GetComponent<RectTransform>().sizeDelta = new Vector2(70, 70);

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

    // ������ ���� ����.
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();

        if (itemCount <= 0)
            ClearSlot();
    }

    // ���� �ʱ�ȭ.
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        go_CountImage.SetActive(false);
    }

    private float lastClickTime = 0f; // ������ Ŭ�� �ð��� ����
    private const float doubleClickTime = 0.2f; // ���� Ŭ�� ���� (��)
    public Character playerCharacter; // Player ĳ���� ����
    public EquipmentItem equipmentItem; // �� ���Կ� �Ҵ�� ��� ������

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && item != null)
        {
            if ((Time.time - lastClickTime) < doubleClickTime)
            {
                if (item.itemType == Item.ItemType.Equipment)
                {
                    playerCharacter.Equip(equipmentItem); // ������ ����
                }

                else
                {
                    Debug.Log(item.itemName + " �� ����߽��ϴ�");
                    // Heal �޼ҵ� ¥���� ��. -> HP��ũ��Ʈ���� �۾�.
                    SetSlotCount(-1);
                }
            }
            lastClickTime = Time.time;
        }
    }


    private void EquipItem()
    {
        // �������� �����ϴ� ���� ����
        Debug.Log(item.itemName + "�� �����߽��ϴ�.");

        // ���⿡ ��� ���� ���� ������ �߰�
        // ��: ĳ���� �𵨿� ��������Ʈ ����, ���� ������Ʈ ��
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
            itemNameText.text = item.itemModify + " " + item.itemName;
            itemExplainImage.sprite = item.itemImage;
            itemBackImage.sprite = item.colorImage;
            itemRankImage.sprite = item.rankedImage;

            // �̹��� ũ�� ����
            itemExplainImage.GetComponent<RectTransform>().sizeDelta = new Vector2(110, 110);
            itemBackImage.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150); 
            itemRankImage.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150); 

            itemInfoText.text = 
                "��� : " + item.itemRank + "\n" + 
                "���ݷ� : " + item.itemAttack + "\n" + 
                "���� : " + item.itemDefence + "\n" + 
                "Code : " + item.itemCode;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoPanel.SetActive(false);
        itemInfoText.text = "";
    }

}
