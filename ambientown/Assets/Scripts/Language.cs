using UnityEngine;
using UnityEngine.SceneManagement;

public class Language : MonoBehaviour
{
    public void portuguese()
    {
        PlayerPrefs.SetString("language", "pt");
        SceneManager.LoadScene("Main");
    }

    public void english()
    {
        PlayerPrefs.SetString("language", "en");
        SceneManager.LoadScene("Main");
    }
}
