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
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // �̵�
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector2(horizontalInput, 0);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        // ����
        if (Input.GetKeyDown(KeyCode.LeftAlt) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // ���� ��ȯ
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;  // Input.GetAxisRaw : ����Ű �Է� �޾ƿ�. ���� -1 ������ 1
        }

        bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
            return hit.collider != null;
        }
    }
}