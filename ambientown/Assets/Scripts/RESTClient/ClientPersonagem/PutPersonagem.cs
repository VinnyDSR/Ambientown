using Assets.Lineker.Assets.REST_Client;
using Assets.Lineker.Assets.REST_Client.Enums;
using Assets.Lineker.Assets.REST_Client.Models;
using Assets.Scripts.RESTClient.Enums;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PutPersonagem : MonoBehaviour
{
    [SerializeField]
    private string baseUrl = "https://ambientown.ddns.net/api/personagens";

    [SerializeField]
    private string clientId;

    [SerializeField]
    private string clientSecret;

    [SerializeField]
    private int pele;

    [SerializeField]
    private int genero;

    [SerializeField]
    private int roupaId;

    [SerializeField]
    private int personagemId;

    public Text message;

    // Start is called before the first frame update
    public void ChangeClothes()
    {
        //Configura o RequestHeader
        RequestHeader header = new RequestHeader()
        {
            Key = "Content-Type",
            Value = "application/json"
        };

        Personagem personagem = new Personagem()
        {
            Id = PlayerPrefs.GetInt("CharacterId"),
            Genero = (Genero)PlayerPrefs.GetInt("Gender"),
            Pele = (Pele)PlayerPrefs.GetInt("SelectedSkin"),
            RoupaId = (Clothes)PlayerPrefs.GetInt("Clothes")
        };

        StartCoroutine(RestWebClient.Instance.HttpPut($"{baseUrl}", JsonUtility.ToJson(personagem), (r) => OnRequestComplete(r), new List<RequestHeader> { header }));
    }

    void OnRequestComplete(Response response)
    {
        //caso a requisição seja efetuada com sucesso, o retorno estará em response.Data
        Debug.Log($"Status Code: {response.StatusCode}");
        Debug.Log($"Data: {response.Data}");
        Debug.Log($"Error: {response.Error}");

        if (response.StatusCode == 200)
        {
            SceneManager.LoadScene("Map");
        }
        else
        {
            var messageText = JObject.Parse(response.Data)["message"];
            message.text = messageText.ToString();
        }
    }
}