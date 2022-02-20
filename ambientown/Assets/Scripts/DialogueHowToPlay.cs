using UnityEngine;

public class DialogueHowToPlay : MonoBehaviour
{
    public TextAsset textJSON;

    [System.Serializable]
    public class KeyBoard
    {
        public string name;
        public string[] TxtEn;
        public string[] TxtPt;

        //public object KeyA { get; internal set; }
    }

    [System.Serializable]
    public class TxtList
    {
        public KeyBoard KeyA;
        public KeyBoard KeyD;
        public KeyBoard KeyE;
        public KeyBoard KeyP;
        public KeyBoard KeySpaceBar;
        public KeyBoard Collectable;
        public KeyBoard Time;

    }

    public TxtList myTxtList = new TxtList();
    [Header("Profile A")]
    public Sprite profileA;
    public string actorNameAEn;
    public string actorNameAPt;
    public string[] speechTxtEnA;
    public string[] speechTxtPtA;

    [Header("Profile D")]
    public Sprite profileD;
    public string actorNameDEn;
    public string actorNameDPt;
    public string[] speechTxtEnD;
    public string[] speechTxtPtD;


    [Header("Profile E")]
    public Sprite profileE;
    public string actorNameEEn;
    public string actorNameEPt;
    public string[] speechTxtEnE;
    public string[] speechTxtPtE;


    [Header("Profile P")]
    public Sprite profileP;
    public string actorNamePEn;
    public string actorNamePPt;
    public string[] speechTxtEnP;
    public string[] speechTxtPtP;


    [Header("Profile SpaceBar")]
    public Sprite profileSpace;
    public string actorNameSpaceEn;
    public string actorNameSpacePt;
    public string[] speechTxtEnSpace;
    public string[] speechTxtPtSpace;


    [Header("Profile TextCollectable")]
    public Sprite profileCollectable;
    public string actorNameCollectableEn;
    public string actorNameCollectablePt;
    public string[] speechTxtEnCollectable;
    public string[] speechTxtPtCollectable;


    [Header("Profile TextTime")]
    public Sprite profileTime;
    public string actorNameTimeEn;
    public string actorNameTimePt;
    public string[] speechTxtEnTime;
    public string[] speechTxtPtTime;

    //[Header("Textos")]
    //public string[] speechTxtEn;
    //public string[] speechTxtPt;

    string language;
    private DialogueControlHowToPlay dc;

    private void Start()
    {
        Debug.Log("Language: " + PlayerPrefs.GetString("language"));
        Debug.Log("FalarDnv: " + PlayerPrefs.GetString("FalarDnv"));
        
        myTxtList = JsonUtility.FromJson<TxtList>(textJSON.text);

        speechTxtEnA = myTxtList.KeyA.TxtEn;
        speechTxtPtA = myTxtList.KeyA.TxtPt;
        speechTxtEnD = myTxtList.KeyD.TxtEn;
        speechTxtPtD = myTxtList.KeyD.TxtPt;
        speechTxtEnE = myTxtList.KeyE.TxtEn;
        speechTxtPtE = myTxtList.KeyE.TxtPt;
        speechTxtEnP = myTxtList.KeyP.TxtEn;
        speechTxtPtP = myTxtList.KeyP.TxtPt;
        speechTxtEnSpace = myTxtList.KeySpaceBar.TxtEn;
        speechTxtPtSpace = myTxtList.KeySpaceBar.TxtPt;
        speechTxtEnCollectable = myTxtList.Collectable.TxtEn;
        speechTxtPtCollectable = myTxtList.Collectable.TxtPt;
        speechTxtEnTime = myTxtList.Time.TxtEn;
        speechTxtPtTime = myTxtList.Time.TxtPt;

        language = PlayerPrefs.GetString("language");
        PlayerPrefs.SetInt("flag", 1);
        dc = FindObjectOfType<DialogueControlHowToPlay>();
        language = PlayerPrefs.GetString("language");

        PlayerPrefs.SetInt("tuto", 0);


        
        DialogueA();
      



    }

