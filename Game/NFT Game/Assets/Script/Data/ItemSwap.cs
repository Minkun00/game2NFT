using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSwap : MonoBehaviour
{
    public EquipmentUIslot equipmentUIslot; // EquipmentUIslot 스크립트에 대한 참조

    public SpriteRenderer helmetRenderer;
    public SpriteRenderer topRenderer;
    public SpriteRenderer bottomRenderer;
    public SpriteRenderer shoesRenderer;
    public SpriteRenderer weaponRenderer;

    public void Equip(Sprite itemsprite, string itempart, Sprite colorsprite)
    {
        switch (itempart)
        {
            case "Helmet":
                helmetRenderer.sprite = itemsprite;
                helmetRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
                break;
            case "Top":
                topRenderer.sprite = itemsprite;
                topRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
                break;
            case "Pants":
                bottomRenderer.sprite = itemsprite;
                bottomRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
                break;
            case "Shoes":
                shoesRenderer.sprite = itemsprite;
                shoesRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
                break;
            case "Sword":
                weaponRenderer.sprite = itemsprite;
                weaponRenderer.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
                break;
        }

        if (equipmentUIslot != null)
        {
            equipmentUIslot.SetEquipmentImage(itempart, itemsprite, colorsprite);
        }
    }
}
