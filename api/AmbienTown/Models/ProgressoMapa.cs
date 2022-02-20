using AmbienTown.Enums;
using System;
using TimeSheet.Api.Models;

namespace AmbienTown.Models
{
    public class ProgressoMapa : BaseModel
    {
        public Mapa Mapa { get; set; }
        public int Tempo { get; set; }
        public int Colecionavel { get; set; }
        public int ColecionavelEspecial { get; set; }
        public DateTime DataRegistro { get; set; }
        public int Pontuacao { get; set; }

        public virtual int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}