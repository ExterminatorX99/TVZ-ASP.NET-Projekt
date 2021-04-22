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
	}
}
