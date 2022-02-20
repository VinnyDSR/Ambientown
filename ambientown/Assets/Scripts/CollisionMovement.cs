using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionMovement : MonoBehaviour
{

    private Rigidbody2D rig;
    private Animator anim;

    public float speed;

    public Transform rightCol;
    public Transform leftCol;
    public Transform headPoint;

    public LayerMask layer;

    private bool colliding;

    public BoxCollider2D boxCollider2D;
    public CircleCollider2D circleCollider2D;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);
        colliding = Physics2D.Linecast(rightCol.position, leftCol.position, layer);
        if (colliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }
    }

    bool playerDestroyed = false;
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {

            // Quando o player, ao entrar na zona de colisão, estar com uma altura inferior ao do objeto impactado e o 
            // player não estiver destruído, troca para a animação die (caso tenha), desativa os componentes, toca o som de morte e destrói o objeto impactado.
            // caso contrário, destrói o plaer.
            float height = col.contacts[0].point.y - headPoint.position.y;
            if (height > 0 && !playerDestroyed)
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 2, ForceMode2D.Impulse);
                speed = 0.33f;
                anim.SetTrigger("die");
                boxCollider2D.enabled = false;
                circleCollider2D.enabled = false;
                rig.bodyType = RigidbodyType2D.Kinematic;
                FindObjectOfType<AudioManager>().Play("PlayerDeath");
                Scene scene = SceneManager.GetActiveScene();
                Destroy(gameObject, 0.33f);
            }
            else
            {
                playerDestroyed = true;
                GameController.instance.ShowGameOver();
                FindObjectOfType<AudioManager>().Play("PlayerDeath");
                Destroy(col.gameObject);
            }
        }
    }
}
