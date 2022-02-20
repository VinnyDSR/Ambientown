using Assets.Lineker.Assets.REST_Client;
using Assets.Lineker.Assets.REST_Client.Models;
using UnityEngine;

public class GetPersonagemPorId : MonoBehaviour
{
    [SerializeField]
    private string baseUrl = "https://ambientown.ddns.net/api/personagens";

    [SerializeField]
    private string clientId;

    [SerializeField]
    private string clientSecret;

    private int personagemId;

    void GetCharacterById()
    {
        //Configura o RequestHeader
        RequestHeader header = new RequestHeader()
        {
            Key = "Content-Type",
            Value = "application/json"
        };

        StartCoroutine(RestWebClient.Instance.HttpGet($"{baseUrl}/{personagemId}", (r) => OnRequestComplete(r)));
    }

    void OnRequestComplete(Response response)
    {
        //caso a requisi��o seja efetuada com sucesso, o retorno estar� em response.Data
    }
}