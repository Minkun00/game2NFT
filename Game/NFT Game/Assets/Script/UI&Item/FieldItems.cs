using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;

 
    public void SetItem(Item _item)        // 아이템 드랍되어있는 상태의 메소드
    {
        item.itemName = _item.itemName;    // SetItem 메소드로 전달받은 Item으로 현재 class의 item을 초기화한다.
        item.itemImage = _item.itemImage;
        item.itemType = _item.itemType;

        image.sprite = item.itemImage;
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
