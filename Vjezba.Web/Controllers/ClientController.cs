using System.Collections.Generic;
using System.Linq;
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
			IQueryable<Client> clientQuery = _dbContext.Clients.AsQueryable();

			filter = filter ?? new ClientFilterModel();

			if (!string.IsNullOrWhiteSpace(filter.FullName))
				clientQuery = clientQuery.Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(filter.FullName.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.Address))
				clientQuery = clientQuery.Where(p => p.Address.ToLower().Contains(filter.Address.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.Email))
				clientQuery = clientQuery.Where(p => p.Email.ToLower().Contains(filter.Email.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.City))
				clientQuery = clientQuery.Where(p => p.CityID != null && p.City.Name.ToLower().Contains(filter.City.ToLower()));

			List<Client> model = clientQuery.ToList();
			return View("Index", model);
		}

		public IActionResult Details(int? id = null) {
			Client? client = _dbContext.Clients.Include(p => p.City).FirstOrDefault(p => p.ID == id);

			return View(client);
		}

		public IActionResult Create() => View();

		[HttpPost]
		public IActionResult Create(Client model) {
			model.CityID = 1;
			_dbContext.Clients.Add(model);
			_dbContext.SaveChanges();

			return RedirectToAction(nameof(Index));
		}
	}
}
