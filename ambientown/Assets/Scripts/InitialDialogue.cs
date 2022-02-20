using UnityEngine;

public class InitialDialogue : MonoBehaviour
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

    [Header("Textos")]
    public string[] speechTxtEn;
    public string[] speechTxtPt;

    private DialogueControl dc;

    private void Start()
    {

        myTxtList = JsonUtility.FromJson<TxtList>(textJSON.text);
        speechTxtEn = myTxtList.npcs.TxtEn;
        speechTxtPt = myTxtList.npcs.TxtPt;


        dc = FindObjectOfType<DialogueControl>();

        language = PlayerPrefs.GetString("language");
        Debug.Log(language);

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