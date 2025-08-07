using AvalonMozi.Domain.Movies;
using AvalonMozi.Domain.Orders;
using AvalonMozi.Domain.Tickets;
using AvalonMozi.Domain.Users;
using Microsoft.EntityFrameworkCore;

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

    }
}
