using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Diagnostics;

using SportsFeed.Data.Contracts;
using SportsFeed.Models;

namespace SportsFeed.Data
{
    public class SportsFeedDbContext : DbContext, ISportsFeedDbContext
    {
        public SportsFeedDbContext()
            : base("name=SportsFeedDbContext")
        {
            this.Database.CreateIfNotExists();
            this.Configuration.AutoDetectChangesEnabled = false;
            //this.Database.Log = this.Write;
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public virtual IDbSet<Sport> Sports { get; set; }

        public virtual IDbSet<Event> Events { get; set; }

        public virtual IDbSet<Match> Matches { get; set; }

        public virtual IDbSet<Bet> Bets { get; set; }

        public virtual IDbSet<Odd> Odds { get; set; }

        private void Write(string str)
        {
            Debug.Write(str);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sport>()
                        .Property(x => x.Name)
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnAnnotation("Index", new IndexAnnotation(new[] { new IndexAttribute("Index") { IsUnique = true } }));

            base.OnModelCreating(modelBuilder);
        }
    }
}
