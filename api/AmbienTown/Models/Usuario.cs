using System.Collections.Generic;
using TimeSheet.Api.Models;

namespace AmbienTown.Models
{
    public class Usuario : BaseModel
    {
        public string Apelido { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public virtual int PersonagemId { get; set; }
        public virtual Personagem Personagem { get; set; }
        public virtual int ConfiguracaoId { get; set; }
        public virtual Configuracao Configuracao { get; set; }

        public ICollection<ProgressoMapa> Progressos { get; set; }
    }
}