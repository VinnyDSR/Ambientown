using UnityEngine;

public class SpecialCollectibles : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D circle;

    public GameObject collected;

    //bloco do arquivo JSON
    public TextAsset textJSON;

    [System.Serializable]
    public class Npc
    {
        public string name;
        public string[] TxtEn;
        public string[] TxtPt;

    }

    [System.Serializable]
    public class TxtList
    {
        public Npc npcs;
    }

    public TxtList myTxtList = new TxtList();

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
        dc = FindObjectOfType<DialogueControl>();

        language = PlayerPrefs.GetString("language");

        //bloco de captura de dados

        myTxtList = JsonUtility.FromJson<TxtList>(textJSON.text);
        speechTxtEn = myTxtList.npcs.TxtEn;
        speechTxtPt = myTxtList.npcs.TxtPt;
        //fim do bloco de captura

        Debug.Log(language);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            // Desativa componentes
            sr.enabled = false;
            circle.enabled = false;
            collected.SetActive(true);

            FindObjectOfType<AudioManager>().Play("ItemSound");

            switch (language)
            {
                case "en":
                    dc.Speech(profile, speechTxtEn, actorNameEn);
                    break;
                case "pt":
                    dc.Speech(profile, speechTxtPt, actorNamePt);
                    break;
            }
            //Chama método para atualizar os SpecialCollectibles
            GameController.instance.UpdateScoreSpecialItem();
            Destroy(gameObject, 0.3f);
        }
    }
    public Sprite profile;

    public string actorNameEn;
    public string actorNamePt;
    string language;

    [Header("Textos")]
    public string[] speechTxtEn;
    public string[] speechTxtPt;


    public LayerMask playerLayer;
    public float radious;

    private DialogueControl dc;
    //bool onRadious;


    //private void FixedUpdate()
    //{
    //    Interact();
    //}

    //public void Interact()
    //{
    //    Collider2D hit = Physics2D.OverlapCircle(transform.position, radious, playerLayer);

    //    if (hit != null)
    //    {
    //        onRadious = true;
    //    }
    //    else
    //    {
    //        onRadious = false;
    //    }
    //}

    //private void nDrawGizmosSelected()
    //{
    //    Gizmos.DrawWireSphere(transform.position, radious);
    //}
}
