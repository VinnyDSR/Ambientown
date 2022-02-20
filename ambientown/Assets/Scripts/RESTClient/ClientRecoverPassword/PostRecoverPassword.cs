using Assets.Lineker.Assets.REST_Client;
using Assets.Lineker.Assets.REST_Client.Models;
using Assets.Scripts.RESTClient.Models;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.RESTClient.ClientRecoverPassword
{
    public class PostRecoverPassword : MonoBehaviour
    {
        [SerializeField]
        private string baseUrl = "https://ambientown.ddns.net/api/alteracaoSenha/recuperar";

        [SerializeField]
        private string clientId;

        [SerializeField]
        private string clientSecret;

        [SerializeField]
        private string email;

        public InputField _email;

        public bool success = false;


        public void Button()
        {
            Postar();
        }
        public bool Postar()
        {
            //Configura o RequestHeader
            RequestHeader header = new RequestHeader()
            {
                Key = "Content-Type",
                Value = "application/json"
            };

            RecoverPassword recover = new RecoverPassword()
            {
                Email = _email.text
        };

            StartCoroutine(RestWebClient.Instance.HttpPost($"{baseUrl}", JsonUtility.ToJson(recover), (r) => OnRequestComplete(r), new List<RequestHeader> { header }));

            return success;
        }

        void OnRequestComplete(Response response)
        {
            //caso a requisição seja efetuada com sucesso, o retorno estará em response.Data
            Debug.Log($"Status Code: {response.StatusCode}");
            Debug.Log($"Data: {response.Data}");
            Debug.Log($"Error: {response.Error}");

            if (response.StatusCode == 201)
                success = true;
        }
    }
}