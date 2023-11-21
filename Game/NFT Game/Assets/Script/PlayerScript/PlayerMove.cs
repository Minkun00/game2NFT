using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    static public PlayerMove Instance;

    public string playerCurrentMap; // TownToRoad1 ��ũ��Ʈ�� �ִ� transferMapName ������ ���� ����.
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
        // �̵�
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput);
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        // ����
        if (Input.GetKeyDown(KeyCode.LeftAlt) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetBool("isJumping", true);
        }

        // ���� �� ���� �ִϸ��̼� ����
        if (!IsJumping() && anim.GetBool("isJumping") && IsGrounded())
        {
            anim.SetBool("isJumping", false);
        }

        // ���� ��ȯ
        if (Input.GetButton("Horizontal"))
        {
            float direction = Input.GetAxisRaw("Horizontal");
            if (direction == -1) // �������� �̵�
            {
                transform.localScale = new Vector3(-3.625789f, 3.625789f, 3.625789f);
            }
            else if (direction == 1) // ���������� �̵�
            {
                transform.localScale = new Vector3(3.625789f, 3.625789f, 3.625789f);
            }
        }


        //Raycast �� Ȯ��
        Debug.DrawRay(rb.position, Vector3.down, new Color(0, 1, 0));

        // Walkiing animation
        if (Mathf.Abs(rb.velocity.x) < 0.3)
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector3.down, 0.9f, LayerMask.GetMask("Ground"));
        return hit.collider != null;
    }

    bool IsJumping()
    {
        return rb.velocity.y > 0.01f;
    }


    //�ǰݽ� ȿ��
    bool isHurt;
    bool isknockback = false;
    Color halfA = new Color(1, 1, 1, 0.5f);
    Color fullA = new Color(1, 1, 1, 1);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //finish �±׿� ����� �� ���ӿ���
        if (collision.CompareTag("Finish"))
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            Hurt(collision.GetComponentInParent<Enemy>().damage, collision.transform.position);
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
                //anim.SetTrigger("hurt");
                float x = transform.position.x - pos.x;
                if (x < 0)
                    x = 1;
                else
                    x = -1;

                StartCoroutine(Knockback(x));
                StartCoroutine(HurtRoutine());
                StartCoroutine(alphablink());
            }
        }
    }

    IEnumerator Knockback(float dir) // �ǰ� �� �������� ȿ��
    {
        isknockback = true;
        float ctime = 0;
        while (ctime < 0.2f)
        {
            if (transform.rotation.y == 0)
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime * dir);
            else
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime * -1f * dir);

            ctime += Time.deltaTime;
            yield return null;
        }
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