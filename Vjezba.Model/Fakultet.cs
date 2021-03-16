using System.Collections.Generic;
using System.Linq;

namespace Vjezba.Model
{
	public class Fakultet
	{
		public Fakultet(IList<Profesor> profesori, IList<Student> studenti) {
			Profesori = profesori;
			Studenti = studenti;
		}

		public IList<Profesor> Profesori { get; set; }

		public IList<Student> Studenti { get; set; }

		public int KolikoStudenata() => Studenti.Count;

		public Student DohvatiStudenta(string jmbag) => Studenti.SingleOrDefault(s => s.JMBAG == jmbag);

		public IEnumerable<Student> DohvatiStudente91() => Studenti.Where(student => student.DatumRodjenja().Year > 1991);

		public IEnumerable<Student> DohvatiStudente91NoLinq() {
			var s = new List<Student>();
			foreach (Student student in Studenti)
				if (student.DatumRodjenja().Year > 1991)
					s.Add(student);
			return s;
		}

		public List<Student> DohvatiStudente91List() => Studenti.Where(student => student.DatumRodjenja().Year > 1991).ToList();

		public IEnumerable<Student> StudentiNeTvzD() => Studenti.Where(s => s.JMBAG.Substring(0, 4) == "0246" && s.Prezime[0] == 'D');

		public Student NajboljiProsjek(int god) => Studenti.Where(s => s.DatumRodjenja().Year == god).OrderByDescending(s => s.Prosjek).FirstOrDefault();

		public IEnumerable<Student> StudentiGodinaOrdered(int god) => Studenti.Where(s => s.DatumRodjenja().Year == god).OrderByDescending(s => s.Prosjek);

		public int KolikoProfesora() => Profesori.Count;

		public IEnumerable<Profesor> SviProfesori(bool asc) {
			if (asc)
				return Profesori.OrderBy(p => p.Prezime).ThenBy(p => p.Ime);
			return Profesori.OrderByDescending(p => p.Prezime).ThenByDescending(p => p.Ime);
		}

		public int KolikoProfesoraUZvanju(Zvanje zvanje) => Profesori.Count(p => p.Zvanje == zvanje);

		public IEnumerable<Profesor> DohvatiProfesore() => Profesori.OrderBy(profesor => profesor.DatumIzbora);

		public IEnumerable<Profesor> NeaktivniProfesori(int x) => Profesori.Where(p => (p.Zvanje == Zvanje.Predavac || p.Zvanje == Zvanje.VisiPredavac) && p.Predmeti.Count < x);
	}
}
