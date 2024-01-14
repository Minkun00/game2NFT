using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using static ColorImageLink;
using System;
using static ItemImageLink;
using NUnit.Framework.Internal;

// �����ۿ� �ش��ϴ� �̹����� unity ������ ���� ItemManager ������Ʈ���� ��������.
[System.Serializable]
public class ItemImageLink
{
    public enum Type
    {
        Helmet,
        Top,
        Pants,
        Shoes,
        Sword
    }
    public enum Name
    {
        Absolute,
        Knight,
        Army
    }

    public Type itemPart;       // �Ʒ� enum ����
    public Name itemName;             // Absolute, Army, Knight
    public Sprite image;


    public ItemImageLink(Name _name, Type _part, Sprite _image)
    {
        itemPart = _part;
        itemName = _name;
        image = _image;
    }
}

// ��� �� �̹��� ����
[System.Serializable]
public class ColorImageLink
{
    public enum Color
    {
        Red,
        Orange,
        Yellow,
        Green,
        Skyblue,
        Blue,
        Purple
    }

    public Color color;
    public Sprite image;

    public ColorImageLink(Color _color, Sprite _image)
    {
        color = _color;
        image = _image;
    }
}

// ��ũ �̹��� ����
[System.Serializable]
public class RankImageLink
{
    public enum Rank
    {
        Normal,
        Epic,
        Unique,
        Legendary
    }

    public Rank rank;
    public Sprite image;

    public RankImageLink(Rank _rank, Sprite _image)
    {
        rank = _rank;
        image = _image;
    }
}


// �����Ǵ� ��� �������� ����Ʈ�� ¥�� ���� class �� ������
[System.Serializable]
public class ItemList
{
    public string Adjective, ItemName, ItemPart, Color, Rank;

    // ������ ItemList�� ���� �� �� ����� ��� �ش� �̹����� ���ڷ� �޾ƿ�.
    public ItemList(string _Adjective, string _ItemName, string _ItemPart, string _Color, string _Rank)
    {
        Adjective = _Adjective;
        ItemName = _ItemName;
        ItemPart = _ItemPart;
        Color = _Color;
        Rank = _Rank;
    }
}


[System.Serializable]
public class ItemData
{
    public Dictionary<string, string> Adjective;
    public Dictionary<string, string> ItemName;
    public Dictionary<string, string> ItemPart;
    public Dictionary<string, string> Color;
    public Dictionary<string, string> Rank;
}

[System.Serializable]
public class CurItemList
{
    public string Adjective, ItemName, ItemPart, Color, Rank, ItemCode;
    public Sprite EquipmentImage, ColorImage, RankImage;

    public CurItemList(string _Adjective, string _ItemName, string _ItemPart, string _Color, string _Rank, string _ItemCode, Sprite _EquipmentImage, Sprite _ColorImage, Sprite _RankImage)
    {
        Adjective = _Adjective;
        ItemName = _ItemName;
        ItemPart = _ItemPart;
        Color = _Color;
        Rank = _Rank;
        ItemCode = _ItemCode;
        EquipmentImage = _EquipmentImage;
        ColorImage = _ColorImage;
        RankImage = _RankImage;
    }
}


/*
 *  ���������� ItemData.json�� �о���� ����
 */
public class ItemManager : MonoBehaviour
{
    public List<CurItemList> curItemList;
    public TextAsset ItemDatabase;

    public List<ItemImageLink> EquipmentImages;
    public List<ColorImageLink> ColorImages;
    public List<RankImageLink> RankImages;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        ItemData itemData = JsonConvert.DeserializeObject<ItemData>(ItemDatabase.text);

