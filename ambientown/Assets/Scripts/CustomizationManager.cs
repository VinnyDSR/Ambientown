using UnityEngine;

public class CustomizationManager : MonoBehaviour
{

    [Header("Container")]
    [SerializeField]
    GameObject PlayerCustomizationContainer;

    [Header("Panels")]
    [SerializeField]
    GameObject PlayerMalePanel1;

    [SerializeField]
    GameObject PlayerMalePanel2;

    [SerializeField]
    GameObject PlayerMalePanel3;

    [SerializeField]
    GameObject PlayerFemalePanel1;

    [SerializeField]
    GameObject PlayerFemalePane2;

    [SerializeField]
    GameObject PlayerFemalePane3;

    [Header("Square")]
    [SerializeField]
    GameObject SelectedSquare;

    [Header("Player Male 1")]
    [SerializeField]
    GameObject PlayerMale1_Red;

    [SerializeField]
    GameObject PlayerMale1_Green;

    [SerializeField]
    GameObject PlayerMale1_Blue;

    [Header("Player Male 2")]
    [SerializeField]
    GameObject PlayerMale2_Red;

    [SerializeField]
    GameObject PlayerMale2_Green;

    [SerializeField]
    GameObject PlayerMale2_Blue;

    [Header("Player Male 3")]
    [SerializeField]
    GameObject PlayerMale3_Red;

    [SerializeField]
    GameObject PlayerMale3_Green;

    [SerializeField]
    GameObject PlayerMale3_Blue;

    [Header("Player Female 1")]
    [SerializeField]
    GameObject PlayerFemale1_Pink;

    [SerializeField]
    GameObject PlayerFemale1_Green;

    [SerializeField]
    GameObject PlayerFemale1_Yellow;

    [Header("Player Female 2")]
    [SerializeField]
    GameObject PlayerFemale2_Pink;

    [SerializeField]
    GameObject PlayerFemale2_Green;

    [SerializeField]
    GameObject PlayerFemale2_Yellow;

    [Header("Player Female 3")]
    [SerializeField]
    GameObject PlayerFemale3_Pink;

    [SerializeField]
    GameObject PlayerFemale3_Green;

    [SerializeField]
    GameObject PlayerFemale3_Yellow;

    private int SpawnNumber;
    private int Gender;
    private int Clothes;

    private void Start()
    {
        SpawnNumber = PlayerPrefs.GetInt("SelectedSkin");
        Gender = PlayerPrefs.GetInt("Gender");
        PlayerCustomizationContainer.SetActive(true);
        SelectedSquare.SetActive(true);
        if (Gender == 1)
        {
            switch (SpawnNumber)
            {
                case 1:
                    PlayerMalePanel1.SetActive(true);
                    break;
                case 2:
                    PlayerMalePanel2.SetActive(true);
                    break;
                case 3:
                    PlayerMalePanel3.SetActive(true);
                    break;
                default:
                    Debug.Log("Erro ao exibir painel.");
                    break;
            }
        }
        else if (Gender == 2)
        {
            switch (SpawnNumber)
            {
                case 1:
                    PlayerFemalePanel1.SetActive(true);
                    break;
                case 2:
                    PlayerFemalePane2.SetActive(true);
                    break;
                case 3:
                    PlayerFemalePane3.SetActive(true);
                    break;
                default:
                    Debug.Log("Erro ao exibir painel.");
                    break;
            }
        }
    }


    public void PlayerClothes1(GameObject gameObject)
    {
        PlayerPrefs.SetInt("Clothes", 1);
        SelectedSquare.transform.position = gameObject.transform.position;
    }
    public void PlayerClothes2(GameObject gameObject)
    {
        PlayerPrefs.SetInt("Clothes", 2);
        SelectedSquare.transform.position = gameObject.transform.position;
    }
    public void PlayerClothes3(GameObject gameObject)
    {
        PlayerPrefs.SetInt("Clothes", 3);
        SelectedSquare.transform.position = gameObject.transform.position;
    }
}
