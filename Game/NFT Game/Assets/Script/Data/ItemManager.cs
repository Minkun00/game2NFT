using System.Collections.Generic;
using UnityEngine;

// 아이템에 해당하는 이미지를 unity 에디터 내의 ItemManager 오브젝트에서 연결해줌.
[System.Serializable]
public class ImageLink
{
    public string ItemName;             // Absolute, Army, Knight
    public ItemPartType ItemPart;       // 아래 enum 참고
    public Sprite image;

    public enum ItemPartType
    {
        Helmet,
        Top,
        Pants,
        Shoes,
        Sword
    }

    public ImageLink(string _name, ItemPartType _part, Sprite _image)
    {
        ItemName = _name;
        ItemPart = _part;
        image = _image;
    }
}

// 생성되는 모든 아이템의 리스트를 짜기 위한 class 및 생성자
[System.Serializable]
public class ItemList
{
    public string Adjective, ItemName, ItemPart, Color, Rank, ItemCode, Name;
    public Sprite EquipmentImage, ColorImage, RankImage;

    // 생성자 ItemList를 생성 할 때 형용사 등과 해당 이미지를 인자로 받아옴.
    public ItemList(string _Adjective, string _ItemName, string _ItemPart, string _Color, string _Rank, string _ItemCode,
                   Sprite _EquipmentImage, Sprite _ColorImage, Sprite _RankImage)
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


[System.Serializable]
public class ItemData
{
    public Dictionary<string, string> Adjective;
    public Dictionary<string, string> ItemName;
    public Dictionary<string, string> ItemPart;
    public Dictionary<string, string> Color;
    public Dictionary<string, string> Rank;
}


/*
 *  본격적으로 ItemData.json을 읽어오는 과정
 */
public class ItemManager : MonoBehaviour
{
    public TextAsset ItemDatabase;
    public List<ItemList> AllItemList;

    public List<ImageLink> EquipmentImages;
    public List<ImageLink> ColorImages;
    public List<ImageLink> RankImages;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        // JSON 데이터 읽기
        TextAsset jsonData = Resources.Load<TextAsset>("ItemData");
        ItemData itemData = JsonUtility.FromJson<ItemData>(jsonData.text);

        // 랜덤 아이템 생성
        CreateRandomItem(itemData);
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
        // 예: ImageLink equipmentLink = EquipmentImages.Find(img => img.ItemName == randomItemName);
        // 아이템 오브젝트 생성 등...

        // 생성된 아이템 정보 로깅 또는 처리
        Debug.Log("Created Item: " + randomAdjective + " " + randomItemName + " " + randomItemPart + " " + randomColor + " " + randomRank);
    }

    private string GetRandomKeyFromDictionary(Dictionary<string, string> dict)
    {
        List<string> keys = new List<string>(dict.Keys);
        return keys[UnityEngine.Random.Range(0, keys.Count)];
    }

}
