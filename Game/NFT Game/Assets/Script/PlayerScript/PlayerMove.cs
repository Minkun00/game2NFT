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
    
    bool wasGrounded = false;
    float airborneTime = 0f;
    float maxairborneTime = 0f;

    void Start()
    {
        if (Instance == null)
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

        Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));

        //Walking animation
        if (Mathf.Abs(rb.velocity.x) < 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);

        bool isGrounded = IsGrounded();

        if(isGrounded) //땅에 닿았을 때
        {
            if(!wasGrounded)
            {
                wasGrounded = true;
                airborneTime = 0f;
            }
        }
        else //땅에 닿지 않았을 때
        {
            wasGrounded = false;
            airborneTime += Time.deltaTime;
        }

        if (airborneTime > 1f)
        {
            maxairborneTime = airborneTime;
            if (airborneTime > maxairborneTime) 
                maxairborneTime = airborneTime;
        }
          
        if (maxairborneTime > 1.5f && isGrounded)
        {
            Hurt(10f, transform.position);
            maxairborneTime = 0f;
        //    RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector3.down, 0.9f, LayerMask.GetMask("Ground"));
        //    string layername = LayerMask.LayerToName(hit.collider.gameObject.layer);
        //    if (layername != "NULL")
        //    {
        //        Debug.Log(layername);
        //    }
        //    else
        //        Debug.Log("NULL");
        //    Debug.Log(maxairborneTime);
        //    Debug.Log("Ouch");

        }
        //if(airborneTime != 0f)
        //{
        //    Debug.Log("Airborne Tiem: " + airborneTime);  ///del
        //}
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector3.down, 0.9f, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }

    bool IsJumping()
    {
        return rb.velocity.y > 0.01f || rb.velocity.y < -0.01f;
    }

    //피격시 효과
    bool isHurt;
    bool isknockback = false;
    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    //도적 제외한 obstacle script를 가진 장애물 피격(non rigidbody2D)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //finish 태그에 닿았을 시 게임오버
        if (collision.CompareTag("Finish"))
        {
            GameManager.Instance.GameOver();
        }
        else if(collision.CompareTag("Damage"))
        {
            Hurt(collision.GetComponentInParent<Obstacle>().damage, collision.transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //도적과 충돌시 피격(rigidbody2D)
        Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
        if(enemyScript != null)
        {
            if(collision.gameObject.tag == "Enemy")
            {
                float e_damage = enemyScript.damage;
                Hurt(e_damage, collision.transform.position);
            }
        }
    }

    public void Hurt(float damage, Vector2 pos)
    {
        if (!isHurt)
        {   
            float currentHealth = HP.health;
            isHurt = true;
            currentHealth -= damage;
            HP.health = currentHealth;

            if (currentHealth <= 0)
            {
                GameManager.Instance.GameOver();
            }
            else
            {
                float x = transform.position.x - pos.x;
                float y = transform.position.y - pos.y;

                if (x < 0)
                    x = 1;
                else
                    x = -1;

                if (y < 0)
                    y = 1;
                else
                    y = -1;

                StartCoroutine(Knockback(x,y));
                StartCoroutine(HurtRoutine());
                StartCoroutine(alphablink());
            }
        }
    }

    IEnumerator Knockback(float x_dir, float y_dir) // 피격 시 밀쳐지는 효과
    {
        isknockback = true;
        Vector2 knockbackVelocity = new Vector2(x_dir, y_dir) * 0.2f;

        rb.velocity = knockbackVelocity;

        float ctime = 0;

        while (ctime < 0.2f)
        {
            ctime += Time.deltaTime;
            yield return null;
        }
        rb.velocity = Vector2.zero;
        isknockback = false;
    }

    IEnumerator alphablink() //피격 시 깜빡이는 효과
    {
        while (isHurt)
        {
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = halfA;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = fullA;
        }
    }

    IEnumerator HurtRoutine() //피격시 3초 동안 무적
    {
        yield return new WaitForSeconds(3f);
        isHurt = false;
    }
}