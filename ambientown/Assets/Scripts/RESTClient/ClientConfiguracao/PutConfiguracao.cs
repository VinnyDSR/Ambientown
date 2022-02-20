using Assets.Lineker.Assets.REST_Client;
using Assets.Lineker.Assets.REST_Client.Enums;
using Assets.Lineker.Assets.REST_Client.Models;
using System.Collections.Generic;
using UnityEngine;

public class PutConfiguracao : MonoBehaviour
{
    [SerializeField]
    private string baseUrl = "https://ambientown.ddns.net/api/configuracoes";

    [SerializeField]
    private string clientId;

    [SerializeField]
    private string clientSecret;

    [SerializeField]
    private Idioma idioma;

    //[SerializeField]
    //private bool coleta;

    //[SerializeField]
    //private bool fala;

    //[SerializeField]
    //private bool som;

    void ChangeConfiguration()
    {
        //Configura o RequestHeader
        RequestHeader header = new RequestHeader()
        {
            Key = "Content-Type",
            Value = "application/json"
        };

        switch (PlayerPrefs.GetString("language"))
        {
            case "pt":
                idioma = Idioma.PORTUGUES;
                break;

            case "en":
                idioma = Idioma.INGLES;
                break;

            default:
                idioma = Idioma.INGLES;
                break;
        }

        Configuracao configuracao = new Configuracao()
        {
            Id = PlayerPrefs.GetInt("ConfigurationId"),
            Idioma = idioma,
            Coleta = true,
            Fala = true,
            Som = true
        };

        StartCoroutine(RestWebClient.Instance.HttpPut($"{baseUrl}", JsonUtility.ToJson(configuracao), (r) => OnRequestComplete(r), new List<RequestHeader> { header }));
    }

    void OnRequestComplete(Response response)
    {
        var status = response.StatusCode;

        Debug.Log("Status Code: " + status);
        //caso a requisição seja efetuada com sucesso, o retorno estará em response.Data
    }
}