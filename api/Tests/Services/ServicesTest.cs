using AmbienTown.Utils.AutoMapper;
using AutoMapper;
using Bogus;

namespace Tests.Services
{
    public class ServicesTest
    {
        private IMapper mapper;
        private Faker faker;

        public IMapper Mapper
        {
            get
            {
                if (mapper == null)
                {
                    mapper = new Mapper(
                        AutoMapperUtils.GetConfigurationMappings()
                    );
                }

                return mapper;
            }
        }

        public Faker Faker
        {
            get
            {
                if (faker == null)
                {
                    faker = new Faker("pt_BR");
                }

                return faker;
            }
        }
    }
}