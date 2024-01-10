using System.Collections.Generic;
using UnityEngine;

// �����ۿ� �ش��ϴ� �̹����� unity ������ ���� ItemManager ������Ʈ���� ��������.
[System.Serializable]
public class ImageLink
{
    public string ItemName;             // Absolute, Army, Knight
    public ItemPartType ItemPart;       // �Ʒ� enum ����
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

// �����Ǵ� ��� �������� ����Ʈ�� ¥�� ���� class �� ������
[System.Serializable]
public class ItemList
{
    public string Adjective, ItemName, ItemPart, Color, Rank, ItemCode, Name;
    public Sprite EquipmentImage, ColorImage, RankImage;

    // ������ ItemList�� ���� �� �� ����� ��� �ش� �̹����� ���ڷ� �޾ƿ�.
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
 *  ���������� ItemData.json�� �о���� ����
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
        // JSON ������ �б�
        TextAsset jsonData = Resources.Load<TextAsset>("ItemData");
        ItemData itemData = JsonUtility.FromJson<ItemData>(jsonData.text);

        // ���� ������ ����
        CreateRandomItem(itemData);
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
        // ��: ImageLink equipmentLink = EquipmentImages.Find(img => img.ItemName == randomItemName);
        // ������ ������Ʈ ���� ��...

        // ������ ������ ���� �α� �Ǵ� ó��
        Debug.Log("Created Item: " + randomAdjective + " " + randomItemName + " " + randomItemPart + " " + randomColor + " " + randomRank);
    }

    private string GetRandomKeyFromDictionary(Dictionary<string, string> dict)
    {
        List<string> keys = new List<string>(dict.Keys);
        return keys[UnityEngine.Random.Range(0, keys.Count)];
    }

}