        // ���� ������ ����
        CreateRandomItem(itemData);
        CreateItemSprite(curItemList[0]);
    }

    private void CreateRandomItem(ItemData itemData)
    {
        // �������� ������ ���� ��� ����
        string randomAdjective = GetRandomKeyFromDictionary(itemData.Adjective);
        string randomItemName = GetRandomKeyFromDictionary(itemData.ItemName);
        string randomItemPart = GetRandomKeyFromDictionary(itemData.ItemPart);
        string randomColor = GetRandomKeyFromDictionary(itemData.Color);
        string randomRank = GetRandomKeyFromDictionary(itemData.Rank);

        // ������ �ڵ� ����
        string itemCode = itemData.Adjective[randomAdjective] + itemData.ItemName[randomItemName] +
                          itemData.ItemPart[randomItemPart] + itemData.Color[randomColor] +
                          itemData.Rank[randomRank];


        // �̹��� ��ũ �� �߰� ó��
        Sprite itemSprite = null;
        Sprite colorSprite = null;
        Sprite rankSprite = null;

        // ��� �̹���
        ItemImageLink.Type partType;
        if (Enum.TryParse(randomItemPart, out partType))
        {
            ItemImageLink.Name nameType;
            if (Enum.TryParse(randomItemName, out nameType))
            {
                // ã�� Ÿ�԰� �̸��� �ش��ϴ� �̹��� ��ũ ã��
                ItemImageLink link = EquipmentImages.Find(link => link.itemPart == partType && link.itemName == nameType);
                if (link != null)
                {
                    itemSprite = link.image;
                    // ���� itemSprite�� ����Ͽ� ������ �̹����� ������ �� �ֽ��ϴ�.
                }
            }
        }

        // ��� �̹���
        ColorImageLink.Color bgc;
        if (Enum.TryParse(randomColor, out bgc))
        {
            // ã�� Ÿ�԰� �̸��� �ش��ϴ� �̹��� ��ũ ã��
            ColorImageLink link = ColorImages.Find(link => link.color == bgc);
            if (link != null)
            {
                colorSprite = link.image;
            }
        }

        // ��ũ �̹���
        RankImageLink.Rank rank;
        if (Enum.TryParse(randomRank, out rank))
        {
            // ã�� Ÿ�԰� �̸��� �ش��ϴ� �̹��� ��ũ ã��
            RankImageLink link = RankImages.Find(link => link.rank == rank);
            if (link != null)
            {
                rankSprite = link.image;
            }
        }

        // ���� ������ ����Ʈ�� ������Ʈ
        curItemList.Add(new CurItemList(randomAdjective, randomItemName, randomItemPart, randomColor, randomRank, itemCode, itemSprite, colorSprite, rankSprite));

        // ������ ������ ���� �α� �Ǵ� ó��
        Debug.Log("Created Item: " + randomAdjective + " " + randomItemName + " " + randomItemPart);
    }

    private string GetRandomKeyFromDictionary(Dictionary<string, string> dict)
    {
        List<string> keys = new List<string>(dict.Keys);
        return keys[UnityEngine.Random.Range(0, keys.Count)];
    }



    private GameObject CreateItemSprite(CurItemList randomItem)
    {
        // ���õ� �������� ��������Ʈ�� ���� ������Ʈ�� ����
        Sprite[] sprites = new Sprite[] { randomItem.EquipmentImage, randomItem.ColorImage, randomItem.RankImage };
        int[] sortingOrders = new int[] { 2, 0, 1 };

        GameObject itemObject = CreateObjectSprites(sprites, sortingOrders, "Dropped_Item", "Item");

        // ������ ������Ʈ�� ItemPickUp ������Ʈ �߰�
        ItemPickUp itemPickUp = itemObject.AddComponent<ItemPickUp>();

        // ������ ���� ����
        itemPickUp.itemInfo = randomItem;

        // ������ ������ Item ��ũ��Ʈ ������Ʈ�� ����
        Item item = ScriptableObject.CreateInstance<Item>();
        item.itemName = randomItem.ItemName;
        item.itemImage = randomItem.EquipmentImage;
        item.itemType = Item.ItemType.Equipment; // �� �κ��� �ʿ信 ���� ����
        item.itemCode = randomItem.ItemCode;
        item.itemModify = randomItem.Adjective;
        item.itemRank = randomItem.Rank;
        item.equipmentImage = randomItem.EquipmentImage;
        item.colorImage = randomItem.ColorImage;
        item.rankedImage = randomItem.RankImage;

        // ItemPickUp ������Ʈ�� Item ���� ����
        itemPickUp.item = item;

        return itemObject;
    }

    private GameObject CreateObjectSprites(Sprite[] sprites, int[] sortingOrders, string objectName, string objectTag)
    {
        // �� �Լ��� ���� ��������Ʈ�� ���� ���� ������Ʈ�� ����
        GameObject obj = new GameObject(objectName);
        obj.tag = objectTag;
        obj.layer = LayerMask.NameToLayer("Item");

        // BoxCollider2D ������Ʈ �߰�
        BoxCollider2D collider = obj.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;  // isTrigger ����

        for (int i = 0; i < sprites.Length; i++)
        {
            GameObject child = new GameObject("Sprite_" + i);
            child.transform.parent = obj.transform;
            SpriteRenderer spriteRenderer = child.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[i];
            spriteRenderer.sortingOrder = sortingOrders[i];

            // ������ ����
            child.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f); 
        }

        return obj;
    }
}