using System;
using System.Linq;

namespace Vjezba.Model
{
	public class Student : Osoba
	{
		private string jmbag;

		public Student(string ime, string prezime, string jmbg, string oib, string jmbag, decimal prosjek, int brPolozeno, int ects) : base(ime, prezime, jmbg, oib) {
			JMBAG = jmbag;
			Prosjek = prosjek;
			BrPolozeno = brPolozeno;
			ECTS = ects;
		}

		public Student() {
		}

		public string JMBAG {
			get => jmbag;
			set {
				if (value.Length != 10 || !value.All(char.IsDigit))
					throw new InvalidOperationException();
				jmbag = value;
			}
		}

		public decimal Prosjek { get; set; }

		public int BrPolozeno { get; set; }

		public int ECTS { get; set; }
	}
}
