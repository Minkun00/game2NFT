using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
