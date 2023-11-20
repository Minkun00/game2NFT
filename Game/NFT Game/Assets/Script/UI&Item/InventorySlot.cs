//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class InventorySlot : MonoBehaviour
//{
//    public Image icon;
//    public TextMeshProUGUI itemName_Text;
//    public TextMeshProUGUI itemCount_Text;
//    public GameObject selected_Item;

//    public void Additem(Item _item)
//    {
//        itemName_Text.text = _item.itemName; 
//        icon.sprite = _item.itemIcon;
//        if(Item.ItemType.Use == _item.itemType)
//        {
//            if(_item.itemCount > 0)
//            {
//                itemCount_Text.text = _item.itemCount.ToString();
//            }
//            else
//            {
//                itemCount_Text.text = "";
//            }
//        }
//    }

//    void RemoveItem()
//    {
//        itemName_Text.text = "";
//        itemCount_Text.text = "";
//        icon.sprite = null;
//    }


//}
