using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    static public MainCamera instance;

    public Transform target;
    public float speed;

    void Start()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void LateUpdate()
    {
        // ī�޶� �ڵ��󰡴� ������ �̵�
        // Vector3 Lerp�� Vector A(0, 0, 0), Vector3 B(10, 0, 0)�϶�, float t�� �޾Ƽ�, t��(0.0 ~ 1.0)�� ���� A�� B������ ���Ͱ� ��ȯ. t�� 0.2���, (2, 0, 0)��ȯ
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);  // Time.deltaTime : �� �������� �Ϸ�� �ð�
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}
