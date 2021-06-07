using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Vjezba.DAL;
using Vjezba.Model;

namespace Vjezba.Web.Controllers
{
	[Route("api/client")]
	[ApiController]
	public class ClientApiController : Controller
	{
		private ClientManagerDbContext _dbContext;

		public ClientApiController(ClientManagerDbContext dbContext) {
			_dbContext = dbContext;
		}

		[HttpGet]
		public IEnumerable<ClientDTO> Get() => GetClientDTOs();

		[HttpGet("{id:int}")]
		public IEnumerable<ClientDTO> Get(int id) => GetClientDTOs().Where(c => c.ID == id);

		[HttpGet("{name:alpha}")]
		public IEnumerable<ClientDTO> Get(string name) => GetClientDTOs().Where(c => c.FullName.Contains(name, StringComparison.CurrentCultureIgnoreCase));

		[HttpPost]
		public async Task<ActionResult<ClientDTO>> Post([FromBody] Client model) {
			model.ID = default;
			EntityEntry<Client> client = _dbContext.Clients.Add(model);
			await _dbContext.SaveChangesAsync();
			return GetClientDTOs().Single(c => c.ID == client.Entity.ID);
		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult<ClientDTO>> Put(int id, [FromBody] Client model) {
			if (model.ID != id)
				throw new ArgumentException();
			_dbContext.Clients.Update(model);
			await _dbContext.SaveChangesAsync();
			return GetClientDTOs().Single(c => c.ID == id);
		}

		[HttpDelete("{id:int}")]
		public IActionResult Delete(int id) {
			_dbContext.Clients.Remove(new Client { ID = id });
			_dbContext.SaveChanges();
			if (_dbContext.Clients.Any(c => c.ID == id))
				return Problem();
			return Ok();
		}

		private IEnumerable<ClientDTO> GetClientDTOs() {
			return _dbContext.Clients.Include(c => c.City).Select(c => (ClientDTO)c);
		}
	}
}
