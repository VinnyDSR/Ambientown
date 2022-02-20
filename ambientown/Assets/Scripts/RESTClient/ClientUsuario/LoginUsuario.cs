using Assets.Lineker.Assets.REST_Client;
using Assets.Lineker.Assets.REST_Client.Enums;
using Assets.Lineker.Assets.REST_Client.Models;
using Assets.Scripts.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginUsuario : MonoBehaviour
{
    [SerializeField]
    private string baseUrl = "https://ambientown.ddns.net/api/usuarios/login";

    [SerializeField]
    private string clientId;

    [SerializeField]
    private string clientSecret;

    [SerializeField]
    private string email;

    [SerializeField]
    private string senha;

    public InputField _email, _senha;

    public int userId;

    public int skin;

    public long Status;

    public Text mensagem;

    public void Logar()
    {
        //Configura o RequestHeader
        RequestHeader header = new RequestHeader()
        {
            Key = "Content-Type",
            Value = "application/json"
        };

        Login login = new Login()
        {
            Email = _email.text,
            Senha = _senha.text,
        };

        StartCoroutine(RestWebClient.Instance.HttpPost($"{baseUrl}", JsonUtility.ToJson(login), (r) => OnRequestComplete(r), new List<RequestHeader> { header }));

        if (Status == 200 || Status == 201)
        {            
            SceneManager.LoadScene("Map");
        }
    }

    void OnRequestComplete(Response response)
    {
        Status = response.StatusCode;
        
        if (Status != 200)
        {
            var mensagemTexto = JObject.Parse(response.Data)["message"];
            mensagem.text = mensagemTexto.ToString();
        }
        else
        {
            var valor = JObject.Parse(response.Data)["value"];
            Usuario usuario = JsonConvert.DeserializeObject<Usuario>(valor.ToString());
            userId = usuario.Id;
            Debug.Log("UserId: " + userId);
            SceneManager.LoadScene("Map");
            PlayerPrefs.SetInt("UserId", userId);
            PlayerPrefs.SetString("UserNickname", usuario.Apelido);
            Debug.Log("Apelido: " + usuario.Apelido);
            PlayerPrefs.SetString("UserEmail", usuario.Email);
            Debug.Log("Email: " + usuario.Email);
            PlayerPrefs.SetInt("CharacterId", usuario.PersonagemId);
            Debug.Log("PersonagemId: " + usuario.PersonagemId);
            PlayerPrefs.SetInt("SelectedSkin", (int)usuario.Personagem.Pele);
            Debug.Log("Pele: " + usuario.Personagem.Pele);
            PlayerPrefs.SetInt("Gender", (int)usuario.Personagem.Genero);
            Debug.Log("Genero: " + usuario.Personagem.Genero);
            PlayerPrefs.SetInt("Clothes", (int)usuario.Personagem.RoupaId);
            Debug.Log("Clothes: " + usuario.Personagem.RoupaId);
            PlayerPrefs.SetInt("ConfigurationId", usuario.Configuracao.Id);
            Debug.Log("Configuração Id: " + usuario.Configuracao.Id);
            if (usuario.Configuracao?.Idioma == Idioma.PORTUGUES)
                PlayerPrefs.SetString("language", "pt");
            else
                PlayerPrefs.SetString("language", "en");
            Debug.Log("Idioma: " + usuario.Configuracao?.Idioma);
            PlayerPrefs.SetInt("SelectedSound", TypeConverter.boolToInt(usuario.Configuracao.Som));
            PlayerPrefs.SetInt("SelectedSpeak", TypeConverter.boolToInt(usuario.Configuracao.Fala));
            PlayerPrefs.SetInt("SelectedCollect", TypeConverter.boolToInt(usuario.Configuracao.Coleta));
            GetProgress(usuario.Progressos);
        }        
    }

    private void GetProgress(ICollection<ProgressoMapa> progress)
    {
        if (progress.Any(x => x != null && x.Mapa == Mapa.FOREST && x.UsuarioId == userId))
        {
            var forestProgress = progress.Where(x => x.Mapa == Mapa.FOREST && x.UsuarioId == userId).First();

            PlayerPrefs.SetInt("ForestProgressTime", forestProgress.Tempo);
            PlayerPrefs.SetInt("ForestProgressCollectibles", forestProgress.Colecionavel);
            PlayerPrefs.SetInt("ForestProgressSpecialCollectibles", forestProgress.ColecionavelEspecial);
            PlayerPrefs.SetString("ForestProgressDate", forestProgress.DataRegistro.ToString());
            PlayerPrefs.SetInt("ForestProgressScore", forestProgress.Pontuacao);
            Debug.Log("ForestProgressTime: " + PlayerPrefs.GetInt("ForestProgressTime"));
        }
        else
        {
            PlayerPrefs.SetInt("ForestProgressTime", 0);
            PlayerPrefs.SetInt("ForestProgressCollectibles", 0);
            PlayerPrefs.SetInt("ForestProgressSpecialCollectibles", 0);
            PlayerPrefs.SetString("ForestProgressDate", "");
            PlayerPrefs.SetInt("ForestProgressScore", 0);
        }

        if (progress.Any(x => x != null && x.Mapa == Mapa.CITY && x.UsuarioId == userId))
        {
            var cityProgress = progress.Where(x => x.Mapa == Mapa.CITY && x.UsuarioId == userId).First();

            PlayerPrefs.SetInt("CityProgressTime", cityProgress.Tempo);
            PlayerPrefs.SetInt("CityProgressCollectibles", cityProgress.Colecionavel);
            PlayerPrefs.SetInt("CityProgressSpecialCollectibles", cityProgress.ColecionavelEspecial);
            PlayerPrefs.SetString("CityProgressDate", cityProgress.DataRegistro.ToString());
            PlayerPrefs.SetInt("CityProgressScore", cityProgress.Pontuacao);
        }
        else
        {
            PlayerPrefs.SetInt("CityProgressTime", 0);
            PlayerPrefs.SetInt("CityProgressCollectibles", 0);
            PlayerPrefs.SetInt("CityProgressSpecialCollectibles", 0);
            PlayerPrefs.SetString("CityProgressDate", "");
            PlayerPrefs.SetInt("CityProgressScore", 0);
        }

        if (progress.Any(x => x != null && x.Mapa == Mapa.RIVER && x.UsuarioId == userId))
        {
            var riverProgress = progress.Where(x => x.Mapa == Mapa.RIVER && x.UsuarioId == userId).First();

            PlayerPrefs.SetInt("RiverProgressTime", riverProgress.Tempo);
            PlayerPrefs.SetInt("RiverProgressCollectibles", riverProgress.Colecionavel);
            PlayerPrefs.SetInt("RiverProgressSpecialCollectibles", riverProgress.ColecionavelEspecial);
            PlayerPrefs.SetString("RiverProgressDate", riverProgress.DataRegistro.ToString());
            PlayerPrefs.SetInt("RiverProgressScore", riverProgress.Pontuacao);
        }
        else
        {
            PlayerPrefs.SetInt("RiverProgressTime", 0);
            PlayerPrefs.SetInt("RiverProgressCollectibles", 0);
            PlayerPrefs.SetInt("RiverProgressSpecialCollectibles", 0);
            PlayerPrefs.SetString("RiverProgressDate", "");
            PlayerPrefs.SetInt("RiverProgressScore", 0);
        }        
    }
}