using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Ground Properties")]
    public LayerMask groundLayer;
    public float groundDistance;
    public bool isGrounded;
    public bool isBlowing;

    public Vector3[] footOffset;
    RaycastHit2D leftCheck;
    RaycastHit2D rightCheck;
    private int direction = 1;

    public float speed;
    public float jumpForce;

    private Rigidbody2D rig;
    private Animator anim;

    public int maxJumps = 2;
    private int jumps;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            PhysicsCheck();
            Move();
            Jump();
        }
    }

    void Move()
    {
        float movement = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if (movement > 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (movement < 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        if (movement == 0f)
        {
            anim.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isBlowing)

        {
            if (jumps > 0 || isGrounded)
            {
                FindObjectOfType<AudioManager>().Play("JumpSound");
                rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                anim.SetBool("jump", true);
                if (jumps == 1)
                {
                    anim.SetBool("double_jump", true);
                }
                jumps -= 1;
            }
            if (jumps == 0)
            {
                return;
            }
        }
    }

    private void PhysicsCheck()
    {
        isGrounded = false;

        leftCheck = Raycast(new Vector2(footOffset[0].x * direction, footOffset[0].y), Vector2.down, groundDistance, groundLayer);
        rightCheck = Raycast(new Vector2(footOffset[1].x * direction, footOffset[1].y), Vector2.down, groundDistance, groundLayer);

        if (leftCheck || rightCheck)
        {
            isGrounded = true;
            anim.SetBool("jump", false);
            anim.SetBool("double_jump", false);
            jumps = maxJumps;
        }
    }

    public RaycastHit2D Raycast(Vector3 origin, Vector2 rayDirection, float length, LayerMask mask)
    {
        Vector3 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + origin, rayDirection, length, mask);

        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + origin, rayDirection * length, color);


        return hit;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            GameController.instance.ShowGameOver();
            //Tocar som de impacto
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Saw")
        {
            //Tocar som de impacto
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            GameController.instance.ShowGameOver();
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 12)
        {
            isBlowing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 12)
        {
            isBlowing = false;
        }
    }
}
