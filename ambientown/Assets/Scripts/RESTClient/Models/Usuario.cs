using System.Collections.Generic;

namespace Assets.Lineker.Assets.REST_Client.Models
{
    public class Usuario
    {
        public int Id;
        public string Apelido;
        public string Email;
        public string Senha;
        public Configuracao Configuracao;
        public int ConfiguracaoId;
        public Personagem Personagem;
        public int PersonagemId;
        public ICollection<ProgressoMapa> Progressos;
    }
}