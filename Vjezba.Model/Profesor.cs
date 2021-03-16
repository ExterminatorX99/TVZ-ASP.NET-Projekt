using System;

namespace Vjezba.Model
{
	public class Profesor : Osoba
	{
		public Profesor(string ime, string prezime, string jmbg, string oib, string odjel, Zvanje zvanje, DateTime datumIzbora) : base(ime, prezime, jmbg, oib) {
			Odjel = odjel;
			Zvanje = zvanje;
			DatumIzbora = datumIzbora;
		}

		public string Odjel { get; set; }

		public Zvanje Zvanje { get; set; }

		public DateTime DatumIzbora { get; set; }

		public int KolikoDoReizbora() {
			int doIzbora;
			switch (Zvanje) {
				case Zvanje.Asistent:
					doIzbora = DatumIzbora.Year + 4 - DateTime.Now.Year;
					break;
				case Zvanje.Predavac:
					doIzbora = DatumIzbora.Year + 5 - DateTime.Now.Year;
					break;
				case Zvanje.VisiPredavac:
					doIzbora = DatumIzbora.Year + 5 - DateTime.Now.Year;
					break;
				case Zvanje.ProfVisokeSkole:
					doIzbora = DatumIzbora.Year + 5 - DateTime.Now.Year;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return doIzbora;
		}
	}
}
