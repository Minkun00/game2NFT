using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inven;

    public GameObject inventoryPanel;
    public bool activeInventory = false;

    public Slot[] slots;
    public Transform slotHolder;


    private void Start()
    {
        DontDestroyOnLoad(this);
        inven = Inventory.Instance;
        slots = slotHolder.GetComponentsInChildren<Slot>();
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
            Debug.Log("SetActive activeInventory");
        }
    }

    public void AddSlot()
    {
        inven.SlotCnt = inven.SlotCnt + 1;
        Debug.Log("AddSlot");
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
