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

		public int KolikoProfesora() {
			return Profesori.Count;
		}

		public int KolikoStudenata() {
			return Studenti.Count;
		}

		public Student DohvatiStudenta(string jmbag) {
			return Studenti.SingleOrDefault(s => s.JMBAG == jmbag);
		}

		public IEnumerable<Profesor> DohvatiProfesore() {
			return Profesori.OrderBy(profesor => profesor.DatumIzbora);
		}
	}
}
