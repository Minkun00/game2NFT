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
//        typeDict.Add("낭만있는", new Type("낭만있는", 1, "Romantic_Sprite"));
//        typeDict.Add("쾌속의", new Type("쾌속의", 2, "Fast_Sprite"));
//        typeDict.Add("벽력일섬의", new Type("벽력일섬의", 3, "Powerful_Sprite"));
//        typeDict.Add("음주가무의", new Type("음주가무의", 4, "DrinkingDancing_Sprite"));
//        typeDict.Add("불굴의", new Type("불굴의", 5, "Indomitable_Sprite"));
//        typeDict.Add("진격의", new Type("진격의", 6, "Advancing_Sprite"));

//        // Color Initialization
//        colorDict.Add("빨강", new Color("빨강", "FF0000", "Red_Sprite"));
//        colorDict.Add("주황", new Color("주황", "FF5E00", "Orange_Sprite"));
//        colorDict.Add("노랑", new Color("노랑", "FFE400", "Yellow_Sprite"));
//        colorDict.Add("초록", new Color("초록", "1DDB16", "Green_Sprite"));
//        colorDict.Add("파랑", new Color("파랑", "00D8FF", "Blue_Sprite"));
//        colorDict.Add("남색", new Color("남색", "0100FF", "DeepBlue_Sprite"));
//        colorDict.Add("보라", new Color("보라", "3F0099", "Purple_Sprite"));

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
