using Microsoft.EntityFrameworkCore;
using Vjezba.Model;

namespace Vjezba.DAL
{
	public class ClientManagerDbContext : DbContext
	{

		public DbSet<Client> Clients { get; set; }

		public DbSet<City> Cities { get; set; }

		public DbSet<Meeting> Meetings { get; set; }

		protected ClientManagerDbContext() { }
		public ClientManagerDbContext(DbContextOptions<ClientManagerDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<City>().HasData(new City { ID = 1, Name = "Zagreb" });
			modelBuilder.Entity<City>().HasData(new City { ID = 2, Name = "New York" });
			modelBuilder.Entity<City>().HasData(new City { ID = 3, Name = "London" });
			modelBuilder.Entity<Client>().HasData(new Client { ID = 1, FirstName = "Kristijan", LastName = "Kos", Email = "kkos1@tvz.hr", Gender = 'M', PhoneNumber = "091-0911-091", Address = "Negdje 12", CityID = 1 });
		}
	}
}
