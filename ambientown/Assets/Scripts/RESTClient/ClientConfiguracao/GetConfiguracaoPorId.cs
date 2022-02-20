using Assets.Lineker.Assets.REST_Client;
using Assets.Lineker.Assets.REST_Client.Models;
using UnityEngine;

public class GetConfiguracaoPorId : MonoBehaviour
{
    [SerializeField]
    private string baseUrl = "https://ambientown.ddns.net/api/configuracoes";

    [SerializeField]
    private string clientId;

    [SerializeField]
    private string clientSecret;

    private int configuracaoId;

    void GetConfigurationById()
    {
        //Configura o RequestHeader
        RequestHeader header = new RequestHeader()
        {
            Key = "Content-Type",
            Value = "application/json"
        };

        StartCoroutine(RestWebClient.Instance.HttpGet($"{baseUrl}/{configuracaoId}", (r) => OnRequestComplete(r)));
    }

    void OnRequestComplete(Response response)
    {
        //caso a requisição seja efetuada com sucesso, o retorno estará em response.Data
    }
}