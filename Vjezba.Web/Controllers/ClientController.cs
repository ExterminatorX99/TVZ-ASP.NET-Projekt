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
		public IActionResult Index() {
			List<Client> clients = MockClientRepository.Instance.All().ToList();

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
