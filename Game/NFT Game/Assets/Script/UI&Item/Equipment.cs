using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Helmet, Top, Pants, Shoes, Sword
}

[System.Serializable]
public class EquipmentItem : MonoBehaviour
{
    public Sprite equipmentSprite;
    public EquipmentType equipmentPart;
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
        switch (equipment.equipmentPart)
        {
            case EquipmentType.Helmet:
                helmetRenderer.sprite = equipment.equipmentSprite;
                break;
            case EquipmentType.Top:
                topRenderer.sprite = equipment.equipmentSprite;
                break;
            case EquipmentType.Pants:
                bottomRenderer.sprite = equipment.equipmentSprite;
                break;
            case EquipmentType.Shoes:
                shoesRenderer.sprite = equipment.equipmentSprite;
                break;
            case EquipmentType.Sword:
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
            case EquipmentType.Pants:
                bottomRenderer.sprite = null;
                break;
            case EquipmentType.Shoes:
                shoesRenderer.sprite = null;
                break;
            case EquipmentType.Sword:
                weaponRenderer.sprite = null;
                break;
        }
    }
}