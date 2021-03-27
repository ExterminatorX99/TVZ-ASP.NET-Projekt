using System;
using System.Collections.Generic;
using System.Linq;

namespace Vjezba.Model
{
	public class Fakultet
	{
		public Fakultet(List<Osoba> osobe) {
			Osobe = osobe;
		}

		public Fakultet() {
			Osobe = new List<Osoba>();
		}

		public List<Osoba> Osobe { get; set; }

		public int KolikoStudenata() => Osobe.OfType<Student>().Count();

		public Student DohvatiStudenta(string jmbag) => Osobe.OfType<Student>().SingleOrDefault(s => s.JMBAG == jmbag);

		public IEnumerable<Student> DohvatiStudente91() => Osobe.OfType<Student>().Where(student => student.DatumRodjenja.Year > 1991);

		public IEnumerable<Student> DohvatiStudente91NoLinq() {
			var s = new List<Student>();
			foreach (Osoba osoba in Osobe)
				if (osoba is Student student && student.DatumRodjenja.Year > 1991)
					s.Add(student);
			return s;
		}

		public List<Student> DohvatiStudente91List() => Osobe.OfType<Student>().Where(student => student.DatumRodjenja.Year > 1991).ToList();

		public IEnumerable<Student> StudentiNeTvzD() => Osobe.OfType<Student>().Where(s => s.JMBAG.Substring(0, 4) != "0246" && s.Prezime[0] == 'D');

		public Student NajboljiProsjek(int god) => Osobe.OfType<Student>().Where(s => s.DatumRodjenja.Year == god).OrderByDescending(s => s.Prosjek).FirstOrDefault();

		public IEnumerable<Student> StudentiGodinaOrdered(int god) => Osobe.OfType<Student>().Where(s => s.DatumRodjenja.Year == god).OrderByDescending(s => s.Prosjek);

		public int KolikoProfesora() => Osobe.OfType<Profesor>().Count();

		public IEnumerable<Profesor> SviProfesori(bool asc) {
			if (asc)
				return Osobe.OfType<Profesor>().OrderBy(p => p.Prezime).ThenBy(p => p.Ime);
			return Osobe.OfType<Profesor>().OrderByDescending(p => p.Prezime).ThenByDescending(p => p.Ime);
		}

		public int KolikoProfesoraUZvanju(Zvanje zvanje) => Osobe.OfType<Profesor>().Count(p => p.Zvanje == zvanje);

		public IEnumerable<Profesor> DohvatiProfesore() => Osobe.OfType<Profesor>().OrderBy(profesor => profesor.DatumIzbora);

		public IEnumerable<Profesor> NeaktivniProfesori(int x) => Osobe.OfType<Profesor>().Where(p => (p.Zvanje == Zvanje.Predavac || p.Zvanje == Zvanje.VisiPredavac) && p.Predmeti.Count < x);

		public IEnumerable<Profesor> AktivniAsistenti(int x, int minEcts) => Osobe.OfType<Profesor>().Where(p => p.Zvanje == Zvanje.Asistent && p.Predmeti.Count(p => p.ECTS >= minEcts) > x);

		public void IzmjeniProfesore(Action<Profesor> action) {
			foreach (Profesor p in Osobe.OfType<Profesor>()) {
				action(p);
			}
		}
	}
}
