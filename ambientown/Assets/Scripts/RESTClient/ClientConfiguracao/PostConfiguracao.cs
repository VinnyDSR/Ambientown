using Assets.Lineker.Assets.REST_Client;
using Assets.Lineker.Assets.REST_Client.Enums;
using Assets.Lineker.Assets.REST_Client.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PostConfiguracao : MonoBehaviour
{
    [SerializeField]
    private string baseUrl = "https://ambientown.ddns.net/api/configuracoes";

    [SerializeField]
    private string clientId;

    [SerializeField]
    private string clientSecret;

    [SerializeField]
    private int idioma;

    [SerializeField]
    private bool coleta;

    [SerializeField]
    private bool fala;

    [SerializeField]
    private bool som;

    //private static int N_Config;

    // Start is called before the first frame update
    void Start()
    {
        //Configura o RequestHeader
        RequestHeader header = new RequestHeader()
        {
            Key = "Content-Type",
            Value = "application/json"
        };

        Idioma idioma;

        var language = PlayerPrefs.GetString("language");

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
            Idioma = idioma,
            Coleta = true,
            Fala = true,
            Som = true
        };

        StartCoroutine(RestWebClient.Instance.HttpPost($"{baseUrl}", JsonUtility.ToJson(configuracao), (r) => OnRequestComplete(r), new List<RequestHeader> { header }));
    }

    void OnRequestComplete(Response response)
    {
        //caso a requisição seja efetuada com sucesso, o retorno estará em response.Data
        Debug.Log($"Status Code: {response.StatusCode}");
        Debug.Log($"Data: {response.Data}");
        Debug.Log($"Error: {response.Error}");

        var valor = JObject.Parse(response.Data)["value"];
        int configuracaoId = JsonConvert.DeserializeObject<int>(valor.ToString());

        if (response.StatusCode == 200)
        {
            PlayerPrefs.SetInt("ConfigurationId", configuracaoId);
        }
        else
        {
            PlayerPrefs.SetInt("ConfigurationId", 1);
        }
    }

    //public static int getN_Config()
    //{
    //    return N_Config;
    //}
}