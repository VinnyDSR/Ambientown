using AmbienTown.Enums;

namespace AmbienTown.Dto.Personagem
{
    public class NovoPersonagemDto
    {
        public Pele Pele { get; set; }
        public Genero Genero { get; set; }
        public int RoupaId { get; set; }
    }
}