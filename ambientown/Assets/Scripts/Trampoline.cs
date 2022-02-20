using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private Animator anim;

    public float jumpForce;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Quando objeto com tag "player" entra em zona de colisão, ativa trigger de animação "jump", toca o som e adicionado força ao objeto encostado.
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("jump");
            FindObjectOfType<AudioManager>().Play("TrampolinesSound");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}
