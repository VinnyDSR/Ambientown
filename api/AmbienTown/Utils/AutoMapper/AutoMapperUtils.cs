using AmbienTown.Dto.Configuracao;
using AmbienTown.Dto.Personagem;
using AmbienTown.Dto.ProgressoMapa;
using AmbienTown.Dto.Usuario;
using AmbienTown.Models;
using AutoMapper;

namespace AmbienTown.Utils.AutoMapper
{
    public class AutoMapperUtils
    {
        public static IConfigurationProvider GetConfigurationMappings() => new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<AtualizarUsuarioDto, Usuario>().ReverseMap();
            cfg.CreateMap<NovoUsuarioDto, Usuario>().ForMember(x => x.Progressos, cfg => cfg.Ignore()).ReverseMap();
            cfg.CreateMap<ObterUsuarioDto, Usuario>().ReverseMap();
            cfg.CreateMap<AtualizarConfiguracaoDto, Configuracao>().ReverseMap();
            cfg.CreateMap<NovaConfiguracaoDto, Configuracao>().ReverseMap();
            cfg.CreateMap<ObterConfiguracaoDto, Configuracao>().ReverseMap();
            cfg.CreateMap<AtualizarPersonagemDto, Personagem>().ReverseMap();
            cfg.CreateMap<NovoPersonagemDto, Personagem>().ReverseMap();
            cfg.CreateMap<ObterPersonagemDto, Personagem>().ReverseMap();
            cfg.CreateMap<NovoProgressoMapaDto, ProgressoMapa>().ReverseMap();
            cfg.CreateMap<ObterProgressoMapaDto, ProgressoMapa>().ReverseMap();
        });
    }
}