using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;    // 다른 class에서 접근할 수 있게
    private void Awake()
    {
        instance = this; 
    }

    public List<Item> itemDB = new List<Item>();
    [Space(20)]    
    public GameObject fieldItemPrefab;   
    public Vector3[] pos;        // 생성되는 위치 정하는 배열    

    private void Start()
    {
        // 시작할 때 아이템 몇 개 생성해 놓는 것(연습)
        for(int i = 0; i < 5; i++)
        {
            GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0,5)]);
        } 
    }
    
}
