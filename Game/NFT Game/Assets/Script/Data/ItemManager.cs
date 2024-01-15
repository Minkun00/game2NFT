using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

/**
 *  JSON데이터를 키-밸류값으로 불러오기 위함.
 *  Adjective, ItemName 등 가장 큰 타입의 키들을 가져오고,
 *  해당하는 밸류인 <낭만있는, 101>... 등 가져옴
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
         *  ItemData.json을 역직렬화하여 itemData에 저장한다. 
         */
        ItemData itemData = JsonConvert.DeserializeObject<ItemData>(ItemDatabase.text);

        // 랜덤 아이템 생성
        int wantItemCount = 4;
        for (int i = 0; i < wantItemCount; ++i)
        {
            CreateRandomItem(itemData);
            CreateItemSprite(itemList[i]);
        }
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
        itemList.Add(new ItemList(randomAdjective, randomItemName, randomItemPart, randomColor, randomRank, itemCode, itemSprite, colorSprite, rankSprite));

        // 생성된 아이템 정보 로깅 또는 처리
        Debug.Log("Created Item: " + randomAdjective + " " + randomItemName + " " + randomItemPart);
    }

    private string GetRandomKeyFromDictionary(Dictionary<string, string> dict)
    {
        List<string> keys = new List<string>(dict.Keys);
        return keys[UnityEngine.Random.Range(0, keys.Count)];
    }



    private GameObject CreateItemSprite(ItemList randomItem)
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
        item.itemType = Item.ItemType.Equipment; // 이 부분은 필요에 따라 조정
        item.ItemCode = randomItem.ItemCode;
        item.Adjective = randomItem.Adjective;
        item.ItemName = randomItem.ItemName;
        item.ItemPart = randomItem.ItemPart;
        item.Rank = randomItem.Rank;
        item.Color = randomItem.Color;
        item.EquipmentImage = randomItem.EquipmentImage;
        item.ColorImage = randomItem.ColorImage;
        item.RankImage = randomItem.RankImage;

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