    private void Update()
    {

        if (PlayerPrefs.GetInt("flag") == 1)
        {

            PlayerPrefs.SetInt("flag", 0);

            Debug.Log("TUto:" + PlayerPrefs.GetInt("tuto"));
            Debug.Log("FalarDNV:" + PlayerPrefs.GetInt("FalarDnv"));

            switch (PlayerPrefs.GetInt("tuto"))
            {
                case 1:
                    DialogueD();
                    break;
                case 2:
                    DialogueE();
                    break;
                case 3:
                    DialogueP();
                    break;
                case 4:
                    DialogueSpaceBar();
                    break;
                case 5:
                    DialogueCollectable();
                    break;
                case 6:
                    DialogueTime();
                    break;
            }
        }
    }
    void DialogueA()
    {


        switch (language)
        {
            case "en":
                dc.Speech(profileA, speechTxtEnA, actorNameAEn);
                speechTxtEnA = myTxtList.KeyA.TxtEn;
                break;
            case "pt":
                dc.Speech(profileA, speechTxtPtA, actorNameAPt);
                speechTxtPtA = myTxtList.KeyA.TxtPt;
                break;
        }
    }

    public void DialogueD()
    {

        Debug.Log(language);
        switch (language)
        {
            case "en":
                dc.Speech(profileD, speechTxtEnD, actorNameDEn);
                speechTxtEnD = myTxtList.KeyD.TxtEn;
                break;
            case "pt":
                dc.Speech(profileD, speechTxtPtD, actorNameDPt);
                speechTxtPtD = myTxtList.KeyD.TxtPt;
                break;
        }
    }

    public void DialogueE()
    {
        switch (language)
        {
            case "en":
                dc.Speech(profileE, speechTxtEnE, actorNameEEn);
                speechTxtEnE = myTxtList.KeyE.TxtEn;
                break;
            case "pt":
                dc.Speech(profileE, speechTxtPtE, actorNameEPt);
                speechTxtPtE = myTxtList.KeyE.TxtPt;
                break;
        }
    }

    public void DialogueP()
    {
        switch (language)
        {
            case "en":
                dc.Speech(profileP, speechTxtEnP, actorNamePEn);
                speechTxtEnP = myTxtList.KeyP.TxtEn;
                break;
            case "pt":
                dc.Speech(profileP, speechTxtPtP, actorNamePPt);
                speechTxtPtP = myTxtList.KeyP.TxtPt;
                break;
        }
    }
    public void DialogueSpaceBar()
    {
        switch (language)
        {
            case "en":
                dc.Speech(profileSpace, speechTxtEnSpace, actorNameSpaceEn);
                speechTxtEnSpace = myTxtList.KeySpaceBar.TxtEn;
                break;
            case "pt":
                dc.Speech(profileSpace, speechTxtPtSpace, actorNameSpacePt);
                speechTxtPtSpace = myTxtList.KeySpaceBar.TxtPt;
                break;
        }
    }

    public void DialogueCollectable()
    {
        switch (language)
        {
            case "en":
                dc.Speech(profileCollectable, speechTxtEnCollectable, actorNameCollectableEn);
                speechTxtEnCollectable = myTxtList.Collectable.TxtEn;
                break;
            case "pt":
                dc.Speech(profileCollectable, speechTxtPtCollectable, actorNameCollectablePt);
                speechTxtPtCollectable = myTxtList.Collectable.TxtPt;
                break;
        }
    }

    public void DialogueTime()
    {
        switch (language)
        {
            case "en":
                dc.Speech(profileTime, speechTxtEnTime, actorNameTimeEn);
                speechTxtEnTime = myTxtList.Time.TxtEn;
                break;
            case "pt":
                dc.Speech(profileTime, speechTxtPtTime, actorNameTimePt);
                speechTxtPtTime = myTxtList.Time.TxtPt;
                break;

        }
        PlayerPrefs.SetInt("FalarDnv", 1);
    }

}
