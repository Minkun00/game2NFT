using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inven;

    public GameObject inventoryPanel;
    public bool activeInventory = false;

    public Slot[] slots;
    public Transform slotHolder;

    public static InventoryUI Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        inven = Inventory.Instance;
        if (inven == null)
        {
            Debug.LogError("Inventory instance is null!");
            return;
        }

        slots = slotHolder.GetComponentsInChildren<Slot>();
        if (slots.Length == 0)
        {
            Debug.LogError("No slots found!");
            return;
        }

        SceneManager.sceneLoaded += (scene, mode) =>
        {
            RedrawSlotUI();
        };

        inven.onSlotCountChange += SlotChange;    // onSlotCountChange가 참조할 메서드를 정의
        inven.onChangeItem += RedrawSlotUI;       // OnChangeItem이 참조할 메서드 정의
        inventoryPanel.SetActive(activeInventory);
    }




    private void SlotChange(int val)
    {
       for (int i = 0; i < slots.Length; i++)
        {
            if (i < inven.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;    // for문으로 SlotCnt만큼만 intractable을 true로 해줌.
            else
                slots[i].GetComponent<Button>().interactable = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            activeInventory = !activeInventory;
            inventoryPanel.SetActive(activeInventory);
        }
    }


    void RedrawSlotUI()
    {
    // 반복문을 통해 슬롯들을 초기화하고, items의 개수만큼 slot을 채워넣는다.
        for(int i  = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }

        for(int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }        
    }
}
