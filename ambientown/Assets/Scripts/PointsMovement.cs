using UnityEngine;

public class PointsMovement : MonoBehaviour
{
    private bool moveRight = true;
    private bool moveDown = true;

    public float speed = 1f;

    public string typeMovement;

    public Transform a;
    public Transform b;

    void Update()
    {
        // Define tipo de movimento
        if (typeMovement == "h")
            HorizontalMovement();

        if (typeMovement == "v")
            VerticalMovement();
    }


    void HorizontalMovement()
    {
        // Valida se objeto passou dos pontos na horizontal
        if (transform.position.x < a.position.x)
            moveRight = true;
        if (transform.position.x > b.position.x)
            moveRight = false;

        if (moveRight)
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }

    void VerticalMovement()
    {

        // Valida se objeto passou dos pontos na vertical
        if (transform.position.y > a.position.y)
            moveDown = true;
        if (transform.position.y < b.position.y)
            moveDown = false;

        if (moveDown)
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        else
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
    }
}
