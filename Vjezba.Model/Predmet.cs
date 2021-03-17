namespace Vjezba.Model
{
	public class Predmet
	{
		public Predmet(int sifra, int ects, string naziv) {
			Sifra = sifra;
			ECTS = ects;
			Naziv = naziv;
		}

		public Predmet() { }

		public int Sifra { get; set; }

		public int ECTS { get; set; }

		public string Naziv { get; set; }
	}
}
