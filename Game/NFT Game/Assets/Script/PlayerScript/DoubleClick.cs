using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClick : MonoBehaviour, IPointerClickHandler
{
    private float lastClickTime = 0;
    private const float doubleClickTime = 0.2f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if ((Time.time - lastClickTime) < doubleClickTime)
        {
            // ���� Ŭ�� �߻�
            EquipItem();
        }
        lastClickTime = Time.time;
    }

    private void EquipItem()
    {
        // ������ ���� ���� ����
        Debug.Log("������ ����");
    }
}
