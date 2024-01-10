using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    // �������� �⺻ ����
    public string itemName;          // �������� �̸�
    public ItemType itemType;        // �������� ����
    public Sprite itemImage;         // �������� �̹���
    public string itemCode, itemModify, itemRank;          // �������� �ڵ�, ���ľ�, ���
    public string itemAttack = "0", itemDefence = "0";

    // ������ �ڵ��� �� �κп� �����ϴ� �̹���
    public Sprite equipmentImage;
    public Sprite colorImage;
    public Sprite rankedImage;

    public enum ItemType
    {
        Used,
        Equipment,
        ETC
    }
}
