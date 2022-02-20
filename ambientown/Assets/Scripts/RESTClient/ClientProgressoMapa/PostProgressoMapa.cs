using Assets.Lineker.Assets.REST_Client;
using Assets.Lineker.Assets.REST_Client.Enums;
using Assets.Lineker.Assets.REST_Client.Models;
using Assets.Scripts.RESTClient.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostProgressoMapa : MonoBehaviour
{
    [SerializeField]
    private string baseUrl = "https://ambientown.ddns.net/api/progressoMapa";

    [SerializeField]
    private string clientId;

    [SerializeField]
    private string clientSecret;

    [SerializeField]
    private int mapa;

    [SerializeField]
    private int tempo;

    [SerializeField]
    private int colecionavel;

    [SerializeField]
    private int colecionavelEspecial;

    [SerializeField]
    private int usuarioId;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Configura o RequestHeader
            RequestHeader header = new RequestHeader()
            {
                Key = "Content-Type",
                Value = "application/json"
            };

            int mapa;

            switch (SceneManager.GetActiveScene().name)
            {
                case "FOREST":
                    mapa = 1;
                    //PlayerPrefs.SetInt("CityFlag", 1);
                    break;

                case "CITY":
                    mapa = 2;
                    //PlayerPrefs.SetInt("RiverFlag", 1);
                    break;

                case "RIVER":
                    mapa = 3;
                    break;

                default:
                    mapa = 0;
                    Debug.Log("Fase n�o encontrada");
                    break;
            }

            NovoProgressoMapaDto progressoMapa = new NovoProgressoMapaDto()
            {
                Colecionavel = PlayerPrefs.GetInt("score"),
                ColecionavelEspecial = PlayerPrefs.GetInt("scoreSpecialItem"),
                Mapa = (Mapa)mapa,
                Tempo = (int)PlayerPrefs.GetFloat("time"),
                UsuarioId = PlayerPrefs.GetInt("UserId")
            };

            Debug.Log(progressoMapa.Colecionavel);
            Debug.Log(progressoMapa.ColecionavelEspecial);
            Debug.Log(progressoMapa.Mapa);
            Debug.Log(progressoMapa.Pontuacao);
            Debug.Log(progressoMapa.Tempo);
            Debug.Log(progressoMapa.UsuarioId);

            switch (progressoMapa.Mapa)
            {
                case Mapa.FOREST:
                    PlayerPrefs.SetInt("ForestProgressTime", progressoMapa.Tempo);
                    break;
                case Mapa.CITY:
                    PlayerPrefs.SetInt("CityProgressTime", progressoMapa.Tempo);
                    break;
                case Mapa.RIVER:
                    PlayerPrefs.SetInt("RiverProgressTime", progressoMapa.Tempo);
                    break;
                default:
                    Debug.LogError("Fase não encontrada!");
                    break;
            }

            StartCoroutine(RestWebClient.Instance.HttpPost($"{baseUrl}", JsonUtility.ToJson(progressoMapa), (r) => OnRequestComplete(r), new List<RequestHeader> { header }));
            if (SceneManager.GetActiveScene().name == "RIVER")
                SceneManager.LoadScene("Credits");
            else
                SceneManager.LoadScene("Map");
        }

        void OnRequestComplete(Response response)
        {
            //caso a requisição seja efetuada com sucesso, o retorno estará em response.Data
        }
    }
}