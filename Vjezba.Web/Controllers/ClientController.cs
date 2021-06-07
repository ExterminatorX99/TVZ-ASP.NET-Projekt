using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

		public IActionResult Index() {
			IQueryable<Client> clientQuery = _dbContext.Clients.Include(p => p.City);

			List<Client> model = clientQuery.ToList();
			return View("Index", model);
		}

		public IActionResult IndexAjax(ClientFilterModel filter) {
			IQueryable<Client> clientQuery = _dbContext.Clients.Include(p => p.City);

			filter ??= new ClientFilterModel();

			if (!string.IsNullOrWhiteSpace(filter.FullName))
				clientQuery = clientQuery.Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(filter.FullName.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.Address))
				clientQuery = clientQuery.Where(p => p.Address.ToLower().Contains(filter.Address.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.Email))
				clientQuery = clientQuery.Where(p => p.Email.ToLower().Contains(filter.Email.ToLower()));

			if (!string.IsNullOrWhiteSpace(filter.City))
				clientQuery = clientQuery.Where(p => p.CityID != null && p.City.Name.ToLower().Contains(filter.City.ToLower()));

			List<Client> model = clientQuery.ToList();

			return PartialView("_IndexTable", model);
		}

		public IActionResult Details(int? id = null) {
			Client? client = _dbContext.Clients.Include(p => p.City).FirstOrDefault(p => p.ID == id);

			return View(client);
		}

		public IActionResult Create() {
			FillDropdownValues();
			return View();
		}

		[HttpPost]
		public IActionResult Create(Client model) {
			if (ModelState.IsValid) {
				_dbContext.Clients.Add(model);
				_dbContext.SaveChanges();

				return RedirectToAction(nameof(Index));
			}
			FillDropdownValues();
			return View();
		}

		[ActionName(nameof(Edit))]
		public IActionResult Edit(int id) {
			Client? model = _dbContext.Clients.FirstOrDefault(c => c.ID == id);
			FillDropdownValues();
			return View(model);
		}

		[HttpPost]
		[ActionName(nameof(Edit))]
		public async Task<IActionResult> EditPost(int id) {
			Client? client = _dbContext.Clients.FirstOrDefault(c => c.ID == id);
			bool ok = await TryUpdateModelAsync(client);

			if (ok && ModelState.IsValid) {
				await _dbContext.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			FillDropdownValues();
			return View();
		}

		private void FillDropdownValues() {
			var selectItems = new List<SelectListItem>();

			//Polje je opcionalno
			var listItem = new SelectListItem {
				Text = "- odaberite -",
				Value = ""
			};
			selectItems.Add(listItem);

			foreach (City category in _dbContext.Cities) {
				listItem = new SelectListItem(category.Name, category.ID.ToString());
				selectItems.Add(listItem);
			}

			ViewBag.PossibleCities = selectItems;
		}
	}
}
