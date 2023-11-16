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

    // BoxCollider 영역의 최소 최대 xyz값을 지님.
    private Vector3 minBound;
    private Vector3 maxBound;

    // 카메라의 반너비, 반높이의 반값 변수
    private float halfWidth;
    private float halfHeight;

    // 카메라의 반높이값을 구할 속성을 이용하기 위한 변수
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
        // 카메라가 뒤따라가는 느낌의 이동
        // Vector3 Lerp는 Vector A(0, 0, 0), Vector3 B(10, 0, 0)일때, float t를 받아서, t값(0.0 ~ 1.0)에 따라 A와 B사이의 벡터값 반환. t가 0.2라면, (2, 0, 0)반환
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);  // Time.deltaTime : 전 프레임이 완료된 시간
        transform.position = new Vector3(transform.position.x, transform.position.y, this.gameObject.transform.position.z);

        // Clamp는 (x, a, b) 일 때, a와 b 사이에 x가 있는지 확인함. 
        // 사이에 있으면 x return
        // x < a이면 a return
        // x > b이면 b return
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
