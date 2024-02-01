using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurrentItemList
{
    public string Adjective;
    public string ItemName;
    public string ItemPart;
    public string Rank;
    public string ItemCode;

    public CurrentItemList(string _Adjective, string _ItemName, string _ItemPart, string _Rank, string _Code)
    {
        Adjective = _Adjective;
        ItemName = _ItemName;
        ItemPart = _ItemPart;
        Rank = _Rank;
        ItemCode = _Code;
    }
}

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated;
    public List<CurrentItemList> CurItemList = new List<CurrentItemList>();

    // 필요한 컴포넌트
    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;
    [SerializeField]
    private GameObject go_ShowText;

    // 슬롯들
    private Slot[] slots;

    public static Inventory Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>();
    }

    private void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I) && !inventoryActivated)
        {
            inventoryActivated = true;
            OpenInventory();
        }
        else if ((Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Escape)) && inventoryActivated)
        {
            inventoryActivated = false;
            CloseInventory();
        }
    }


    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        // 아이템이 장비가 아닌 경우
        if (Item.ItemType.Equipment != _item.itemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null)
                {
                    if (slots[i].item.ItemName == _item.ItemName)
                    {
                        slots[i].SetSlotCount(_count);
                        CurItemList.Add(new CurrentItemList(_item.Adjective, _item.ItemName, _item.ItemPart, _item.Rank, _item.ItemCode));
                        return;
                    }
                }
            }
        }

        // 새로운 슬롯에 아이템 추가
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item, _count);
                CurItemList.Add(new CurrentItemList(_item.Adjective, _item.ItemName, _item.ItemPart, _item.Rank, _item.ItemCode));
                return;
            }
        }
    }



}
