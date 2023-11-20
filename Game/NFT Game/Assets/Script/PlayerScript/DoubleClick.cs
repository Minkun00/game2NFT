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
            // 더블 클릭 발생
            EquipItem();
        }
        lastClickTime = Time.time;
    }

    private void EquipItem()
    {
        // 아이템 착용 로직 구현
        Debug.Log("아이템 착용");
    }
}
