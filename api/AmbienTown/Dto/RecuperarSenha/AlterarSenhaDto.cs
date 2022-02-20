using System.ComponentModel.DataAnnotations;

namespace AmbienTown.Dto.RecuperarSenha
{
    public class AlterarSenhaDto
    {
        [Required, DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required, DataType(DataType.Password)]
        [Compare("Senha")]
        public string ConfirmacaoSenha { get; set; }

        [Required]
        public string Token { get; set; }

        public bool IsSuccess { get; set; }
    }
}