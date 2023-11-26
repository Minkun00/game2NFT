using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    // �������� �⺻ ����
    public string itemName;          // �������� �̸�
    public ItemType itemType;        // �������� ����
    public Sprite itemImage;         // �������� �̹���
    public GameObject itemPrefab;    // �������� ������
    public string itemCode;          // �������� �ڵ�

    // ������ �ڵ��� �� �κп� �����ϴ� �̹���
    public Sprite equipmentImage;
    public Sprite colorImage;
    public Sprite rankedImage;

    public string weaponType;        // ���� ����

    public enum ItemType
    {
        Used,
        Equipment,
        ETC
    }

    // ������ �ڵ带 �����ϴ� ������ �޼��带 ����ϴ�.
    public void GenerateCode()
    {
        if (ItemList.Instance != null)
        {
            this.itemCode = ItemList.Instance.GenerateItemCode(this);
        }
        else
        {
            Debug.LogError("ItemList.Instance is not initialized");
        }
    }
}
