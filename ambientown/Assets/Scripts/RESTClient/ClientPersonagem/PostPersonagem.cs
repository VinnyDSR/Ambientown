using Assets.Lineker.Assets.REST_Client;
using Assets.Lineker.Assets.REST_Client.Enums;
using Assets.Lineker.Assets.REST_Client.Models;
using Assets.Scripts.RESTClient.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostPersonagem : MonoBehaviour
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

    private static int N_Personagem;

    public Text mensagem;

    public void Post()
    {
        //Configura o RequestHeader
        RequestHeader header = new RequestHeader()
        {
            Key = "Content-Type",
            Value = "application/json"
        };

        Personagem personagem = new Personagem()
        {
            Genero = (Genero)PlayerPrefs.GetInt("Gender"),
            Pele = (Pele)PlayerPrefs.GetInt("SelectedSkin"),
            RoupaId = (Clothes)1
        };

        StartCoroutine(RestWebClient.Instance.HttpPost($"{baseUrl}", JsonUtility.ToJson(personagem), (r) => OnRequestComplete(r), new List<RequestHeader> { header }));
    }

    void OnRequestComplete(Response response)
    {
        //caso a requisição seja efetuada com sucesso, o retorno estará em response.Data

        Debug.Log($"Status Code: {response.StatusCode}");
        Debug.Log($"Data: {response.Data}");
        //N_Personagem = int.Parse(response.Data);
        Debug.Log($"Error: {response.Error}");

        var valor = JObject.Parse(response.Data)["value"];
        var mensagemTexto = JObject.Parse(response.Data)["message"];

        int personagemid = JsonConvert.DeserializeObject<int>(valor.ToString());

        if (response.StatusCode == 200)
        {
            PlayerPrefs.SetInt("CharacterId", personagemid);
        }
        else
        {
            mensagem.text = mensagemTexto.ToString();
        }
    }

    public static int getN_Personagem()
    {
        return N_Personagem;
    }
}