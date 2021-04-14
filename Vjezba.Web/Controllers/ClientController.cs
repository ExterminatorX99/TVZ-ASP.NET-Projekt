using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vjezba.Web.Mock;
using Vjezba.Web.Models;

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

		public IActionResult Details(int? id = null) {
			Client client = default;
			if(id != null)
				client = MockClientRepository.Instance.FindByID((int)id);

			return View(client);
		}
	}
}
