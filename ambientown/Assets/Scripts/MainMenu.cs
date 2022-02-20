using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Registration");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Login");
    }

    public void Config()
    {
        SceneManager.LoadScene("ConfigurationScene");
    }

    public void HomeScreen()
    {
        SceneManager.LoadScene("Main");
    }

    public void Map()
    {
        SceneManager.LoadScene("Map");
    }
    public void Forest()
    {
        PlayerPrefs.SetInt("FalarDnv", 1);
        SceneManager.LoadScene("lvl_1");
    }

    public void City()
    {
        PlayerPrefs.SetInt("FalarDnv", 1);
        SceneManager.LoadScene("lvl_11");
    }


    public void River()
    {
        PlayerPrefs.SetInt("FalarDnv", 1);
        SceneManager.LoadScene("lvl_21");
    }

    public void HowToPlay()
    {

        PlayerPrefs.SetInt("FalarDnv", 1);
        SceneManager.LoadScene("HowToPlay");
    }

    public void LeaderBoard()
    {
        SceneManager.LoadScene("LeaderBoard");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //Teste
    public void CustomizationScene()
    {
        SceneManager.LoadScene("Customization");
    }

    public void PasswordRecovery()
    {
        SceneManager.LoadScene("PasswordRecovery");
    }

}
