using Assets.Lineker.Assets.REST_Client.Enums;
using Newtonsoft.Json;

namespace Assets.Lineker.Assets.REST_Client.Models
{
    public class Leaderboard
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("mapa")]
        public Mapa Mapa { get; set; }

        [JsonProperty("pontuacao")]
        public long Pontuacao { get; set; }

        [JsonProperty("apelido")]
        public string Apelido { get; set; }
    }
}