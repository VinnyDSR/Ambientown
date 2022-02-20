using UnityEngine;

public class CommonCollectibles : MonoBehaviour
{

    private SpriteRenderer sr;
    private CircleCollider2D circle;

    public GameObject collected;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Quando entra na zona de gatilho, desativa os componentes do coletável, ativa o sprite "collected" e, após 0.3 segundos, destrói o objeto

        if (collider.gameObject.tag == "Player")
        {
            sr.enabled = false;
            circle.enabled = false;
            collected.SetActive(true);

            GameController.instance.UpdateScoreText();

            FindObjectOfType<AudioManager>().Play("ItemSound");

            Destroy(gameObject, 0.3f);
        }
    }
}
