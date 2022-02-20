using AmbienTown.Repositories;
using AmbienTown.Repositories.Interfaces;
using AmbienTown.Services;
using AmbienTown.Services.Interfaces;
using AmbienTown.Utils.AutoMapper;
using AmbienTown.Utils.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace AmbienTown.Utils.DI
{
    public class DependencyInjection
    {
        public static void RegisterDependecies(IServiceCollection services, AppSettings appSettings)
        {
            //Configura todas as services
            services.AddScoped<IConfiguracaoService, ConfiguracaoService>();
            services.AddScoped<IPersonagemService, PersonagemService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IProgressoMapaService, ProgressoMapaService>();

            //services.AddScoped<IPerfilService, PerfilService>();
            //services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ISecurityTokenService, SecurityTokenService>();
            //services.AddScoped<IAutenticacaoService, AutenticacaoService>();
            //services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IRecuperacaoSenhaService, RecuperacaoSenhaService>();


            //Configura todos os repositórios
            services.AddScoped<IConfiguracaoRepository, ConfiguracaoRepository>();
            services.AddScoped<IPersonagemRepository, PersonagemRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IProgressoMapaRepository, ProgressoMapaRepository>();

            services.AddScoped(typeof(Entities));

            RegisterAutoMapper(services);
        }

        private static void RegisterAutoMapper(IServiceCollection services)
        {
            var config = AutoMapperUtils.GetConfigurationMappings();

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}