using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    // 아이템의 기본 정보
    public string itemName;          // 아이템의 이름
    public ItemType itemType;        // 아이템의 유형
    public Sprite itemImage;         // 아이템의 이미지
    public GameObject itemPrefab;    // 아이템의 프리팹
    public string itemCode;          // 아이템의 코드

    // 아이템 코드의 각 부분에 대응하는 이미지
    public Sprite equipmentImage;
    public Sprite colorImage;
    public Sprite rankedImage;

    public string weaponType;        // 무기 유형

    public enum ItemType
    {
        Used,
        Equipment,
        ETC
    }
}
