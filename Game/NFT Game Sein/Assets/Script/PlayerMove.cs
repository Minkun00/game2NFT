using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed; 
    public float jumpPower;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    CapsuleCollider2D capsuleCollider2D;
      

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

   
    void Update()
    {
        // 방향전환
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;  // Input.GetAxisRaw : 방향키 입력 받아옴. 왼쪽 -1 오른쪽 1
        }

        // stop speed
        if (Input.GetButtonUp("Horizontal"))  
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);  // nomalized : 단위 벡터의 상태.

        // Jump
        if (Input.GetKeyDown(KeyCode.LeftAlt) && !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            //anim.SetBool("isJumping", true);
        }

        // animation
        /*
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
        */
    }


    void FixedUpdate()
    {
        // Move by Key Control
        float h = Input.GetAxisRaw("Horizontal");  
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        
        else if (rigid.velocity.x < maxSpeed * (-1))  // Left Max Speed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        
        //Landing Platform
        if (rigid.velocity.y < -2)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));  // 에디터 상에서만 Ray를 그려주는 함수.

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 2, LayerMask.GetMask("Ground"));  // RatCastHit : Ray에 닿은 오브젝트
                                                                                                                      // LayerMask.GetMask로 Platform이라는 Layer에만 적용되게 함.
            
            if (rayHit.collider != null)  // RaycastHit의 변수인 rayHit의 collider로 검색 확인 가능.
            {
                if (rayHit.distance < 1.0f)
                    anim.SetBool("isJumping", false);
            }
            
        }
    }

}
