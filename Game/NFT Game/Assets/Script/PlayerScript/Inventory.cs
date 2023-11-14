using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Singleton 패턴은 특정 클래스가 단 하나의 인스턴스만을 가지도록 보장하는 패턴
    // 클래스 내부에서 해당 클래스의 인스턴스를 관리한다.
    #region Singleton                          
    public static Inventory Instance;      // 정적 변수 생성

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);        
        }
        Instance = this;
    }
    #endregion 

    public delegate void OnSlotCountChange(int val);  // 대리자 정의
    public OnSlotCountChange onSlotCountChange;       // 대리자 인스턴스화

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;


    public List<Item> items = new List<Item>();       // 획득한 아이템을 담을 List를 한 개 생성

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

    // items리스트에 아이템을 추가할 수 있는 메서드 생성
    public bool AddItem(Item _item) 
    {
        if(items.Count < SlotCnt)
        {
            items.Add(_item);
            if(onChangeItem != null)    
            onChangeItem.Invoke();    // 아이템 추가에 성공하면 onChangeItem 호출
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)   // player와 item이 충돌하면 AddItem을 호출해서 필드아이템의 Item정보를 인자로 넘겨준다.
    {
        if(collision.CompareTag("FieldItem"))
        {
            FieldItems fieldItems = collision.GetComponent<FieldItems>();
            if(AddItem(fieldItems.GetItem()))           // 아이템 추가되면 AddItem에서 true 반환
                fieldItems.DestroyItem();               // 필드아이템은 없어지고 아이템 획득
        }
    }

}
