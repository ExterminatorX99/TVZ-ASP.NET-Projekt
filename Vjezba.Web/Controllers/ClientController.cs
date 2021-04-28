using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vjezba.DAL;
using Vjezba.Model;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
    public class ClientController : Controller
    {
        private ClientManagerDbContext _dbContext;

        public ClientController(ClientManagerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IActionResult Index(string query = null)
        {
            var clientQuery = this._dbContext.Clients.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
                clientQuery = clientQuery.Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(query));

            ViewBag.ActiveTab = 1;

            return View(clientQuery.ToList());
        }

        [HttpPost]
        public ActionResult Index(string queryName, string queryAddress)
        {
            var clientQuery = this._dbContext.Clients.AsQueryable();

            //Primjer iterativnog građenja upita - dodaje se "where clause" samo u slučaju da je parametar doista proslijeđen.
            //To rezultira optimalnijim stablom izraza koje se kvalitetnije potencijalno prevodi u SQL
            if (!string.IsNullOrWhiteSpace(queryName))
                clientQuery = clientQuery.Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(queryName));

            if (!string.IsNullOrWhiteSpace(queryAddress))
                clientQuery = clientQuery.Where(p => p.Address.ToLower().Contains(queryAddress));

            ViewBag.ActiveTab = 2;

            var model = clientQuery.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult AdvancedSearch(ClientFilterModel filter)
        {
            var clientQuery = this._dbContext.Clients.AsQueryable();

            //Primjer iterativnog građenja upita - dodaje se "where clause" samo u slučaju da je parametar doista proslijeđen.
            //To rezultira optimalnijim stablom izraza koje se kvalitetnije potencijalno prevodi u SQL
            if (!string.IsNullOrWhiteSpace(filter.FullName))
                clientQuery = clientQuery.Where(p => (p.FirstName + " " + p.LastName).ToLower().Contains(filter.FullName.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Address))
                clientQuery = clientQuery.Where(p => p.Address.ToLower().Contains(filter.Address.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Email))
                clientQuery = clientQuery.Where(p => p.Email.ToLower().Contains(filter.Email.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.City))
                clientQuery = clientQuery.Where(p => p.CityID != null && p.City.Name.ToLower().Contains(filter.City.ToLower()));

            ViewBag.ActiveTab = 3;

            var model = clientQuery.ToList();
            return View("Index", model);
        }

        public IActionResult Details(int? id = null)
        {
            var client = this._dbContext.Clients
                .Include(p => p.City)
                .Where(p => p.ID == id)
                .FirstOrDefault();

            return View(client);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Client model)
        {
            model.CityID = 1;
            this._dbContext.Clients.Add(model);
            this._dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
