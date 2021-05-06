using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vjezba.DAL;
using Vjezba.Model;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
	public class ClientController : Controller
	{
		private ClientManagerDbContext _dbContext;

		public ClientController(ClientManagerDbContext dbContext) {
			_dbContext = dbContext;
		}

		public IActionResult Index(ClientFilterModel filter) {
			IQueryable<Client> clientQuery = _dbContext.Clients.Include(c => c.City).AsQueryable();

			filter ??= new ClientFilterModel();

			if (!string.IsNullOrWhiteSpace(filter.FullName))
				clientQuery = clientQuery.Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(filter.FullName.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.Address))
				clientQuery = clientQuery.Where(p => p.Address.ToLower().Contains(filter.Address.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.Email))
				clientQuery = clientQuery.Where(p => p.Email.ToLower().Contains(filter.Email.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.City))
				clientQuery = clientQuery.Where(p => p.CityID != null && p.City.Name.ToLower().Contains(filter.City.ToLower()));

			List<Client> model = clientQuery.OrderBy(c => c.ID).ToList();
			return View(nameof(Index), model);
		}

		public IActionResult Details(int? id = null) {
			Client client = _dbContext.Clients.Include(p => p.City).FirstOrDefault(p => p.ID == id);

			return View(client);
		}

		public IActionResult Create() => View();

		[HttpPost]
		public IActionResult Create(Client model) {
			if (ModelState.IsValid) {
				model.CityID = 1;
				_dbContext.Clients.Add(model);
				_dbContext.SaveChanges();
			return RedirectToAction(nameof(Index));
			}
			return View(model);
		}

		public IActionResult Edit(int id) {
			Client client = _dbContext.Clients.FirstOrDefault(c => c.ID == id);
			return View(client);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Client model) {
			Client client = _dbContext.Clients.FirstOrDefault(c => c.ID == model.ID);
			bool ok = await TryUpdateModelAsync(client);

			if (ModelState.IsValid) {
				await _dbContext.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}
	}
}
