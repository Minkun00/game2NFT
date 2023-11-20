using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public string itemName;          // �������� �̸�
    public ItemType itemType;        // �������� ����
    public Sprite itemImage;         // �������� �̹���
    public GameObject itemPrefab;    // �������� ������

    public string weaponType;        // ���� ����


    public enum ItemType
    {
        Used,
        Equipment,
        ETC
    }
}