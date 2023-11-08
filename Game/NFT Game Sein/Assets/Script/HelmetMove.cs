using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetMove : MonoBehaviour
{
    SpriteRenderer spriteRenderer1;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer1 = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer1.flipX = Input.GetAxisRaw("Horizontal") == 1;  // Input.GetAxisRaw : ����Ű �Է� �޾ƿ�. ���� -1 ������ 1
        }
    }
}
