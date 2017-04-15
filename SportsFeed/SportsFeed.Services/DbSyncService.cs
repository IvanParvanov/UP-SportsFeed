using System.Collections.Generic;
using System.Data.Entity.Migrations;

using Bytes2you.Validation;

using SportsFeed.Data.Contracts;
using SportsFeed.Models.Models.Contracts;
using SportsFeed.Services.Contracts;

namespace SportsFeed.Services
{
    public class DbSyncService : IDbSyncService
    {
        private readonly ISportsFeedDbContext dbContext;
        private readonly IBetInformationService betService;

        public DbSyncService(ISportsFeedDbContext dbContext, IBetInformationService betService)
        {
            Guard.WhenArgument(dbContext, nameof(dbContext)).IsNull().Throw();
            Guard.WhenArgument(betService, nameof(betService)).IsNull().Throw();

            this.dbContext = dbContext;
            this.betService = betService;
        }

        public IEnumerable<IExternalEntity> SyncDatabase()
        {
            var webEntities = this.betService.GetData();

            foreach (var sport in webEntities)
            {
                this.dbContext.Sports.AddOrUpdate(sport);

                this.dbContext.SaveChanges();
            }

            return webEntities;
        }
    }
}
