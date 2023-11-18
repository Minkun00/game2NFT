using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;
    public static FieldItems Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void SetItem(Item _item)        // ������ ����Ǿ��ִ� ������ �޼ҵ�
    {
        item.itemName = _item.itemName;    // SetItem �޼ҵ�� ���޹��� Item���� ���� class�� item�� �ʱ�ȭ�Ѵ�.
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
