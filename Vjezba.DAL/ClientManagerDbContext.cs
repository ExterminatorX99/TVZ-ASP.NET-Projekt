using Microsoft.EntityFrameworkCore;
using Vjezba.Web.Mock;

namespace Vjezba.DAL
{
	public class ClientManagerDbContext : DbContext
	{

		public DbSet<Client> Clients { get; set; }

		public DbSet<City> Cities { get; set; }

		protected ClientManagerDbContext() { }
		public ClientManagerDbContext(DbContextOptions<ClientManagerDbContext> options) : base(options) { }
	}
}
