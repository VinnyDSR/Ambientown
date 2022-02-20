using AmbienTown.Enums;

namespace AmbienTown.Dto.Configuracao
{
    public class AtualizarConfiguracaoDto
    {
        public int Id { get; set; }
        public Idioma Idioma { get; set; }
        public bool Som { get; set; }
        public bool Fala { get; set; }
        public bool Coleta { get; set; }
    }
}