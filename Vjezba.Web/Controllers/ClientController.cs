using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vjezba.Web.Mock;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
	public class ClientController : Controller
	{
		public IActionResult Index(string query) {
			IQueryable<Client> clients = MockClientRepository.Instance.All();

			if (!string.IsNullOrWhiteSpace(query))
				clients = clients.Where(c => c.FullName.ToLower().Contains(query.ToLower()));

			return View(clients.ToList());
		}

		[HttpPost]
		public IActionResult Index(string queryName, string queryAddress) {
			IQueryable<Client> clients = MockClientRepository.Instance.All();

			if (!string.IsNullOrWhiteSpace(queryName))
				clients = clients.Where(c => c.FullName.ToLower().Contains(queryName.ToLower()));
			if (!string.IsNullOrWhiteSpace(queryAddress))
				clients = clients.Where(c => c.Address.ToLower().Contains(queryAddress.ToLower()));

			return View(clients.ToList());
		}

		[HttpPost]
		public ActionResult AdvancedSearch(ClientFilterModel model)
		{
			IQueryable<Client> clients = MockClientRepository.Instance.All();

			if (!string.IsNullOrWhiteSpace(model.FullName))
				clients = clients.Where(c => c.FullName.ToLower().Contains(model.FullName.ToLower()));
			if (!string.IsNullOrWhiteSpace(model.Email))
				clients = clients.Where(c => c.Address.ToLower().Contains(model.Email.ToLower()));
			if (!string.IsNullOrWhiteSpace(model.Address))
				clients = clients.Where(c => c.Address.ToLower().Contains(model.Address.ToLower()));
			if (!string.IsNullOrWhiteSpace(model.CityName))
				clients = clients.Where(c => c.City != null && c.City.Name.ToLower().Contains(model.CityName.ToLower()));

			return View("Index", clients.ToList());
		}

		public IActionResult Details(int? id = null) {
			Client client = default;
			if (id != null)
				client = MockClientRepository.Instance.FindByID((int)id);

			return View(client);
		}
	}
}
