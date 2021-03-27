using System;
using System.Linq;

namespace Vjezba.Model
{
	public class Student : Osoba
	{
		private string _JMBAG;

		public Student(string ime, string prezime, string jmbg, string oib, string _JMBAG, decimal prosjek, int brPolozeno, int ects) : base(ime, prezime, jmbg, oib) {
			JMBAG = _JMBAG;
			Prosjek = prosjek;
			BrPolozeno = brPolozeno;
			ECTS = ects;
		}

		public Student() {
		}

		public string JMBAG {
			get => _JMBAG;
			set {
				if (value.Length != 10 || !value.All(char.IsDigit))
					throw new InvalidOperationException();
				_JMBAG = value;
			}
		}

		public decimal Prosjek { get; set; }

		public int BrPolozeno { get; set; }

		public int ECTS { get; set; }
	}
}
