using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    static public PlayerMove Instance;

    public string playerCurrentMap; // TownToRoad1 스크립트에 있는 transferMapName 변수의 값을 저장.
    public string playerCurrentMapSecond;
    public string playerCurrentMapThird;
    public string playerCurrentMapFourth;

    public const float moveSpeed = 7f;
    public const float jumpForce = 25f;
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Animator anim;




    void Start()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        anim = GetComponent<Animator>();
        anim.SetBool("isWalking", false);
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
            anim.SetBool("isJumping", true);
        }

        // 착지 후 점프 애니메이션 종료
        if (!IsJumping() && anim.GetBool("isJumping") && IsGrounded())
        {
            anim.SetBool("isJumping", false);
        }

        // 방향 전환
        if (Input.GetButton("Horizontal"))
        {
            float direction = Input.GetAxisRaw("Horizontal");
            if (direction == -1) // 왼쪽으로 이동
            {
                transform.localScale = new Vector3(-3.625789f, 3.625789f, 3.625789f);
            }
            else if (direction == 1) // 오른쪽으로 이동
            {
                transform.localScale = new Vector3(3.625789f, 3.625789f, 3.625789f);
            }
        }


        //Raycast 선 확인
        Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));

        // Walkiing animation
        if (Mathf.Abs(rb.velocity.x) < 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
    }
    
    //finish 태그에 닿았을 시 게임오버
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Finish"))
        {
            GameManager.Instance.GameOver();
        }
    }


    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector3.down, 0.7f, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }

    bool IsJumping()
    {
        return rb.velocity.y > 0.01f;
    }
}