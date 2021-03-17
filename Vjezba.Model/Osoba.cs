using System;
using System.Linq;

namespace Vjezba.Model
{
	public class Osoba
	{
		private string jmbg;
		private string oib;

		public Osoba(string ime, string prezime, string jmbg, string oib) {
			Ime = ime;
			Prezime = prezime;
			JMBG = jmbg;
			OIB = oib;
		}

		public Osoba() { }

		public string Ime { get; set; }

		public string Prezime { get; set; }

		public string OIB {
			get => oib;
			set {
				if (value.Length != 11 || !value.All(char.IsDigit))
					throw new InvalidOperationException();
				oib = value;
			}
		}

		public string JMBG {
			get => jmbg;
			set {
				if (value.Length != 13 || !value.All(char.IsDigit))
					throw new InvalidOperationException();
				jmbg = value;
			}
		}


		public DateTime DatumRodjenja {
			get {
				int dan = int.Parse(JMBG.Substring(0, 2));
				int mjesec = int.Parse(JMBG.Substring(2, 2));
				int godina = int.Parse(JMBG.Substring(4, 3));
				//DateTime.Today.Year
				if (godina + 100 > 1000)
					godina += 1000;
				else
					godina += 2000;

				return new DateTime(godina, mjesec, dan);
			}
		}
	}
}
