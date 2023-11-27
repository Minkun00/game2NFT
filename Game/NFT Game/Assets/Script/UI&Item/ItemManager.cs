using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemList
{
    public ItemList(string _Modifier, string _Equipment, string _Color, string _Rank, string _ItemCode)
    { Modifier = _Modifier; Equipment = _Equipment; Color = _Color; Rank = _Rank; ItemCode = _ItemCode; }

    public string Modifier, Equipment, Color, Rank, ItemCode;
}

public class ItemManager : MonoBehaviour
{
    public TextAsset ItemDatabase;
    public List<ItemList> AllItemList;

    private void Start()
    {
        // 전체 아이템 리스트 불러오기
        string[] line = ItemDatabase.text.Substring(0, ItemDatabase.text.Length - 1).Split('\n');

        List<string> EquipmentList = new List<string>();
        List<string> ModifierList = new List<string>();
        List<string> ColorList = new List<string>();
        List<string> RankList = new List<string>();

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
                        AllItemList.Add(new ItemList(ModifierList[m], EquipmentList[e], ColorList[c], RankList[r], itemCode));
                    }
                }
            }
        }
    }
}
