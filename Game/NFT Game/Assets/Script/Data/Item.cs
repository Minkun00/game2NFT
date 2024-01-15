using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    // 아이템의 기본 정보
    public string Adjective, ItemName, ItemPart, Color, Rank, ItemCode;
    public Sprite EquipmentImage, ColorImage, RankImage;
    public ItemType itemType;        // 아이템의 유형
    public string itemAttack = "0", itemDefence = "0";

    public enum ItemType
    {
        Used,
        Equipment,
        ETC
    }
}
