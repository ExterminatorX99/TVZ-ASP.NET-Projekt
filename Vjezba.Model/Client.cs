using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vjezba.Model
{
	public class Client
	{
		[Key]
		public int ID { get; set; }

		[Display(Name = "Ime")]
		public string FirstName { get; set; }

		[Display(Name = "Prezime")]
		public string LastName { get; set; }

		public string Email { get; set; }

		[Display(Name = "Spol")]
		public char Gender { get; set; }

		[Display(Name = "Adresa")]
		public string Address { get; set; }

		[Display(Name = "Broj telefona")]
		public string PhoneNumber { get; set; }

		[ForeignKey(nameof(City))]
		public int? CityID { get; set; }

		public City City { get; set; }

		public string FullName => $"{FirstName} {LastName}";

		public virtual ICollection<Meeting> Meetings { get; set; }
	}
}
