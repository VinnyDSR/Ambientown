using UnityEngine;

public class TimeMovement : MonoBehaviour
{
    public float moveTime;
    public float speed;
    public string typeMovement;

    private bool dirRight = true;
    private bool dirUp = true;

    private float timer;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("SawSound");
    }

    public void Update()
    {
        //Define o tipo de movimento (horizontal/vertical)
        if (typeMovement == "h")
            HorizontalMovement();
        if (typeMovement == "v")
            VerticalMovement();
    }

    void HorizontalMovement()
    {
        if (dirRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        //timer é incrementado até ser maior ou igual a moveTime, após inverte direção
        timer += Time.deltaTime;
        if (timer >= moveTime)
        {
            dirRight = !dirRight;
            //Ao inverter, seta timer para 0 para ser incrementado novamente 
            timer = 0f;
        }
    }

    void VerticalMovement()
    {
        if (dirUp)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }

        timer += Time.deltaTime;
        if (timer >= moveTime)
        {
            dirUp = !dirUp;
            timer = 0f;
        }
    }
}

