namespace AmbienTown.Utils.Email
{
    public class SmtpOptions
    {
        /// <summary>
        /// Informa o servidor de e-mail
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// Informa a porta do servidor
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Informa o usuário que fará o login no servidor de e-mail
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Informa a senha do usuário
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Remetente do e-mail
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// Se for especificado como 'FALSE' será utilizado NetworkCredential para autenticação do usuário no servidor
        /// </summary>
        public bool Relay { get; set; }
        /// <summary>
        /// *** APENAS PARA DESENVOLVIMENTO E TESTES ***
        /// Informa que o e-mail enviado será armazenado em um pasta ao invés de ser enviado para o destinatário.
        /// Se for 'TRUE' serão ignorados os valores das propriedades: User, Password e Relay.
        /// Se for 'TRUE' não será habilitado SSL no envio de e-mail
        /// 
        /// </summary>
        public bool? UseSpecifiedPickupDirectory { get; set; }
        /// <summary>
        /// *** APENAS PARA DESENVOLVIMENTO E TESTES ***
        /// Caminho da pasta que irá armazenar os e-mails
        /// </summary>
        public string PickupDirectoryLocation { get; set; }

        /// <summary>
        /// Número de minutos no qual o código enviado por email será valido
        /// </summary>
        public int ExpiresIn { get; set; }
    }
}