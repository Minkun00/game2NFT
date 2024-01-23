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

}