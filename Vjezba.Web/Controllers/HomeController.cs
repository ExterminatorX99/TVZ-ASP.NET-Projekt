using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using Vjezba.Web.Models;

namespace Vjezba.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult FAQ(int? selected = null)
		{
			var questions = new List<(string q, string a, bool bold)>
			{
				("What is 2+2?", "4", false),
				("What is the answer to life?", "42", false),
				("Why didn’t the Eagles fly the Ring to Mount Doom?", "Shut up", false),
				("Why?", "Because", false),
				("Who?", "Kristijan Kos", false)
			};

			if (selected != null)
			{
				int s = (int) selected;
				(string q, string a, bool bold) q = questions[s - 1];
				q.bold = true;
				questions[s - 1] = q;
			}

			ViewBag.Questions = questions;

			return View();
		}

		public IActionResult Contact()
		{
			ViewBag.Message = "Jednostavan način proslijeđivanja poruke iz Controller -> View.";
			//Kao rezultat se pogled /Views/Home/Contact.cshtml renderira u "pravi" HTML
			//Primjetiti - View() je poziv funkcije koja uzima cshtml template i pretvara ga u HTML
			//Zasto bas Contact.cshtml? Jer se akcija zove Contact, te prema konvenciji se "po defaultu" uzima cshtml datoteka u folderu Views/CONTROLLER_NAME/AKCIJA.cshtml

			return View();
		}

		/// <summary>
		/// Ova akcija se poziva kada na formi za kontakt kliknemo "Submit"
		/// URL ove akcije je /Home/SubmitQuery, uz POST zahtjev isključivo - ne može se napraviti GET zahtjev zbog [HttpPost] parametra
		/// </summary>
		/// <param name="formData"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult SubmitQuery(IFormCollection formData) {
			string ime = formData["ime"];
			string prezime = formData["prezime"];
			string email = formData["email"]; ;
			string poruka = formData["poruka"]; ;
			string tip = formData["tip"]; ;
			string toggle = formData["newsletter"];
			string newsletter = toggle == "on" ? "obavijestit ćemo vas" : "nećemo vas obavijestiti";


			//Ovdje je potrebno obraditi podatke i pospremiti finalni string u ViewBag
			string message = $"Poštovani {ime} {prezime} ({email}) zaprimili smo vašu poruku te će vam se netko ubrzo javiti.<br>";
			message += $"Sadržaj vaše poruke je: [{tip}]<br>{poruka}.<br>";
			message += $"Također, {newsletter} o daljnjim promjenama preko newslettera.<br>";


			ViewBag.Message = message;
			//Kao rezultat se pogled /Views/Home/ContactSuccess.cshtml renderira u "pravi" HTML
			//Kao parametar se predaje naziv cshtml datoteke koju treba obraditi (ne koristi se default vrijednost)
			//Trazenu cshtml datoteku je potrebno samostalno dodati u projekt
			return View("ContactSuccess");
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
