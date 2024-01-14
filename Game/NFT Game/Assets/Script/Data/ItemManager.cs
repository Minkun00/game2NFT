using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using static ColorImageLink;
using System;
using static ItemImageLink;
using NUnit.Framework.Internal;

// 아이템에 해당하는 이미지를 unity 에디터 내의 ItemManager 오브젝트에서 연결해줌.
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

    public Type itemPart;       // 아래 enum 참고
    public Name itemName;             // Absolute, Army, Knight
    public Sprite image;


    public ItemImageLink(Name _name, Type _part, Sprite _image)
    {
        itemPart = _part;
        itemName = _name;
        image = _image;
    }
}

// 배경 색 이미지 연동
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

// 랭크 이미지 연동
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


// 생성되는 모든 아이템의 리스트를 짜기 위한 class 및 생성자
[System.Serializable]
public class ItemList
{
    public string Adjective, ItemName, ItemPart, Color, Rank;

    // 생성자 ItemList를 생성 할 때 형용사 등과 해당 이미지를 인자로 받아옴.
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
 *  본격적으로 ItemData.json을 읽어오는 과정
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

        // 랜덤 아이템 생성
        CreateRandomItem(itemData);
        CreateItemSprite(curItemList[0]);
    }

    private void CreateRandomItem(ItemData itemData)
    {
        // 랜덤으로 아이템 구성 요소 선택
        string randomAdjective = GetRandomKeyFromDictionary(itemData.Adjective);
        string randomItemName = GetRandomKeyFromDictionary(itemData.ItemName);
        string randomItemPart = GetRandomKeyFromDictionary(itemData.ItemPart);
        string randomColor = GetRandomKeyFromDictionary(itemData.Color);
        string randomRank = GetRandomKeyFromDictionary(itemData.Rank);

        // 아이템 코드 생성
        string itemCode = itemData.Adjective[randomAdjective] + itemData.ItemName[randomItemName] +
                          itemData.ItemPart[randomItemPart] + itemData.Color[randomColor] +
                          itemData.Rank[randomRank];


        // 이미지 링크 및 추가 처리
        Sprite itemSprite = null;
        Sprite colorSprite = null;
        Sprite rankSprite = null;

        // 장비 이미지
        ItemImageLink.Type partType;
        if (Enum.TryParse(randomItemPart, out partType))
        {
            ItemImageLink.Name nameType;
            if (Enum.TryParse(randomItemName, out nameType))
            {
                // 찾은 타입과 이름에 해당하는 이미지 링크 찾기
                ItemImageLink link = EquipmentImages.Find(link => link.itemPart == partType && link.itemName == nameType);
                if (link != null)
                {
                    itemSprite = link.image;
                    // 이제 itemSprite를 사용하여 아이템 이미지를 설정할 수 있습니다.
                }
            }
        }

        // 배경 이미지
        ColorImageLink.Color bgc;
        if (Enum.TryParse(randomColor, out bgc))
        {
            // 찾은 타입과 이름에 해당하는 이미지 링크 찾기
            ColorImageLink link = ColorImages.Find(link => link.color == bgc);
            if (link != null)
            {
                colorSprite = link.image;
            }
        }

        // 랭크 이미지
        RankImageLink.Rank rank;
        if (Enum.TryParse(randomRank, out rank))
        {
            // 찾은 타입과 이름에 해당하는 이미지 링크 찾기
            RankImageLink link = RankImages.Find(link => link.rank == rank);
            if (link != null)
            {
                rankSprite = link.image;
            }
        }

        // 현재 아이템 리스트에 업데이트
        curItemList.Add(new CurItemList(randomAdjective, randomItemName, randomItemPart, randomColor, randomRank, itemCode, itemSprite, colorSprite, rankSprite));

        // 생성된 아이템 정보 로깅 또는 처리
        Debug.Log("Created Item: " + randomAdjective + " " + randomItemName + " " + randomItemPart);
    }

    private string GetRandomKeyFromDictionary(Dictionary<string, string> dict)
    {
        List<string> keys = new List<string>(dict.Keys);
        return keys[UnityEngine.Random.Range(0, keys.Count)];
    }



    private GameObject CreateItemSprite(CurItemList randomItem)
    {
        // 선택된 아이템의 스프라이트를 게임 오브젝트로 생성
        Sprite[] sprites = new Sprite[] { randomItem.EquipmentImage, randomItem.ColorImage, randomItem.RankImage };
        int[] sortingOrders = new int[] { 2, 0, 1 };

        GameObject itemObject = CreateObjectSprites(sprites, sortingOrders, "Dropped_Item", "Item");

        // 아이템 오브젝트에 ItemPickUp 컴포넌트 추가
        ItemPickUp itemPickUp = itemObject.AddComponent<ItemPickUp>();

        // 아이템 정보 설정
        itemPickUp.itemInfo = randomItem;

        // 아이템 정보를 Item 스크립트 오브젝트로 생성
        Item item = ScriptableObject.CreateInstance<Item>();
        item.itemName = randomItem.ItemName;
        item.itemImage = randomItem.EquipmentImage;
        item.itemType = Item.ItemType.Equipment; // 이 부분은 필요에 따라 조정
        item.itemCode = randomItem.ItemCode;
        item.itemModify = randomItem.Adjective;
        item.itemRank = randomItem.Rank;
        item.equipmentImage = randomItem.EquipmentImage;
        item.colorImage = randomItem.ColorImage;
        item.rankedImage = randomItem.RankImage;

        // ItemPickUp 컴포넌트에 Item 정보 저장
        itemPickUp.item = item;

        return itemObject;
    }

    private GameObject CreateObjectSprites(Sprite[] sprites, int[] sortingOrders, string objectName, string objectTag)
    {
        // 이 함수는 여러 스프라이트를 갖는 게임 오브젝트를 생성
        GameObject obj = new GameObject(objectName);
        obj.tag = objectTag;
        obj.layer = LayerMask.NameToLayer("Item");

        // BoxCollider2D 컴포넌트 추가
        BoxCollider2D collider = obj.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;  // isTrigger 설정

        for (int i = 0; i < sprites.Length; i++)
        {
            GameObject child = new GameObject("Sprite_" + i);
            child.transform.parent = obj.transform;
            SpriteRenderer spriteRenderer = child.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprites[i];
            spriteRenderer.sortingOrder = sortingOrders[i];

            // 스케일 조절
            child.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f); 
        }

        return obj;
    }
}