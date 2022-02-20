using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    //audio
    public AudioSource _as;
    public AudioClip[] audioClipArray;
    public AudioClip[] audioClipArrayEn;
    private int count;
    public string language;







    [Header("Components")]
    public GameObject dialogueObj;
    public Image profile;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI actorNameText;

    [Header("Settings")]
    public float typingSpeed;
    private string[] sentences;
    private int index;



    void Awake()
    {
        _as = GetComponent<AudioSource>();
        language = PlayerPrefs.GetString("language");
    }




    public void Speech(Sprite p, string[] txt, string actorName)
    {
        Debug.Log(PlayerPrefs.GetInt("FalarDnv"));
        if (PlayerPrefs.GetInt("FalarDnv") == 1)
        {
            Time.timeScale = 0;
            dialogueObj.SetActive(true);
            profile.sprite = p;
            sentences = txt;
            actorNameText.text = actorName;
            StartCoroutine(TypeSentence());
        }
    }

    IEnumerator TypeSentence()
    {
        StartCoroutine(PlayVoice()); // start the audio sample
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    public void NextSentence()
    {
        if (speechText.text == sentences[index])
        {
            //ainda h� textos
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else //lido quando acaba os textos
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                _as.Stop();
                Time.timeScale = 1;

                PlayerPrefs.SetInt("FalarDnv", 0);
            }
        }
    }

    IEnumerator PlayVoice() // Voice audio method
    {


        switch (language)
        {
            case "en":
                if (_as != null)
                {
                    _as.clip = audioClipArrayEn[index];
                    _as.Play();
                }
                break;
            case "pt":
                if (_as != null)
                {
                    _as.clip = audioClipArray[index];
                    _as.Play();
                }
                //_as.Play(_as.clip);
                break;
        }
        yield return new WaitForSecondsRealtime(typingSpeed);
    }
}
