using AmbienTown.Enums;
using System;

namespace AmbienTown.Dto.ProgressoMapa
{
    public class NovoProgressoMapaDto
    {
        public Mapa Mapa { get; set; }
        public int Tempo { get; set; }
        public int Colecionavel { get; set; }
        public int ColecionavelEspecial { get; set; }
        public DateTime DataRegistro { get; set; }
        public int Pontuacao { get; set; }
        public int UsuarioId { get; set; }
    }
}