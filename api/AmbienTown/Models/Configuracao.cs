using AmbienTown.Enums;
using TimeSheet.Api.Models;

namespace AmbienTown.Models
{
    public class Configuracao : BaseModel
    {
        public Idioma Idioma { get; set; }
        public bool Som { get; set; }
        public bool Fala { get; set; }
        public bool Coleta { get; set; }
    }
}