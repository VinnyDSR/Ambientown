using UnityEngine;

public class PlatformF : MonoBehaviour
{
    public float fallingTime;

    private TargetJoint2D target;
    private BoxCollider2D boxColl;

    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Quando objeto com tag "player" entra na zona de colisão, toca som e invoca metodo Falling() após o tempo posto no fallingtime
            FindObjectOfType<AudioManager>().Play("JumpPlatformSound");
            Invoke("Falling", fallingTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 9)
        {
            // Destrói game objetc
            Destroy(gameObject);
        }
    }

    void Falling()
    {
        // Desativa componentes do objeto plataforma
        target.enabled = false;
        boxColl.isTrigger = true;
    }
}
