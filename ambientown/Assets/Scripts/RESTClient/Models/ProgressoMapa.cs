using Assets.Lineker.Assets.REST_Client.Enums;
using Newtonsoft.Json;
using System;

namespace Assets.Lineker.Assets.REST_Client.Models
{
    public partial class ProgressoMapa
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("mapa")]
        public Mapa Mapa { get; set; }

        [JsonProperty("tempo")]
        public int Tempo { get; set; }

        [JsonProperty("colecionavel")]
        public int Colecionavel { get; set; }

        [JsonProperty("colecionavelEspecial")]
        public int ColecionavelEspecial { get; set; }

        [JsonProperty("dataRegistro")]
        public DateTime DataRegistro { get; set; }

        [JsonProperty("pontuacao")]
        public int Pontuacao { get; set; }

        [JsonProperty("usuarioId")]
        public int UsuarioId { get; set; }
    }
}