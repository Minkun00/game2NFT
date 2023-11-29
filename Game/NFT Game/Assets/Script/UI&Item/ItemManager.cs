using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

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

    private void Start()
    {
        // 전체 아이템 리스트 불러오기
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');

        // 아이템 이름 리스트
        List<string> EquipmentList = new List<string>();
        List<string> ModifierList = new List<string>();
        List<string> ColorList = new List<string>();
        List<string> RankList = new List<string>();

        // 아이템 코드 리스트
        List<string> EquipmentCodeList = new List<string>();
        List<string> ModifierCodeList = new List<string>();
        List<string> ColorCodeList = new List<string>();
        List<string> RankCodeList = new List<string>();


        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            // 각 요소가 비어있지 않으면 리스트에 추가
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


        // 모든 경우의 수를 고려하여 아이템 생성
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

                        //Debug.Log($"[Debug] m: {m}, e: {e}, c: {c}, r: {r}");
                        //Debug.Log($"[Debug] Equipment: {EquipmentList[e]}, Color: {ColorList[c]}, Rank: {RankList[r]}");
                        //Debug.Log($"[Debug] Equipment Image: {equipmentLink?.image}, Color Image: {colorLink?.image}, Rank Image: {rankLink?.image}");

                        //if (equipmentLink == null)
                        //{
                        //    Debug.LogError("[Error] equipmentLink ImageLink not found." + equipmentLink);
                        //    continue;
                        //}
                        //else if (colorLink == null)
                        //{
                        //    Debug.LogError("[Error] colorLink ImageLink not found.");
                        //    continue;
                        //}
                        //else if (rankLink == null)
                        //{
                        //    Debug.LogError("[Error] rankLink ImageLink not found.");
                        //    continue;
                        //}

                        Sprite equipmentImage = equipmentLink.image;
                        Sprite colorImage = colorLink.image;
                        Sprite rankImage = rankLink.image;
                        AllItemList.Add(new ItemList(ModifierList[m], EquipmentList[e], ColorList[c], RankList[r], itemCode, equipmentImage, colorImage, rankImage));
                    }
                }
            }
        }

        // AllItemList에서 랜덤한 아이템 선택
        int randomIndex = Random.Range(0, AllItemList.Count);
        ItemList randomItem = AllItemList[randomIndex];

        // 선택된 아이템의 스프라이트를 게임 오브젝트로 생성
        Sprite[] sprites = new Sprite[] { randomItem.EquipmentImage, randomItem.ColorImage, randomItem.RankImage };
        int[] sortingOrders = new int[] { 2, 1, 0 };
        GameObject itemObject = CreateObjectWithMultipleSprites(sprites, sortingOrders);
        itemObject.name = "Dropped_Item";
    }

    // 여러 스프라이트를 가진 게임 오브젝트 생성 함수
    GameObject CreateObjectWithMultipleSprites(Sprite[] sprites, int[] sortingOrders)
    {
        // 빈 GameObject 생성
        GameObject obj = new GameObject();

        for (int i = 0; i < sprites.Length; i++)
        {
            // SpriteRenderer 컴포넌트를 가진 자식 GameObject 생성
            GameObject child = new GameObject();
            child.transform.parent = obj.transform;  // 부모 설정
            SpriteRenderer spriteRenderer = child.AddComponent<SpriteRenderer>();  // SpriteRenderer 컴포넌트 추가
            spriteRenderer.sprite = sprites[i];  // 스프라이트 설정
            spriteRenderer.sortingOrder = sortingOrders[i];  // Layer Order 설정

            // 스케일 조절
            child.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);  // 크기를 절반으로 줄임
        }

        return obj;
    }


}
