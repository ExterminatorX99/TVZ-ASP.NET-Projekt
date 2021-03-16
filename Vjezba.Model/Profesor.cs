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
			int doIzbora = Zvanje switch {
				Zvanje.Asistent => DatumIzbora.Year + 4 - DateTime.Now.Year,
				Zvanje.Predavac => DatumIzbora.Year + 5 - DateTime.Now.Year,
				Zvanje.VisiPredavac => DatumIzbora.Year + 5 - DateTime.Now.Year,
				Zvanje.ProfVisokeSkole => DatumIzbora.Year + 5 - DateTime.Now.Year,
				_ => throw new ArgumentOutOfRangeException()
			};

			return doIzbora;
		}
	}
}
