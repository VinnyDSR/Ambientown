using Assets.Lineker.Assets.REST_Client;
using Assets.Lineker.Assets.REST_Client.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PostUsuario : MonoBehaviour
{
    [SerializeField]
    private string baseUrl = "https://ambientown.ddns.net/api/usuarios";

    //[SerializeField]
    //private string baseUrlPersonagem = "https://ambientown.ddns.net/api/personagens";

    ////[SerializeField]
    //private string baseUrlConfiguracao = "https://ambientown.ddns.net/api/configuracoes";

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

    public long status;
    public string error;
    public Text mensagem;

    public InputField _email, _senha, _nick;

    public void Postar()
    {
        //Configura o RequestHeader
        RequestHeader header = new RequestHeader()
        {
            Key = "Content-Type",
            Value = "application/json"
        };

        //personagemId = PostPersonagem.getN_Personagem();
        //configuracaoId = PostConfiguracao.getN_Config();

        Usuario usuario = new Usuario()
        {
            Apelido = _nick.text,
            Email = _email.text,
            Senha = _senha.text,
            ConfiguracaoId = PlayerPrefs.GetInt("ConfigurationId"),
            PersonagemId = PlayerPrefs.GetInt("CharacterId")
        };

        StartCoroutine(RestWebClient.Instance.HttpPost($"{baseUrl}", JsonUtility.ToJson(usuario), (r) => OnRequestComplete(r), new List<RequestHeader> { header }));

        if (status == 200 || status == 201)
        {
            SceneManager.LoadScene("Map");
        }
        else
        {
            mensagem.text = error;
        }
    }

    void OnRequestComplete(Response response)
    {
        //caso a requisição seja efetuada com sucesso, o retorno estará em response.Data
        status = response.StatusCode;

        //Exemplo de request inválido
        //{ "value":0,"message":"Senha deve ter no mínimo 8 caracteres"}       

        if (status == 400 || status == 409)
        {
            var message = JObject.Parse(response.Data)["message"];
            //ResponseMessage responseMessage = JsonUtility.FromJson<ResponseMessage>(response.Data);
            //JObject obj = JObject.Parse(response.Data);
            //string respostaMensagem = response.Data["message"];
            mensagem.text = message.ToString();
        }
        else if (status == 201 || status == 200)
        {
            var valor = JObject.Parse(response.Data)["value"];
            int userId = JsonConvert.DeserializeObject<int>(valor.ToString());
            Debug.Log("Id do Usuário ao Cadastrar Usuário: " + userId);
            mensagem.text = "Usuário cadastrado com sucesso!";
            PlayerPrefs.SetInt("UserId", userId);
            SceneManager.LoadScene("Map");
        }
        else
        {
            mensagem.text = response.Error;
        }
    }
}