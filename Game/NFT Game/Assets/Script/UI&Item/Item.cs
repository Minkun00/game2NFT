using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public int itemID;              // �������� ���� ID��. �ߺ� �Ұ���. (50001, 50002)
    public string itemName;         // �������� �̸�. �ߺ� ����. (�������, �������)
    public string itemDescription;  // ������ ����
    public int itemCount;           // ���� ����
    public Sprite itemIcon;         // �������� ������
    public ItemType itemType;

    public enum ItemType
    {
        Use,
        Equipment,
        ETC
    }

    public Item(int _itemID, string _itemName, string _itemDes, ItemType _itemType, int _itemCount = 1)
    {
        itemID = _itemID;
        itemName = _itemName;
        itemDescription = _itemDes;
        itemType = _itemType;
        itemCount = _itemCount;

        itemIcon = Resources.Load("ItemIcon/" + _itemID.ToString(), typeof(Sprite)) as Sprite;
    }
}