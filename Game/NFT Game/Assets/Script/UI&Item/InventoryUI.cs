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

        inven.onSlotCountChange += SlotChange;    // onSlotCountChange�� ������ �޼��带 ����
        inven.onChangeItem += RedrawSlotUI;       // OnChangeItem�� ������ �޼��� ����
        inventoryPanel.SetActive(activeInventory);
    }




    private void SlotChange(int val)
    {
       for (int i = 0; i < slots.Length; i++)
        {
            if (i < inven.SlotCnt)
                slots[i].GetComponent<Button>().interactable = true;    // for������ SlotCnt��ŭ�� intractable�� true�� ����.
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
    // �ݺ����� ���� ���Ե��� �ʱ�ȭ�ϰ�, items�� ������ŭ slot�� ä���ִ´�.
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
