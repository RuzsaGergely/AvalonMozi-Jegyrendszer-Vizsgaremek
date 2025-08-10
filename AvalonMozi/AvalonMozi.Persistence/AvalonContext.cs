using AvalonMozi.Domain.Common;
using AvalonMozi.Domain.Movies;
using AvalonMozi.Domain.Orders;
using AvalonMozi.Domain.Tickets;
using AvalonMozi.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AvalonMozi.Persistence
{
    public class AvalonContext : DbContext
    {
        #region User related contexts
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        #endregion

        #region Ticket related contexts
        public DbSet<Ticket> Tickets { get; set; }
        #endregion

        #region Order related contexts
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<BillingInformation> BillingInformations { get; set; }
        #endregion

        #region Movie related contexts
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieDate> MovieDates { get; set; }
        #endregion

        public AvalonContext(DbContextOptions<AvalonContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.ConfigureRelations(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
        private void ConfigureRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasQueryFilter(x => x.Deleted == false)
                .Navigation(y => y.Roles).AutoInclude();

            modelBuilder.Entity<Role>()
                .HasQueryFilter(x => x.Deleted == false);

            modelBuilder.Entity<Ticket>()
                .HasQueryFilter(x => x.Deleted == false)
                .Navigation(y => y.AssignedTo).AutoInclude();

            modelBuilder.Entity<OrderItem>()
                .HasQueryFilter(x => x.Deleted == false)
                .Navigation(y=>y.Movie).AutoInclude();

            modelBuilder.Entity<Order>()
                .HasQueryFilter(x => x.Deleted == false);

            modelBuilder.Entity<BillingInformation>()
                .HasQueryFilter(x => x.Deleted == false);

            modelBuilder.Entity<Movie>()
                .HasQueryFilter(x => x.Deleted == false)
                .Navigation(y => y.Dates).AutoInclude();

            modelBuilder.Entity<MovieDate>()
                .HasQueryFilter(x => x.Deleted == false);
        }

    }
}
