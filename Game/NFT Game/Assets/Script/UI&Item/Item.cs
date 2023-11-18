using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject    // ScriptableObject는 굳이 게임 오브젝트에 붙일 필요 없음
{
    public string itemName;         // 아이템의 이름
    public ItemType itemType;
    public Sprite itemImage;        // 아이템의 이미지
    public GameObject itemPrefab;   // 아이템의 프리펨

    public string weaponType;       // 무기 유형

    public enum ItemType
    {
        Equipment,
        Used,
        ETC
    }
}