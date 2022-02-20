using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterManager : MonoBehaviour
{
    public int id;
    public int speak;
    public string language;

    private int SpawnNumber;
    private int Gender;
    private int clothes;

    private int characterId;

    [SerializeField]
    GameObject RegisterCanvas;

    [SerializeField]
    GameObject PlayerSkinContainer;
    [SerializeField]
    GameObject MalePanel;
    [SerializeField]
    GameObject FemalePanel;
    [SerializeField]
    GameObject GenderButtons;
    [SerializeField]
    GameObject SelectedSquare;

    [SerializeField]
    GameObject ButtonSkin1;
    [SerializeField]
    GameObject ButtonSkin2;
    [SerializeField]
    GameObject ButtonSkin3;
    [SerializeField]
    GameObject ButtonSkin4;
    [SerializeField]
    GameObject ButtonSkin5;
    [SerializeField]
    GameObject ButtonSkin6;
    [SerializeField]
    GameObject ButtonSubmit;

    public InputField password, password2;
    public Text passwordText;

    void Start()
    {
        PlayerPrefs.SetInt("Gender", 1);
        PlayerPrefs.SetInt("Clothes", 1);
        PlayerPrefs.SetInt("SelectedSkin", 1);

        //Carrega cena atual da fase.
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Registration")
        {
            
            //Passa PlayerPrefs para variáveis para serem atribuídos novamente.
            id = PlayerPrefs.GetInt("UserId");
            language = PlayerPrefs.GetString("language");
            speak = PlayerPrefs.GetInt("FalarDnv");
            SpawnNumber = PlayerPrefs.GetInt("SelectedSkin");
            Gender = PlayerPrefs.GetInt("Gender");
            characterId = PlayerPrefs.GetInt("CharacterId");
            clothes = PlayerPrefs.GetInt("Clothes");

            //Deleta todos os PlayerPrefs
            PlayerPrefs.DeleteAll();

            PlayerPrefs.SetInt("FalarDnv", speak);
            PlayerPrefs.SetInt("UserId", id);
            PlayerPrefs.SetString("language", language);
            PlayerPrefs.SetInt("SelectedSkin", SpawnNumber);
            PlayerPrefs.SetInt("Gender", Gender);
            PlayerPrefs.SetInt("CharacterId", characterId);
            PlayerPrefs.SetInt("Clothes", clothes);
        }

        Initial();
        SelectedSquare.transform.position = ButtonSkin1.transform.position;
    }
    void Update()
    {

        if (password.text.Length < 10)
        {

            ButtonSubmit.SetActive(false);
            switch (language)
            {
                case "en":
                    passwordText.text = "Passwords must contain at least 10 characters";
                    break;
                case "pt":
                    passwordText.text = "A senha deve ter no minimo 10 caracteres";
                    break;
                default:
                    passwordText.text = "Passwords must contain at least 10 characters";
                    break;
            }
        }
        else if (password.text == password2.text && password.text.Length > 9)
        {
            ButtonSubmit.SetActive(true);

            passwordText.text = "";
        }
        else if (password.text != password2.text && password.text.Length > 9)
        {

            ButtonSubmit.SetActive(false);
            switch (language)
            {
                
                case "en":
                    passwordText.text = "Passwords do not match";
                    break;
                case "pt":
                    passwordText.text = "As senhas não correspondem";
                    break;
                default:
                    passwordText.text = "Passwords do not match";
                    break;
            }
        }
    }
    public void SkinSelector()
    {
        RegisterCanvas.SetActive(false);
        PlayerSkinContainer.SetActive(true);
        Initial();
    }

    public void MaleSelector()
    {
        PlayerPrefs.SetInt("Gender", 1);
        MalePanel.SetActive(true);
        GenderButtons.SetActive(false);
        SelectedSquare.SetActive(true);
        PlayerPrefs.SetInt("SelectedSkin", 1);
        SelectedSquare.transform.position = ButtonSkin1.transform.position;
    }

    public void FemaleSelector()
    {
        PlayerPrefs.SetInt("Gender", 2);
        FemalePanel.SetActive(true);
        GenderButtons.SetActive(false);
        SelectedSquare.SetActive(true);
        PlayerPrefs.SetInt("SelectedSkin", 1);
        SelectedSquare.transform.position = ButtonSkin4.transform.position;
    }
    public void Initial()
    {
        PlayerSkinContainer.SetActive(true);
        RegisterCanvas.SetActive(false);
        FemalePanel.SetActive(false);
        MalePanel.SetActive(false);
        GenderButtons.SetActive(true);
        SelectedSquare.SetActive(false);
    }

    public void SkinM1()
    {
        PlayerPrefs.SetInt("SelectedSkin", 1);
        SelectedSquare.transform.position = ButtonSkin1.transform.position;
    }
    public void SkinM2()
    {
        PlayerPrefs.SetInt("SelectedSkin", 2);
        SelectedSquare.transform.position = ButtonSkin2.transform.position;
    }
    public void SkinM3()
    {
        PlayerPrefs.SetInt("SelectedSkin", 3);
        SelectedSquare.transform.position = ButtonSkin3.transform.position;
    }

    public void SkinF1()
    {
        PlayerPrefs.SetInt("SelectedSkin", 1);
        SelectedSquare.transform.position = ButtonSkin4.transform.position;
    }

    public void SkinF2()
    {
        PlayerPrefs.SetInt("SelectedSkin", 2);
        SelectedSquare.transform.position = ButtonSkin5.transform.position;
    }

    public void SkinF3()
    {
        PlayerPrefs.SetInt("SelectedSkin", 3);
        SelectedSquare.transform.position = ButtonSkin6.transform.position;
    }

    public void RegisterCanvasAc()
    {
        ButtonSubmit.SetActive(false);
        RegisterCanvas.SetActive(true);
        PlayerSkinContainer.SetActive(false);
    }


}
