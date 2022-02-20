using AmbienTown.Repositories;
using AmbienTown.Repositories.Context;
using Microsoft.EntityFrameworkCore;

namespace Tests.Repositories
{
    public abstract class RepositoryTest //-V3072
    {
        protected AmbienTownContext Context { get; private set; }
        protected Entities Entities { get; private set; }

        public RepositoryTest()
        {
            this.Context = CreateContext();
            this.Entities = new Entities(this.Context);
        }

        private AmbienTownContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AmbienTownContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options;

            return new AmbienTownContext(options);
        }
    }
}