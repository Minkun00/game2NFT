using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public const float moveSpeed = 10f;
    public const float jumpForce = 25f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 이동
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        // 점프
        if (Input.GetKeyDown(KeyCode.LeftAlt) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // 방향 전환
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;  // Input.GetAxisRaw : 방향키 입력 받아옴. 왼쪽 -1 오른쪽 1
        }

        //Raycast 선 확인
        Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector3.down, 2, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }
}