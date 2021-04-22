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

		public IActionResult Index(string query = null) {
			IQueryable<Client> clientQuery = _dbContext.Clients.Include(c => c.City);

			if (!string.IsNullOrWhiteSpace(query))
				clientQuery = clientQuery.Where(p => p.FullName.ToLower().Contains(query));

			ViewBag.ActiveTab = 4;

			return View(clientQuery.ToList());
		}

		[HttpPost]
		public ActionResult Index(string queryName, string queryAddress) {
			IQueryable<Client> clientQuery = _dbContext.Clients.Include(c => c.City);

			//Primjer iterativnog građenja upita - dodaje se "where clause" samo u slučaju da je parametar doista proslijeđen.
			//To rezultira optimalnijim stablom izraza koje se kvalitetnije potencijalno prevodi u SQL
			if (!string.IsNullOrWhiteSpace(queryName))
				clientQuery = clientQuery.Where(p => p.FullName.ToLower().Contains(queryName));

			if (!string.IsNullOrWhiteSpace(queryAddress))
				clientQuery = clientQuery.Where(p => p.Address.ToLower().Contains(queryAddress));

			ViewBag.ActiveTab = 2;

			List<Client> model = clientQuery.ToList();
			return View(model);
		}

		[HttpPost]
		public ActionResult AdvancedSearch(ClientFilterModel filter) {
			IQueryable<Client> clientQuery = _dbContext.Clients.Include(c => c.City);

			//Primjer iterativnog građenja upita - dodaje se "where clause" samo u slučaju da je parametar doista proslijeđen.
			//To rezultira optimalnijim stablom izraza koje se kvalitetnije potencijalno prevodi u SQL
			if (!string.IsNullOrWhiteSpace(filter.FullName))
				clientQuery = clientQuery.Where(p => p.FullName.ToLower().Contains(filter.FullName.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.Address))
				clientQuery = clientQuery.Where(p => p.Address.ToLower().Contains(filter.Address.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.Email))
				clientQuery = clientQuery.Where(p => p.Email.ToLower().Contains(filter.Email.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.City))
				clientQuery = clientQuery.Where(p => p.City != null && p.City.Name.ToLower().Contains(filter.City.ToLower()));

			ViewBag.ActiveTab = 3;

			List<Client> model = clientQuery.ToList();
			return View("Index", model);
		}

		public IActionResult Details(int? id = null) {
			Client model = id != null ? _dbContext.Clients.Include(c => c.City).FirstOrDefault(c => c.ID == id) : null;
			return View(model);
		}

		public IActionResult Create() => View();

		[HttpPost]
		public IActionResult Create(Client client) {
			//client.ID = _dbContext.Clients.Max(c => c.ID) + 1;
			//client.City = _dbContext.Clients.Select(c => c.City).FirstOrDefault(c => c != null && c.Name.Contains(client.City.Name, StringComparison.OrdinalIgnoreCase));
			//client.CityID = client.City?.ID;
			client.CityID = 1;

			_dbContext.Clients.Add(client);
			_dbContext.SaveChanges();
			return View();
		}
	}
}
