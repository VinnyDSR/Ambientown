using AmbienTown.Enums;

namespace AmbienTown.Dto.Leaderboard
{
    public class ObterLeaderboardDto
    {
        public int Id { get; set; }
        public Mapa Mapa { get; set; }
        public int Pontuacao { get; set; }
        public string Apelido { get; set; }
    }
}