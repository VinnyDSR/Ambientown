using AmbienTown.Dto.ProgressoMapa;
using AmbienTown.Enums;
using Bogus;
using System;

namespace Tests.Stubs.ProgressoMapa
{
    public static class NovoProgressoMapaDtoStub
    {
        private static readonly Faker faker = new Faker("pt_BR");

        private static NovoProgressoMapaDto Factory(Mapa mapa, int tempo, int colecionavel, int colecionavelEspecial, DateTime dataRegistro, int usuarioId) => new NovoProgressoMapaDto
        {
            Mapa = mapa,
            Tempo = tempo,
            Colecionavel = colecionavel,
            ColecionavelEspecial = colecionavelEspecial,
            DataRegistro = dataRegistro,
            UsuarioId = usuarioId            
        };

        public static NovoProgressoMapaDto Completo(int usuarioId) =>
            Factory((Mapa)faker.Random.Int(1, 3), faker.Random.Number(5000), faker.Random.Number(20), faker.Random.Number(3), DateTime.Now, usuarioId);

        public static NovoProgressoMapaDto CompletoComMapa(int usuarioId, Mapa mapa) =>
            Factory(mapa, faker.Random.Number(5000), faker.Random.Number(20), faker.Random.Number(3), DateTime.Now, usuarioId);

        public static NovoProgressoMapaDto SemMapa(int usuarioId) =>
            Factory(0, faker.Random.Number(5000), faker.Random.Number(20), faker.Random.Number(3), DateTime.Now, usuarioId);

        public static NovoProgressoMapaDto SemTempo(int usuarioId) =>
            Factory((Mapa)faker.Random.Int(1, 3), 0, faker.Random.Number(20), faker.Random.Number(3), DateTime.Now, usuarioId);

        public static NovoProgressoMapaDto SemDataRegistro(int usuarioId) =>
            Factory((Mapa)faker.Random.Int(1, 3), faker.Random.Number(5000), faker.Random.Number(20), faker.Random.Number(3), DateTime.MinValue, usuarioId);

        public static NovoProgressoMapaDto SemUsuario() =>
            Factory((Mapa)faker.Random.Int(1, 3), faker.Random.Number(5000), faker.Random.Number(20), faker.Random.Number(3), DateTime.Now, 0);
    }
}