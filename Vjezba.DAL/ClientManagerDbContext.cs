using Microsoft.EntityFrameworkCore;
using Vjezba.Model;

namespace Vjezba.DAL
{
	public class ClientManagerDbContext : DbContext
	{

		public DbSet<Client> Clients { get; set; }

		public DbSet<City> Cities { get; set; }

		public DbSet<Meeting> Meetings { get; set; }

		public ClientManagerDbContext(DbContextOptions<ClientManagerDbContext> options) : base(options) {
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<City>().HasData(new City { ID = 1, Name = "Zagreb" });
			modelBuilder.Entity<City>().HasData(new City { ID = 2, Name = "Velika Gorica" });
			modelBuilder.Entity<City>().HasData(new City { ID = 3, Name = "Vrbovsko" });
		}
	}
}
