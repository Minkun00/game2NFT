using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PlayerMove : MonoBehaviour
{
    static public PlayerMove Instance;

    public string playerCurrentMap; // TownToRoad1 ��ũ��Ʈ�� �ִ� transferMapName ������ ���� ����.
    public string playerCurrentMapSecond;
    public string playerCurrentMapThird;
    public string playerCurrentMapFourth;

    public float moveSpeed = 4f;
    public float jumpForce = 7f;
    public float health;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public Animator anim;
    
    bool wasGrounded = false;
    float airborneTime = 0f;
    float maxairborneTime = 0f;
    public float maxHealth = 100f;
    public float sliderValue;

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
        anim.SetInteger("AnimState", 0);

        health = maxHealth;
    }

    void Update()
    {
        // �̵�
        float horizontalInput = UnityEngine.Input.GetAxis("Horizontal");
        float verticalInput = UnityEngine.Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        // ����
        if (UnityEngine.Input.GetKeyDown(KeyCode.LeftAlt) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetTrigger("Jump");
            anim.SetBool("Grounded", false);
        }

        // ���� �� ���� �ִϸ��̼� ����
        if (!IsJumping() && !anim.GetBool("Grounded") && IsGrounded())
        {
            anim.SetBool("Grounded", true);
        }

        // ���� ��ȯ
        if (UnityEngine.Input.GetButton("Horizontal"))
        {
            float direction = UnityEngine.Input.GetAxisRaw("Horizontal");
            if (direction == -1) // �������� �̵�
            {
                spriteRenderer.flipX = false;
            }
            else if (direction == 1) // ���������� �̵�
            {
                spriteRenderer.flipX = true;
            }
        }

        //Walking animation
        if (Mathf.Abs(rb.velocity.x) < 0.3)
            anim.SetInteger("AnimState", 0);
        else
            anim.SetInteger("AnimState", 2);

        //Attack
        if (UnityEngine.Input.GetKeyDown(KeyCode.LeftControl))
        {
            anim.SetTrigger("Attack");
        }


        Vector3 start = transform.position + new Vector3(0, 1.0f, 0); // Y������ 1��ŭ �̵�
        Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));

        bool IsGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector3.down, 0.5f, LayerMask.GetMask("Ground"));
            return hit.collider != null;
        }

        bool IsJumping()
        {
            return rb.velocity.y > 0.01f || rb.velocity.y < -0.01f;
        }



        // =====================================�ǰ�========================================
    

        bool isGrounded = IsGrounded();

        if(isGrounded) 
        {
            if(!wasGrounded)
            {
                wasGrounded = true;
                airborneTime = 0f;
            }
        }
        else 
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
        }

        sliderValue = health / maxHealth;
    }

    //�ǰݽ� ȿ��
    bool isHurt;
    bool isknockback = false;
    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    //���� ������ obstacle script�� ���� ��ֹ� �ǰ�(non rigidbody2D)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //finish �±׿� ����� �� ���ӿ���
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
        //������ �浹�� �ǰ�(rigidbody2D)
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
            isHurt = true;
            health -= damage;

            if (health <= 0)
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

    IEnumerator Knockback(float x_dir, float y_dir) // �ǰ� �� �������� ȿ��
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

    IEnumerator alphablink() //�ǰ� �� �����̴� ȿ��
    {
        while (isHurt)
        {
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = halfA;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = fullA;
        }
    }

    IEnumerator HurtRoutine() //�ǰݽ� 3�� ���� ����
    {
        yield return new WaitForSeconds(3f);
        isHurt = false;
    }
}