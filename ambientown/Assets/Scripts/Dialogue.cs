using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public Sprite profile;

    public string actorName;
    string language;

    [Header("Textos")]
    public string[] speechTxtEn;
    public string[] speechTxtPt;


    public LayerMask playerLayer;
    public float radious;

    private DialogueControl dc;
    bool onRadious;

    private void Start()
    {
        dc = FindObjectOfType<DialogueControl>();

        language = PlayerPrefs.GetString("language");
        Debug.Log(language);
    }

    private void FixedUpdate()
    {
        Interact();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && onRadious && Time.timeScale != 0)
        {

            switch (language)
            {
                case "en":

                    dc.Speech(profile, speechTxtEn, actorName);
                    break;
                case "pt":
                    dc.Speech(profile, speechTxtPt, actorName);
                    break;
            }

        }
    }

    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);

        if (hit != null)
        {
            onRadious = true;
        }
        else
        {
            onRadious = false;
        }
    }

    private void nDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radious);
    }
}