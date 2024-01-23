using UnityEngine;
using UnityEngine.UI;

public class EquipmentUIslot : MonoBehaviour
{
    public Image helmetImage;
    public Image topImage;
    public Image pantsImage;
    public Image shoesImage;
    public Image swordImage;

    public Image helmetColorImage;
    public Image topColorImage;
    public Image pantsColorImage;
    public Image shoesColorImage;
    public Image swordColorImage;

    public Image helmetEquipImage;
    public Image topEquipImage;
    public Image pantsEquipImage;
    public Image shoesEquipImage;
    public Image swordEquipImage;

    public void SetEquipmentImage(string equipmentType, Sprite sprite, Sprite colorsprite)
    {
        switch (equipmentType)
        {
            case "Helmet":
                helmetImage.sprite = sprite;
                helmetEquipImage.sprite = sprite;
                helmetColorImage.sprite = colorsprite;
                Color helmetAlpha = helmetEquipImage.color;
                helmetAlpha.a = 1.0f;
                helmetEquipImage.color = helmetAlpha;
                break;
            case "Top":
                topImage.sprite = sprite;
                topEquipImage.sprite = sprite;
                topColorImage.sprite = colorsprite;
                Color topAlpha = topEquipImage.color;
                topAlpha.a = 1.0f;
                topEquipImage.color = topAlpha;
                break;
            case "Pants":
                pantsImage.sprite = sprite;
                pantsEquipImage.sprite = sprite;
                pantsColorImage.sprite = colorsprite;
                Color pantsAlpha = pantsEquipImage.color;
                pantsAlpha.a = 1.0f;
                pantsEquipImage.color = pantsAlpha;
                break;
            case "Shoes":
                shoesImage.sprite = sprite;
                shoesEquipImage.sprite = sprite;
                shoesColorImage.sprite = colorsprite;
                Color shoesAlpha = shoesEquipImage.color;
                shoesAlpha.a = 1.0f;
                shoesEquipImage.color = shoesAlpha;
                break;
            case "Sword":
                swordImage.sprite = sprite;
                swordEquipImage.sprite = sprite;
                swordColorImage.sprite = colorsprite;
                Color swordAlpha = swordEquipImage.color;
                swordAlpha.a = 1.0f;
                swordEquipImage.color = swordAlpha;
                break;
        }
    }
}
