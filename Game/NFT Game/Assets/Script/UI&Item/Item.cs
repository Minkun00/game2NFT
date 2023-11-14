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

[System.Serializable]

public class Item : MonoBehaviour
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;

    public bool Use()
    {
        return false;       // ������ ��� ���� ���� ��ȯ
    }

    private void Start()
    {
        //DontDestroyOnLoad(this);
    }
}
