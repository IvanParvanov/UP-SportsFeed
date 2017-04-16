using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

using Bytes2you.Validation;

using SportsFeed.Data.Contracts;
using SportsFeed.Models;
using SportsFeed.Models.Contracts;
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
            var externalEntities = this.betService.GetData();

            var syncDatabase = externalEntities as Sport[] ?? externalEntities.ToArray();
            foreach (var sport in syncDatabase)
            {
                this.dbContext.Sports.AddOrUpdate(sport);
            }

            this.dbContext.SaveChanges();

            return syncDatabase;
        }
    }
}
