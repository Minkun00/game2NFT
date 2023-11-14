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
        // 카메라가 뒤따라가는 느낌의 이동
        // Vector3 Lerp는 Vector A(0, 0, 0), Vector3 B(10, 0, 0)일때, float t를 받아서, t값(0.0 ~ 1.0)에 따라 A와 B사이의 벡터값 반환. t가 0.2라면, (2, 0, 0)반환
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);  // Time.deltaTime : 전 프레임이 완료된 시간
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}
