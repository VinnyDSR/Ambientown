using AmbienTown.Dto.Usuario;
using Bogus;

namespace Tests.Stubs.Usuario
{
    public static class NovoUsuarioDtoStub
    {
        private static readonly Faker faker = new Faker("pt_BR");

        private static NovoUsuarioDto Factory(string apelido, bool ativo, string email, string senha, int configuracaoId, int personagemId) => new NovoUsuarioDto
        {
            Apelido = apelido,
            Ativo = ativo,
            Email = email,
            Senha = senha,
            ConfiguracaoId = configuracaoId,
            PersonagemId = personagemId            
        };

        public static NovoUsuarioDto Completo(int configuracaoId, int personagemId) =>
            Factory(faker.Random.AlphaNumeric(20), true, faker.Person.Email, faker.Random.AlphaNumeric(15), configuracaoId, personagemId);

        public static NovoUsuarioDto SemApelido(int configuracaoId, int personagemId) =>
            Factory(string.Empty, true, faker.Person.Email, faker.Random.AlphaNumeric(15), configuracaoId, personagemId);

        public static NovoUsuarioDto ApelidoMaisDe50Caracteres(int configuracaoId, int personagemId) =>
            Factory(faker.Random.AlphaNumeric(51), true, faker.Person.Email, faker.Random.AlphaNumeric(15), configuracaoId, personagemId);

        public static NovoUsuarioDto ApelidoJaCadastrado(string apelido, int configuracaoId, int personagemId) =>
            Factory(apelido, true, faker.Person.Email, faker.Random.AlphaNumeric(15), configuracaoId, personagemId);

        public static NovoUsuarioDto SemEmail(int configuracaoId, int personagemId) =>
            Factory(faker.Random.AlphaNumeric(20), true, string.Empty, faker.Random.AlphaNumeric(15), configuracaoId, personagemId);

        public static NovoUsuarioDto EmailInvalido(int configuracaoId, int personagemId) =>
            Factory(faker.Random.AlphaNumeric(20), true, faker.Random.String2(10), faker.Random.AlphaNumeric(15), configuracaoId, personagemId);

        public static NovoUsuarioDto EmailMaisDe100Caracteres(int configuracaoId, int personagemId) =>
            Factory(faker.Random.AlphaNumeric(20), true, faker.Random.String2(92) + "@mail.com", faker.Random.AlphaNumeric(15), configuracaoId, personagemId);

        public static NovoUsuarioDto SemSenha(int configuracaoId, int personagemId) =>
            Factory(faker.Random.AlphaNumeric(20), true, faker.Person.Email, string.Empty, configuracaoId, personagemId);

        public static NovoUsuarioDto SenhaMenorQue8Caracteres(int configuracaoId, int personagemId) =>
            Factory(faker.Random.AlphaNumeric(20), true, faker.Person.Email, faker.Random.AlphaNumeric(7), configuracaoId, personagemId);

        public static NovoUsuarioDto SenhaMaiorQue100Caracteres(int configuracaoId, int personagemId) =>
            Factory(faker.Random.AlphaNumeric(20), true, faker.Person.Email, faker.Random.AlphaNumeric(101), configuracaoId, personagemId);

        public static NovoUsuarioDto SemConfiguracao(int personagemId) =>
            Factory(faker.Random.AlphaNumeric(20), true, faker.Person.Email, faker.Random.AlphaNumeric(15), 0, personagemId);

        public static NovoUsuarioDto SemPersonagem(int configuracaoId) =>
            Factory(faker.Random.AlphaNumeric(20), true, faker.Person.Email, faker.Random.AlphaNumeric(15), configuracaoId, 0);
    }
}