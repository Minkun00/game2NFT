using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Helmet,
    Top,
    Pants,
    Shoes,
    Sword
}

public enum ItemGrade
{
    Common,
    Rare,
    Epic,
    Legendary
}


[System.Serializable]

public class Item
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;

    public bool Use()
    {
        return false;       // ������ ��� ���� ���� ��ȯ
    }
}
