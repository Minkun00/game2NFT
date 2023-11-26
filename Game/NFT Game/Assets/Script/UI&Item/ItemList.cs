using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public static ItemList Instance;

    // ������ �ڵ�
    Dictionary<string, int> Modifier = new Dictionary<string, int>() {
        { "�����ִ�", 1 },
        { "�����", 2 },
        { "�����ϼ���", 3 },
        { "���ְ�����", 4 },
        { "�ұ���", 5 },
        { "������", 6 } 
    };

    Dictionary<string, int> Equipment = new Dictionary<string, int>()
{
    { "ArmyHelmet", 30100 },
    { "ArmyTop", 30200 },
    { "ArmyPants", 30300 },
    { "ArmyShoes", 30400 },
    { "ArmySword", 30500 },
    { "KnightHelmet", 40100 },
    { "KnightTop", 40200 },
    { "KnightPants", 40300 },
    { "KnightShoes", 40400 },
    { "KnightSword", 40500 },
    { "IronHelmet", 50100 },
    { "IronTop", 50200 },
    { "IronPants", 50300 },
    { "IronShoes", 50400 },
    { "IronSword", 50500 },
    };

    Dictionary<string, string> Color = new Dictionary<string, string>() { 
        { "Red", "FF0000" },
        { "Orange", "FF5E00" }, 
        { "Yellow", "FFE400" }, 
        { "Green", "1DDB16" },
        { "SkyBlue", "00D8FF" },
        { "Blue", "0100FF" },
        { "Purple", "3F0099" } 
    };

    Dictionary<string, int> Ranked = new Dictionary<string, int>() {
        { "Normal", 101 }, 
        { "Epic", 202},
        { "Unique", 303 }, 
        { "Legendry", 404 } 
    };

    // ������ �̹��� �ڵ�
    private Dictionary<string, Sprite> equipmentImages = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> colorImages = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> rankedImages = new Dictionary<string, Sprite>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadImages();
        }
        else
        {
            Debug.LogError("Multiple instances of ItemList detected");
        }
    }



    private void LoadImages()
    {
        // ���� �̹���
        equipmentImages.Add("ArmyHelmet", Resources.Load<Sprite>("Image/Helmet/ArmyHelmet_0"));
        equipmentImages.Add("ArmyTop", Resources.Load<Sprite>("Image/Top/ArmyTop_0"));
        equipmentImages.Add("ArmyPants", Resources.Load<Sprite>("Image/Pants/ArmyPants_0"));
        equipmentImages.Add("ArmyShoes", Resources.Load<Sprite>("Image/Shoes/ArmyShoes_0"));
        equipmentImages.Add("ArmySword", Resources.Load<Sprite>("Image/Sword/ArmySword_0"));

        equipmentImages.Add("KnightHelmet", Resources.Load<Sprite>("Image/Helmet/KnightHelmet_0"));
        equipmentImages.Add("KnightTop", Resources.Load<Sprite>("Image/Top/KnightTop_0"));
        equipmentImages.Add("KnightPants", Resources.Load<Sprite>("Image/Pants/KnightPants_0"));
        equipmentImages.Add("KnightShoes", Resources.Load<Sprite>("Image/Shoes/KnightShoes_0"));
        equipmentImages.Add("KnightSword", Resources.Load<Sprite>("Image/Sword/KnightSword_0"));

        equipmentImages.Add("IronHelmet", Resources.Load<Sprite>("Image/Helmet/IronHelmet_0"));
        equipmentImages.Add("IronTop", Resources.Load<Sprite>("Image/Top/IronTop_0"));
        equipmentImages.Add("IronPants", Resources.Load<Sprite>("Image/Pants/IronPants_0"));
        equipmentImages.Add("IronShoes", Resources.Load<Sprite>("Image/Shoes/IronShoes_0"));
        equipmentImages.Add("IronSword", Resources.Load<Sprite>("Image/Sword/IronSword_0"));

        // ��� �̹���
        colorImages.Add("Red", Resources.Load<Sprite>("Image/Background/Red_0"));
        colorImages.Add("Orange", Resources.Load<Sprite>("Image/Background/Orange_0"));
        colorImages.Add("Yellow", Resources.Load<Sprite>("Image/Background/Yellow_0"));
        colorImages.Add("Green", Resources.Load<Sprite>("Image/Background/Green_0"));
        colorImages.Add("SkyBlue", Resources.Load<Sprite>("Image/Background/SkyBlue_0"));
        colorImages.Add("Blue", Resources.Load<Sprite>("Image/Background/Blue_0"));
        colorImages.Add("Purple", Resources.Load<Sprite>("Image/Background/Purple_0"));

        // ��� �̹���
        rankedImages.Add("Normal", Resources.Load<Sprite>("Image/RankEdge/Normal_0"));
        rankedImages.Add("Epic", Resources.Load<Sprite>("Image/RankEdge/Epic_0"));
        rankedImages.Add("Unique", Resources.Load<Sprite>("Image/RankEdge/Unique_0"));
        rankedImages.Add("Legendry", Resources.Load<Sprite>("Image/RankEdge/Legendry_0"));
    }


    public string GenerateItemCode(Item item)
    {
        string code = "";

        var modifier = Modifier.ElementAt(Random.Range(0, Modifier.Count));
        code += modifier.Value.ToString();

        var equipment = Equipment.ElementAt(Random.Range(0, Equipment.Count));
        code += equipment.Value.ToString();
        item.equipmentImage = GetEquipmentImage(equipment.Key);

        var color = Color.ElementAt(Random.Range(0, Color.Count));
        code += color.Value;
        item.colorImage = GetColorImage(color.Key);

        var ranked = Ranked.ElementAt(Random.Range(0, Ranked.Count));
        code += ranked.Value.ToString();
        item.rankedImage = GetRankedImage(ranked.Key);

        // �������� �̹����� �����մϴ�.
        if (item.equipmentImage != null && item.colorImage != null && item.rankedImage != null)
        {
            // �� �κп��� ���, ���, ��޿� ���� �̹����� �����Ͽ� �������� �̹����� ������ �� �ֽ��ϴ�.
            // �����ϴ� ����� ������Ʈ�� �䱸 ���׿� ���� �޶����ϴ�.
            // ������ ����, �Ʒ� �ڵ�� ��� �̹����� �⺻���� �ϰ�, ���� ��� �̹����� �߰��ϴ� ����Դϴ�.
            Sprite mergedImage = MergeImages(item.equipmentImage, item.colorImage, item.rankedImage);
            item.itemImage = mergedImage;
        }

        return code;
    }

    // �̹����� �����ϴ� �޼���
    Sprite MergeImages(Sprite equipmentImage, Sprite colorImage, Sprite rankedImage)
    {
        // �� �κп��� �̹����� �����ϴ� ������ �����ؾ� �մϴ�.
        // �����ϴ� ����� ������Ʈ�� �䱸 ���׿� ���� �޶����ϴ�.
        // ������ ����, �Ʒ� �ڵ�� ��� �̹����� �⺻���� �ϰ�, ���� ��� �̹����� �߰��ϴ� ����Դϴ�.
        // ���� �ڵ忡���� �� �κ��� �����ϰ� �����ؾ� �մϴ�.
        return equipmentImage;
    }


    // �� ��ҿ� �����ϴ� �̹����� ��ȯ�ϴ� �޼���
    Sprite GetEquipmentImage(string equipment) {
        if (equipmentImages.TryGetValue(equipment, out Sprite image))
        {
            return image;
        }
        else
        {
            Debug.LogError("Invalid equipment: " + equipment);
            return null;
        }
    }

    Sprite GetColorImage(string color) {
        if (colorImages.TryGetValue(color, out Sprite image))
        {
            return image;
        }
        else
        {
            Debug.LogError("Invalid color: " + color);
            return null;
        }
    }

    Sprite GetRankedImage(string ranked) {
        if (rankedImages.TryGetValue(ranked, out Sprite image))
        {
            return image;
        }
        else
        {
            Debug.LogError("Invalid ranked: " + ranked);
            return null;
        }
    }
}
