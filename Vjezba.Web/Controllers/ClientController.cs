using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vjezba.Web.Mock;

namespace Vjezba.Web.Controllers
{
	public class ClientController : Controller
	{
		public IActionResult Index(string query) {
			List<Client> clients = MockClientRepository.Instance.All().ToList();

			if (!string.IsNullOrWhiteSpace(query))
				clients = clients.Where(c => c.FullName.ToLower().Contains(query.ToLower())).ToList();

			return View(clients);
		}

		[HttpPost]
		public IActionResult Index(string queryName, string queryAddress) {
			List<Client> clients = MockClientRepository.Instance.All().ToList();

			if (!string.IsNullOrWhiteSpace(queryName))
				clients = clients.Where(c => c.FullName.ToLower().Contains(queryName.ToLower())).ToList();
			if (!string.IsNullOrWhiteSpace(queryAddress))
				clients = clients.Where(c => c.Address.ToLower().Contains(queryAddress.ToLower())).ToList();

			return View(clients);
		}

		public IActionResult Details(int? id = null) {
			Client client = default;
			if (id != null)
				client = MockClientRepository.Instance.FindByID((int)id);

			return View(client);
		}
	}
}
