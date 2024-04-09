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

                // SwordAnim 컴포넌트가 없는 경우에만 추가
                if (weaponRenderer.gameObject.GetComponent<SwordAnim>() == null)
                {
                    weaponRenderer.gameObject.AddComponent<SwordAnim>();
                }

                AdjustSwordAngle(itemsprite.name);
                // 필요한 경우 여기에 추가적인 SwordAnim 설정 코드를 추가할 수 있습니다.
                break;
        }

        if (equipmentUIslot != null)
        {
            equipmentUIslot.SetEquipmentImage(itempart, itemsprite, colorsprite);
        }
    }
    private void AdjustSwordAngle(string swordType)
    {
        Quaternion newRotation = Quaternion.identity;

        switch (swordType)
        {
            case "ArmySword":
                newRotation = Quaternion.Euler(0, 0, 45); // 예시 각도
                break;
            case "IronSword":
                newRotation = Quaternion.Euler(0, 0, 30); // 예시 각도
                break;
            case "KnightSword":
                newRotation = Quaternion.Euler(0, 0, 15); // 예시 각도
                break;
        }

        weaponRenderer.transform.rotation = newRotation;
    }
}
