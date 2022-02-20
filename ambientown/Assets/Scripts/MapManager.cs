using Assets.Lineker.Assets.REST_Client.Enums;
using Assets.Scripts.Helpers;
using Assets.Scripts.RESTClient.Enums;
using System;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    [Header("Flags")]
    public GameObject city_Flag;
    public GameObject river_Flag;

    [Header("Buttons")]
    [SerializeField]
    GameObject[] customizationSceneButtonPM1;
    [SerializeField]
    GameObject[] customizationSceneButtonPM2;
    [SerializeField]
    GameObject[] customizationSceneButtonPM3;
    [SerializeField]
    GameObject[] customizationSceneButtonPF1;
    [SerializeField]
    GameObject[] customizationSceneButtonPF2;
    [SerializeField]                     
    GameObject[] customizationSceneButtonPF3;

    public int SpawnNumber;
    public int Gender;
    public int Clothes;

    void Start()
    {
        LoadButton();
    }

    private void LoadButton()
    {
        SpawnNumber = PlayerPrefs.GetInt("SelectedSkin");
        Gender = PlayerPrefs.GetInt("Gender");
        Clothes = PlayerPrefs.GetInt("Clothes");

        Debug.Log("Pele: " + PlayerPrefs.GetInt("SelectedSkin"));
        Debug.Log("Genero: " + PlayerPrefs.GetInt("Gender"));
        Debug.Log("Roupa: " + PlayerPrefs.GetInt("Clothes"));

        if (Gender == 1)
        {
            switch (SpawnNumber)
            {
                case 1:
                    customizationSceneButtonPM1[Clothes-1].SetActive(true);
                    break;
                case 2:
                    customizationSceneButtonPM2[Clothes - 1].SetActive(true);
                    break;
                case 3:
                    customizationSceneButtonPM3[Clothes - 1].SetActive(true);
                    break;
                default:
                    Debug.Log("Não existe este personagem!");
                    break;
            }
        }
        else if (Gender == 2)
        {
            switch (SpawnNumber)
            {
                case 1:
                    customizationSceneButtonPF1[Clothes - 1].SetActive(true);
                    break;
                case 2:
                    customizationSceneButtonPF2[Clothes - 1].SetActive(true);
                    break;
                case 3:
                    customizationSceneButtonPF3[Clothes - 1].SetActive(true);
                    break;
                default:
                    Debug.Log("Não existe este personagem!");
                    break;
            }
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Map")
            LoadFlags(city_Flag, river_Flag);
    }

    public void LoadFlags(GameObject flag_City, GameObject flag_River)
    {
        if (IsClothesAvailable.IsClothingAvailable((Mapa)1))
        {
            flag_City.SetActive(true);
        }
        else
        {
            flag_City.SetActive(false);
        }
        if (IsClothesAvailable.IsClothingAvailable((Mapa)2))
        {
            flag_River.SetActive(true);
            flag_City.SetActive(true);
        }
        else
        {
            flag_River.SetActive(false);
        }
        if (IsClothesAvailable.IsClothingAvailable((Mapa)3))
        {
            flag_City.SetActive(true);
            flag_River.SetActive(true);
        }
    }
}
