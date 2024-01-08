using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemList
{
    public ItemList(string _Modifier, string _Equipment, string _Color, string _Rank, string _ItemCode, Sprite _EquipmentImage, Sprite _ColorImage, Sprite _RankImage)
    {
        Modifier = _Modifier;
        Equipment = _Equipment;
        Color = _Color;
        Rank = _Rank;
        ItemCode = _ItemCode;
        EquipmentImage = _EquipmentImage;
        ColorImage = _ColorImage;
        RankImage = _RankImage;
    }

    public string Modifier, Equipment, Color, Rank, ItemCode;
    public Sprite EquipmentImage, ColorImage, RankImage;
}



[System.Serializable]
public class ImageLink
{
    public string name;
    public Sprite image;

    public ImageLink(string _name, Sprite _image)
    {
        name = _name;
        image = _image;
    }
}


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
        // ��ü ������ ����Ʈ �ҷ�����
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');

        // ������ �̸� ����Ʈ
        List<string> EquipmentList = new List<string>();
        List<string> ModifierList = new List<string>();
        List<string> ColorList = new List<string>();
        List<string> RankList = new List<string>();

        // ������ �ڵ� ����Ʈ
        List<string> EquipmentCodeList = new List<string>();
        List<string> ModifierCodeList = new List<string>();
        List<string> ColorCodeList = new List<string>();
        List<string> RankCodeList = new List<string>();

        int numberOfObjectsToCreate = 2;

        for (int k = 0; k < numberOfObjectsToCreate; k++)
        {
            for (int i = 0; i < line.Length; i++)
            {
                string[] row = line[i].Split('\t');

                // �� ��Ұ� ������� ������ ����Ʈ�� �߰�
                if (row.Length > 1 && !string.IsNullOrEmpty(row[0]) && !EquipmentList.Contains(row[0]))
                {
                    EquipmentList.Add(row[0]);
                    EquipmentCodeList.Add(row[1]);
                }

                if (row.Length > 3 && !string.IsNullOrEmpty(row[2]) && !ModifierList.Contains(row[2]))
                {
                    ModifierList.Add(row[2]);
                    ModifierCodeList.Add(row[3]);
                }

                if (row.Length > 5 && !string.IsNullOrEmpty(row[4]) && !ColorList.Contains(row[4]))
                {
                    ColorList.Add(row[4]);
                    ColorCodeList.Add(row[5]);
                }

                if (row.Length > 7 && !string.IsNullOrEmpty(row[6]) && !RankList.Contains(row[6]))
                {
                    RankList.Add(row[6]);
                    RankCodeList.Add(row[7]);
                }
            }


            // ��� ����� ���� ����Ͽ� ������ ����
            for (int m = 0; m < ModifierList.Count; m++)
            {
                for (int e = 0; e < EquipmentList.Count; e++)
                {
                    for (int c = 0; c < ColorList.Count; c++)
                    {
                        for (int r = 0; r < RankList.Count; r++)
                        {
                            string itemCode = ModifierCodeList[m] + EquipmentCodeList[e] + ColorCodeList[c] + RankCodeList[r];

                            ImageLink equipmentLink = EquipmentImages.Find(img => img.name == EquipmentList[e]);
                            ImageLink colorLink = ColorImages.Find(img => img.name == ColorList[c]);
                            ImageLink rankLink = RankImages.Find(img => img.name == RankList[r]);

                            Sprite equipmentImage = equipmentLink.image;
                            Sprite colorImage = colorLink.image;
                            Sprite rankImage = rankLink.image;

                            AllItemList.Add(new ItemList(ModifierList[m], EquipmentList[e], ColorList[c], RankList[r], itemCode, equipmentImage, colorImage, rankImage));
                        }
                    }
                }
            }

            // AllItemList���� ������ ������ ����
            int randomIndex = Random.Range(0, AllItemList.Count);
            ItemList randomItem = AllItemList[randomIndex];

            // ���õ� �������� ��������Ʈ�� ���� ������Ʈ�� ����
            Sprite[] sprites = new Sprite[] { randomItem.EquipmentImage, randomItem.ColorImage, randomItem.RankImage };
            int[] sortingOrders = new int[] { 2 + k*2, 1 + k*2, 0 + k * 2 };

            GameObject itemObject = CreateObjectWithMultipleSprites(sprites, sortingOrders, "Dropped_Item_" + k, "Item");

            // ������ ������Ʈ�� ItemPickUp ������Ʈ �߰�
            ItemPickUp itemPickUp = itemObject.AddComponent<ItemPickUp>();

            // ������ ���� ����
            itemPickUp.itemInfo = randomItem;  // randomItem ������ itemInfo�� ����

            // ������ ������ Item ��ũ��Ʈ ������Ʈ�� ����
            Item item = ScriptableObject.CreateInstance<Item>();
            item.itemName = randomItem.Equipment;
            item.itemImage = randomItem.EquipmentImage;
            item.itemType = Item.ItemType.Equipment;
            item.itemCode = randomItem.ItemCode;
            item.itemModify = randomItem.Modifier;
            item.itemRank = randomItem.Rank;
            item.equipmentImage = randomItem.EquipmentImage;
            item.colorImage = randomItem.ColorImage;
            item.rankedImage = randomItem.RankImage;

            // ItemPickUp ������Ʈ�� Item ���� ����
            itemPickUp.item = item;
        }
    }

    GameObject CreateObjectWithMultipleSprites(Sprite[] sprites, int[] sortingOrders, string objectName, string objectTag)
    {
        // �� GameObject ����
        GameObject obj = new GameObject();
        obj.name = objectName;  // GameObject�� �̸� ����
        obj.tag = objectTag;  // GameObject�� �±� ����
        obj.layer = LayerMask.NameToLayer("Item");

        // BoxCollider2D ������Ʈ �߰�
        BoxCollider2D collider = obj.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;  // isTrigger ����

        for (int i = 0; i < sprites.Length; i++)
        {
            // SpriteRenderer ������Ʈ�� ���� �ڽ� GameObject ����
            GameObject child = new GameObject();
            child.transform.parent = obj.transform;  // �θ� ����
            SpriteRenderer spriteRenderer = child.AddComponent<SpriteRenderer>();  // SpriteRenderer ������Ʈ �߰�
            spriteRenderer.sprite = sprites[i];  // ��������Ʈ ����
            spriteRenderer.sortingOrder = sortingOrders[i];  // Layer Order ����

            // ������ ����
            child.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);  // ũ�⸦ �������� ����
        }

        return obj;
    }



}
