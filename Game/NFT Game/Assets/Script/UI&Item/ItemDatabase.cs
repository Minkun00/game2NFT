using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase instance;    // �ٸ� class���� ������ �� �ְ�
    private void Awake()
    {
        instance = this; 
    }

    public List<Item> itemDB = new List<Item>();
    [Space(20)]    
    public GameObject fieldItemPrefab;   
    public Vector3[] pos;        // �����Ǵ� ��ġ ���ϴ� �迭    

    private void Start()
    {
        // ������ �� ������ �� �� ������ ���� ��(����)
        int itemCount = System.Math.Min(5, pos.Length);
        for (int i = 0; i < itemCount; i++)
        {
            GameObject go = Instantiate(fieldItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0, 5)]);
        }
    }

}


