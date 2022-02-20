using Assets.Lineker.Assets.REST_Client;
using Assets.Lineker.Assets.REST_Client.Models;
using System.Collections.Generic;
using UnityEngine;

public class PutUsuario : MonoBehaviour
{
    [SerializeField]
    private string baseUrl = "https://ambientown.ddns.net/api/usuarios";

    [SerializeField]
    private string clientId;

    [SerializeField]
    private string clientSecret;

    [SerializeField]
    private string apelido;

    [SerializeField]
    private string email;

    [SerializeField]
    private string senha;

    [SerializeField]
    private bool ativo;

    [SerializeField]
    private int personagemId;

    [SerializeField]
    private int configuracaoId;

    [SerializeField]
    private int usuarioId;

    void ChangeUser()
    {
        //Configura o RequestHeader
        RequestHeader header = new RequestHeader()
        {
            Key = "Content-Type",
            Value = "application/json"
        };

        Usuario usuario = new Usuario()
        {
            Id = usuarioId,
            Apelido = apelido,
            Email = email,
            Senha = senha,
            ConfiguracaoId = configuracaoId,
            PersonagemId = personagemId
        };

        StartCoroutine(RestWebClient.Instance.HttpPut($"{baseUrl}", JsonUtility.ToJson(usuario), (r) => OnRequestComplete(r), new List<RequestHeader> { header }));
    }

    void OnRequestComplete(Response response)
    {
        //caso a requisição seja efetuada com sucesso, o retorno estará em response.Data
    }
}