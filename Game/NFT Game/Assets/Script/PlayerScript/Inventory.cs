using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Singleton ������ Ư�� Ŭ������ �� �ϳ��� �ν��Ͻ����� �������� �����ϴ� ����
    // Ŭ���� ���ο��� �ش� Ŭ������ �ν��Ͻ��� �����Ѵ�.
    #region Singleton                          
    public static Inventory Instance;      // ���� ���� ����

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);        
        }
        Instance = this;
    }
    #endregion 

    public delegate void OnSlotCountChange(int val);  // �븮�� ����
    public OnSlotCountChange onSlotCountChange;       // �븮�� �ν��Ͻ�ȭ

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;


    public List<Item> items = new List<Item>();       // ȹ���� �������� ���� List�� �� �� ����

    private int slotCnt;
    public int SlotCnt
    {
        get => slotCnt;
        set
        {
            slotCnt = value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }

    private void Start()
    {
        SlotCnt = 4;
    }

    // items����Ʈ�� �������� �߰��� �� �ִ� �޼��� ����
    public bool AddItem(Item _item) 
    {
        if(items.Count < SlotCnt)
        {
            items.Add(_item);
            if(onChangeItem != null)    
            onChangeItem.Invoke();    // ������ �߰��� �����ϸ� onChangeItem ȣ��
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)   // player�� item�� �浹�ϸ� AddItem�� ȣ���ؼ� �ʵ�������� Item������ ���ڷ� �Ѱ��ش�.
    {
        if(collision.CompareTag("FieldItem"))
        {
            FieldItems fieldItems = collision.GetComponent<FieldItems>();
            if(AddItem(fieldItems.GetItem()))           // ������ �߰��Ǹ� AddItem���� true ��ȯ
                fieldItems.DestroyItem();               // �ʵ�������� �������� ������ ȹ��
        }
    }

}
