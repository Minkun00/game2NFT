using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemList
{
    public string Adjective, ItemName, ItemPart, Color, Rank, ItemCode;
    public Sprite EquipmentImage, ColorImage, RankImage;

    public ItemList(string _Adjective, string _ItemName, string _ItemPart, string _Color, string _Rank, string _ItemCode, Sprite _EquipmentImage, Sprite _ColorImage, Sprite _RankImage)
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


