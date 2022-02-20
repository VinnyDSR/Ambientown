using Assets.Lineker.Assets.REST_Client.Enums;
using System;

namespace Assets.Scripts.RESTClient.Models
{
    public class NovoProgressoMapaDto
    {
        public Mapa Mapa;

        public int Tempo;

        public int Colecionavel;

        public int ColecionavelEspecial;

        public DateTime DataRegistro;

        public int Pontuacao;

        public int UsuarioId;
    }
}