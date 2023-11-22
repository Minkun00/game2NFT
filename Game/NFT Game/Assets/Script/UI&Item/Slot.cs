using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{

    public Item item; // ȹ���� ������.
    public int itemCount; // ȹ���� �������� ����.
    public Image itemImage; // �������� �̹���.


    // �ʿ��� ������Ʈ.
    [SerializeField]
    private TextMeshProUGUI text_Count;
    [SerializeField]
    private GameObject go_CountImage;

    //private WeaponManager theWeaponManager;

    void Start()
    {
        //theWeaponManager = FindObjectOfType<WeaponManager>();
    }

    // �̹����� ���� ����.
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    // ������ ȹ��
    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        itemImage.sprite = item.itemImage;

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


    private float lastClickTime = 0;
    private const float doubleClickTime = 0.2f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (/*eventData.button == PointerEventData.InputButton.Right*/(Time.time - lastClickTime) < doubleClickTime)
        {
            if (item != null)
            {
                if (item.itemType == Item.ItemType.Equipment)
                {
                    // ��� ���� ��ũ��Ʈ �ڷ�ƾ����!
                    Debug.Log(item.name + "����");
                }
                else
                {
                    Debug.Log(item.itemName + " �� ����߽��ϴ�");
                    SetSlotCount(-1);
                }
            }
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
        Debug.Log("OnEndDrag");

    }

    public void OnDrop(PointerEventData eventData)
    {

        if (DragSlot.instance.dragSlot != null)
            ChangeSlot();
        Debug.Log("OnDrop");
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
}
