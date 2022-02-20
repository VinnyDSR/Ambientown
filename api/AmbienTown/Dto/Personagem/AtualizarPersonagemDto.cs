using AmbienTown.Enums;

namespace AmbienTown.Dto.Personagem
{
    public class AtualizarPersonagemDto
    {
        public int Id { get; set; }
        public Pele Pele { get; set; }
        public Genero Genero { get; set; }
        public int RoupaId { get; set; }
    }
}