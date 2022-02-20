using UnityEngine;
using UnityEngine.SceneManagement;



public class KeyCodeDialogue : MonoBehaviour
{

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



    public Sprite profile;

    public string actorName;
    string language;
    public GameObject go;

    [Header("Textos")]
    public string[] speechTxtEn;
    public string[] speechTxtPt;

    public LayerMask playerLayer;
    public float radious;

    private DialogueControl dc;
    bool onRadious;

    private void Start()
    {

        myTxtList = JsonUtility.FromJson<TxtList>(textJSON.text);
        speechTxtEn = myTxtList.npcs.TxtEn;
        speechTxtPt = myTxtList.npcs.TxtPt;




        dc = FindObjectOfType<DialogueControl>();
        language = PlayerPrefs.GetString("language");
        Debug.Log(language);
        if (PlayerPrefs.GetInt("FalarDnv") == 0)
        {
            go.SetActive(true);
        }
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

            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "FOREST" || scene.name == "lvl_3" || scene.name == "lvl_13" || scene.name == "lvl_18" ||
            scene.name == "CITY" || scene.name == "lvl_24" || scene.name == "lvl_26" || scene.name == "lvl_29" || scene.name == "RIVER")
            {
                go.SetActive(true);
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
