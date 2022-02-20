using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class I18nTextTranslator : MonoBehaviour
{
    public string TextId;
    public Dropdown dropdown;
    public Text m_Text;

    void Start()
    {
        var text = GetComponent<Text>();
        if (text != null)
        {
            text.text = I18n.Fields[TextId];
        }
        dropdown = GetComponent<Dropdown>();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        var text = GetComponent<Text>();
        if (text != null)
        {
            text.text = I18n.Fields[TextId];
        }
    }

    public void HandleInputData(int val)
    {
        string lang;
        if (val == 0)
        {
            lang = "en";
            PlayerPrefs.SetString("language", "en");
        }
        else if (val == 1)
        {
            lang = "pt";
            PlayerPrefs.SetString("language", "pt");
        }
        else
        {
            PlayerPrefs.SetString("language", "en");
            lang = "en";
        }
        I18n.LoadLanguage(lang);
    }
}
