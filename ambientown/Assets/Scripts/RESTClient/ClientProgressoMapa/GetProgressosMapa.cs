using Assets.Lineker.Assets.REST_Client;
using Assets.Lineker.Assets.REST_Client.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetProgressosMapa : MonoBehaviour
{
    [SerializeField]
    private string baseUrl = "https://ambientown.ddns.net/api/progressoMapa";

    [SerializeField]
    private string clientId;

    [SerializeField]
    private string clientSecret;

    private LeaderBoardController leaderBoardController;

    private void Start()
    {
        leaderBoardController = FindObjectOfType<LeaderBoardController>();
    }

    public void CallLeaderboard(int mapa)
    {
        Debug.Log("Inicio do Call Leaderboard");

        //Configura o RequestHeader
        RequestHeader header = new RequestHeader()
        {
            Key = "Content-Type",
            Value = "application/json"
        };

        StartCoroutine(RestWebClient.Instance.HttpGet($"{baseUrl}/{mapa}", (r) => OnRequestComplete(r)));
    }

    void OnRequestComplete(Response response)
    {
        //caso a requisição seja efetuada com sucesso, o retorno estará em response.Data
        Debug.Log($"Status Code: {response.StatusCode}");
        Debug.Log($"Data: {response.Data}");
        Debug.Log($"Error: {response.Error}");
        var valor = JObject.Parse(response.Data)["value"];
        var mensagem = JObject.Parse(response.Data)["message"];

        Debug.Log("Valor: " + valor);
        Debug.Log("Mensagem: " + mensagem);

        List<Leaderboard> leaderboard = JsonConvert.DeserializeObject<List<Leaderboard>>(valor.ToString());
        Debug.Log("Leaderboard: " + leaderboard);

        leaderboard = leaderboard.OrderByDescending(x => x.Pontuacao).Take(10).ToList();

        leaderBoardController.ShowLeaderboard(leaderboard);
    }
}