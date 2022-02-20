using AmbienTown.Enums;
using TimeSheet.Api.Models;

namespace AmbienTown.Models
{
    public class Personagem : BaseModel
    {
        public Pele Pele { get; set; }
        public Genero Genero { get; set; }
        public Clothes RoupaId { get; set; }
    }
}