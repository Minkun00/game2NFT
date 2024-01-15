using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    // �������� �⺻ ����
    public string Adjective, ItemName, ItemPart, Color, Rank, ItemCode;
    public Sprite EquipmentImage, ColorImage, RankImage;
    public ItemType itemType;        // �������� ����
    public string itemAttack = "0", itemDefence = "0";

    public enum ItemType
    {
        Used,
        Equipment,
        ETC
    }
}
