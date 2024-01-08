using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Helmet, Top, Bottom, Shoes, Weapon
}

[System.Serializable]
public class EquipmentItem : Item
{
    public Sprite equipmentSprite;
    public EquipmentType equipmentType;
    // 기타 속성들...
}


public class Character : MonoBehaviour
{
    public SpriteRenderer helmetRenderer;
    public SpriteRenderer topRenderer;
    public SpriteRenderer bottomRenderer;
    public SpriteRenderer shoesRenderer;
    public SpriteRenderer weaponRenderer;

    public void Equip(EquipmentItem equipment)
    {
        switch (equipment.equipmentType)
        {
            case EquipmentType.Helmet:
                helmetRenderer.sprite = equipment.equipmentSprite;
                break;
            case EquipmentType.Top:
                topRenderer.sprite = equipment.equipmentSprite;
                break;
            case EquipmentType.Bottom:
                bottomRenderer.sprite = equipment.equipmentSprite;
                break;
            case EquipmentType.Shoes:
                shoesRenderer.sprite = equipment.equipmentSprite;
                break;
            case EquipmentType.Weapon:
                weaponRenderer.sprite = equipment.equipmentSprite;
                break;
        }
    }

    public void Unequip(EquipmentType equipmentType)
    {
        switch (equipmentType)
        {
            case EquipmentType.Helmet:
                helmetRenderer.sprite = null;
                break;
            case EquipmentType.Top:
                topRenderer.sprite = null;
                break;
            case EquipmentType.Bottom:
                bottomRenderer.sprite = null;
                break;
            case EquipmentType.Shoes:
                shoesRenderer.sprite = null;
                break;
            case EquipmentType.Weapon:
                weaponRenderer.sprite = null;
                break;
        }
    }
}