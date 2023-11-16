using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    static public MainCamera instance;

    public Transform target;
    public float speed;

    public BoxCollider2D bound;

    // BoxCollider ������ �ּ� �ִ� xyz���� ����.
    private Vector3 minBound;
    private Vector3 maxBound;

    // ī�޶��� �ݳʺ�, �ݳ����� �ݰ� ����
    private float halfWidth;
    private float halfHeight;

    // ī�޶��� �ݳ��̰��� ���� �Ӽ��� �̿��ϱ� ���� ����
    private Camera theCamera;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    void Start()
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;

    }

    void LateUpdate()
    {
        // ī�޶� �ڵ��󰡴� ������ �̵�
        // Vector3 Lerp�� Vector A(0, 0, 0), Vector3 B(10, 0, 0)�϶�, float t�� �޾Ƽ�, t��(0.0 ~ 1.0)�� ���� A�� B������ ���Ͱ� ��ȯ. t�� 0.2���, (2, 0, 0)��ȯ
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);  // Time.deltaTime : �� �������� �Ϸ�� �ð�
        transform.position = new Vector3(transform.position.x, transform.position.y, this.gameObject.transform.position.z);

        // Clamp�� (x, a, b) �� ��, a�� b ���̿� x�� �ִ��� Ȯ����. 
        // ���̿� ������ x return
        // x < a�̸� a return
        // x > b�̸� b return
        float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
        float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

        this.transform.position = new Vector3(clampedX, clampedY, -10f);
    }

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}
