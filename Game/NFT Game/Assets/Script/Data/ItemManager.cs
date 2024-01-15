using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

/**
 *  JSON�����͸� Ű-��������� �ҷ����� ����.
 *  Adjective, ItemName �� ���� ū Ÿ���� Ű���� ��������,
 *  �ش��ϴ� ����� <�����ִ�, 101>... �� ������
 */
[System.Serializable]
public class ItemData
{
    public Dictionary<string, string> Adjective;
    public Dictionary<string, string> ItemName;
    public Dictionary<string, string> ItemPart;
    public Dictionary<string, string> Color;
    public Dictionary<string, string> Rank;
}



public class ItemManager : MonoBehaviour
{
    public List<ItemList> itemList;
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
        /**
         *  ItemData.json�� ������ȭ�Ͽ� itemData�� �����Ѵ�. 
         */
        ItemData itemData = JsonConvert.DeserializeObject<ItemData>(ItemDatabase.text);

        // ���� ������ ����
        int wantItemCount = 4;
        for (int i = 0; i < wantItemCount; ++i)
        {
            CreateRandomItem(itemData);
            CreateItemSprite(itemList[i]);
        }
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
        itemList.Add(new ItemList(randomAdjective, randomItemName, randomItemPart, randomColor, randomRank, itemCode, itemSprite, colorSprite, rankSprite));

        // ������ ������ ���� �α� �Ǵ� ó��
        Debug.Log("Created Item: " + randomAdjective + " " + randomItemName + " " + randomItemPart);
    }

    private string GetRandomKeyFromDictionary(Dictionary<string, string> dict)
    {
        List<string> keys = new List<string>(dict.Keys);
        return keys[UnityEngine.Random.Range(0, keys.Count)];
    }



    private GameObject CreateItemSprite(ItemList randomItem)
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
        item.itemType = Item.ItemType.Equipment; // �� �κ��� �ʿ信 ���� ����
        item.ItemCode = randomItem.ItemCode;
        item.Adjective = randomItem.Adjective;
        item.ItemName = randomItem.ItemName;
        item.ItemPart = randomItem.ItemPart;
        item.Rank = randomItem.Rank;
        item.Color = randomItem.Color;
        item.EquipmentImage = randomItem.EquipmentImage;
        item.ColorImage = randomItem.ColorImage;
        item.RankImage = randomItem.RankImage;

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