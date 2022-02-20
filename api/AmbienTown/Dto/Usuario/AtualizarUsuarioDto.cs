namespace AmbienTown.Dto.Usuario
{
    public class AtualizarUsuarioDto
    {
        public int Id { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public int PersonagemId { get; set; }
        public int ConfiguracaoId { get; set; }
    }
}