using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControlHowToPlay : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj;
    public Image profile;
    public TextMeshProUGUI speechText;
    public TextMeshProUGUI actorNameText;

    [Header("Settings")]
    public float typingSpeed;
    private string[] sentences;
    private int index;
    private int tuto;

    public void Speech(Sprite p, string[] txt, string actorName)
    {
        Time.timeScale = 0;
        dialogueObj.SetActive(true);
        profile.sprite = p;
        sentences = txt;
        actorNameText.text = actorName;
        StartCoroutine(TypeSentence());
    }

    IEnumerator TypeSentence()
    {
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
                Time.timeScale = 1;

                Debug.Log("tuto");
                tuto = PlayerPrefs.GetInt("tuto");
                tuto++;
                PlayerPrefs.SetInt("tuto", tuto);
                PlayerPrefs.SetInt("flag", 1);
            }
        }
    }
}