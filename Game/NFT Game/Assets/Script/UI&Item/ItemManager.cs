//using System.Collections.Generic;
//using UnityEngine;

//public class ItemManager : MonoBehaviour
//{
//    public Dictionary<string, Item> equipmentDict = new Dictionary<string, Item>();
//    public Dictionary<string, Type> typeDict = new Dictionary<string, Type>();
//    public Dictionary<string, Color> colorDict = new Dictionary<string, Color>();
//    public Dictionary<string, Ranked> rankDict = new Dictionary<string, Ranked>();

//    void Start()
//    {
//        // Equipment Initialization
//        equipmentDict.Add("ArmyHelmet", new Item("ArmyHelmet", 30100, "ArmyHelmet_Sprite"));
//        equipmentDict.Add("ArmyTop", new Item("ArmyTop", 30200, "ArmyTop_Sprite"));
//        equipmentDict.Add("ArmyPants", new Item("ArmyPants", 30300, "ArmyPants_Sprite"));
//        equipmentDict.Add("ArmyShoes", new Item("ArmyShoes", 30400, "ArmyShoes_Sprite"));
//        equipmentDict.Add("ArmySword", new Item("ArmySword", 30500, "ArmySword_Sprite"));
//        equipmentDict.Add("ArmyGun", new Item("ArmyGun", 30600, "ArmyGun_Sprite"));

//        equipmentDict.Add("KnightHelmet", new Item("KnightHelmet", 40100, "KnightHelmet_Sprite"));
//        equipmentDict.Add("KnightTop", new Item("KnightTop", 40200, "KnightTop_Sprite"));
//        equipmentDict.Add("KnightPants", new Item("KnightPants", 40300, "KnightPants_Sprite"));
//        equipmentDict.Add("KnightShoes", new Item("KnightShoes", 40400, "KnightShoes_Sprite"));
//        equipmentDict.Add("KnightSword", new Item("KnightSword", 40500, "KnightSword_Sprite"));
//        equipmentDict.Add("KnightGun", new Item("KnightGun", 40600, "KnightGun_Sprite"));

//        equipmentDict.Add("AbsoluteHelmet", new Item("AbsoluteHelmet", 50100, "AbsoluteHelmet_Sprite"));
//        equipmentDict.Add("AbsoluteTop", new Item("AbsoluteTop", 50200, "AbsoluteTop_Sprite"));
//        equipmentDict.Add("AbsolutePants", new Item("AbsolutePants", 50300, "AbsolutePants_Sprite"));
//        equipmentDict.Add("AbsoluteShoes", new Item("AbsoluteShoes", 50400, "AbsoluteShoes_Sprite"));
//        equipmentDict.Add("AbsoluteSword", new Item("AbsoluteSword", 50500, "AbsoluteSword_Sprite"));
//        equipmentDict.Add("AbsoluteGun", new Item("AbsoluteGun", 50600, "AbsoluteGun_Sprite"));

//        // Type Initialization
//        typeDict.Add("�����ִ�", new Type("�����ִ�", 1, "Romantic_Sprite"));
//        typeDict.Add("�����", new Type("�����", 2, "Fast_Sprite"));
//        typeDict.Add("�����ϼ���", new Type("�����ϼ���", 3, "Powerful_Sprite"));
//        typeDict.Add("���ְ�����", new Type("���ְ�����", 4, "DrinkingDancing_Sprite"));
//        typeDict.Add("�ұ���", new Type("�ұ���", 5, "Indomitable_Sprite"));
//        typeDict.Add("������", new Type("������", 6, "Advancing_Sprite"));

//        // Color Initialization
//        colorDict.Add("����", new Color("����", "FF0000", "Red_Sprite"));
//        colorDict.Add("��Ȳ", new Color("��Ȳ", "FF5E00", "Orange_Sprite"));
//        colorDict.Add("���", new Color("���", "FFE400", "Yellow_Sprite"));
//        colorDict.Add("�ʷ�", new Color("�ʷ�", "1DDB16", "Green_Sprite"));
//        colorDict.Add("�Ķ�", new Color("�Ķ�", "00D8FF", "Blue_Sprite"));
//        colorDict.Add("����", new Color("����", "0100FF", "DeepBlue_Sprite"));
//        colorDict.Add("����", new Color("����", "3F0099", "Purple_Sprite"));

//        // Rank Initialization
//        rankDict.Add("Normal", new Ranked("Normal", 100, "Normal_Sprite"));
//        rankDict.Add("Epic", new Ranked("Epic", 200, "Epic_Sprite"));
//        rankDict.Add("Unique", new Ranked("Unique", 300, "Unique_Sprite"));
//        rankDict.Add("Legendry", new Ranked("Legendry", 400, "Legendry_Sprite"));

//        // Create a random item
//        string randomEquipment = GetRandomKey(equipmentDict);
//        string randomType = GetRandomKey(typeDict);
//        string randomColor = GetRandomKey(colorDict);
//        string randomRank = GetRandomKey(rankDict);

//        Item createdItem = CreateItem(randomEquipment, randomType, randomColor, randomRank);
//    }


//    string GetRandomKey<T>(Dictionary<string, T> dict)
//    {
//        List<string> keys = new List<string>(dict.Keys);
//        return keys[Random.Range(0, keys.Count)];
//    }

//    Item CreateItem(string equipment, string type, string color, string rank)
//    {
//        int code = equipmentDict[equipment].code + typeDict[type].code + colorDict[color].code + rankDict[rank].code;
//        string spriteName = equipment + type + color + rank; // Assuming the combined sprite is pre-made
//        return new Item(equipment + type + color + rank, code, spriteName);
//    }
//}

//public class Item
//{
//    public string name;
//    public int code;
//    public Sprite sprite;

//    public Item(string name, int code, string spriteName)
//    {
//        this.name = name;
//        this.code = code;
//        this.sprite = Resources.Load<Sprite>(spriteName);
//    }
//}

//public class Type : Item
//{
//    public Type(string name, int code, string spriteName) : base(name, code, spriteName) { }
//}

//public class Color : Item
//{
//    public Color(string name, string code, string spriteName) : base(name, code, spriteName) { }
//}

//public class Ranked : Item
//{
//    public Ranked(string name, int code, string spriteName) : base(name, code, spriteName) { }
//}
