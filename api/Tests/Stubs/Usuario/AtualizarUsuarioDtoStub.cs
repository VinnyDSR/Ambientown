using AmbienTown.Dto.Usuario;
using Bogus;

namespace Tests.Stubs.Usuario
{
    public static class AtualizarUsuarioDtoStub
    {
        private static readonly Faker faker = new Faker("pt_BR");

        private static AtualizarUsuarioDto Factory(int id, string apelido, bool ativo, string email, string senha, int configuracaoId, int personagemId) => new AtualizarUsuarioDto
        {
            Id = id,
            Apelido = apelido,
            Ativo = ativo,
            Email = email,
            Senha = senha,
            ConfiguracaoId = configuracaoId,
            PersonagemId = personagemId
        };

        public static AtualizarUsuarioDto Completo(int id, int configuracaoId, int personagemId) =>
            Factory(id, faker.Random.AlphaNumeric(20), true, faker.Person.Email, faker.Random.AlphaNumeric(15), configuracaoId, personagemId);

        public static AtualizarUsuarioDto SemApelido(int id, int configuracaoId, int personagemId) =>
            Factory(id, string.Empty, true, faker.Person.Email, faker.Random.AlphaNumeric(15), configuracaoId, personagemId);

        public static AtualizarUsuarioDto ApelidoMaisDe50Caracteres(int id, int configuracaoId, int personagemId) =>
            Factory(id, faker.Random.AlphaNumeric(51), true, faker.Person.Email, faker.Random.AlphaNumeric(15), configuracaoId, personagemId);

        public static AtualizarUsuarioDto ApelidoJaCadastrado(int id, string apelido, int configuracaoId, int personagemId) =>
            Factory(id, apelido, true, faker.Person.Email, faker.Random.AlphaNumeric(15), configuracaoId, personagemId);

        public static AtualizarUsuarioDto SemEmail(int id, int configuracaoId, int personagemId) =>
            Factory(id, faker.Random.AlphaNumeric(20), true, string.Empty, faker.Random.AlphaNumeric(15), configuracaoId, personagemId);

        public static AtualizarUsuarioDto EmailInvalido(int id, int configuracaoId, int personagemId) =>
            Factory(id, faker.Random.AlphaNumeric(20), true, faker.Random.String2(10), faker.Random.AlphaNumeric(15), configuracaoId, personagemId);

        public static AtualizarUsuarioDto EmailMaisDe100Caracteres(int id, int configuracaoId, int personagemId) =>
            Factory(id, faker.Random.AlphaNumeric(20), true, faker.Random.String2(92) + "@mail.com", faker.Random.AlphaNumeric(15), configuracaoId, personagemId);

        public static AtualizarUsuarioDto SemSenha(int id, int configuracaoId, int personagemId) =>
            Factory(id, faker.Random.AlphaNumeric(20), true, faker.Person.Email, string.Empty, configuracaoId, personagemId);

        public static AtualizarUsuarioDto SenhaMenorQue8Caracteres(int id, int configuracaoId, int personagemId) =>
            Factory(id, faker.Random.AlphaNumeric(20), true, faker.Person.Email, faker.Random.AlphaNumeric(7), configuracaoId, personagemId);

        public static AtualizarUsuarioDto SenhaMaiorQue100Caracteres(int id, int configuracaoId, int personagemId) =>
            Factory(id, faker.Random.AlphaNumeric(20), true, faker.Person.Email, faker.Random.AlphaNumeric(101), configuracaoId, personagemId);

        public static AtualizarUsuarioDto SemConfiguracao(int id, int personagemId) =>
            Factory(id, faker.Random.AlphaNumeric(20), true, faker.Person.Email, faker.Random.AlphaNumeric(15), 0, personagemId);

        public static AtualizarUsuarioDto SemPersonagem(int id, int configuracaoId) =>
            Factory(id, faker.Random.AlphaNumeric(20), true, faker.Person.Email, faker.Random.AlphaNumeric(15), configuracaoId, 0);
    }
}