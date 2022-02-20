using AmbienTown.Enums;

namespace AmbienTown.Dto.Configuracao
{
    public class NovaConfiguracaoDto
    {
        public Idioma Idioma { get; set; }
        public bool Som { get; set; }
        public bool Fala { get; set; }
        public bool Coleta { get; set; }
    }
}