using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vjezba.Model
{
	public class Client
	{
		[Key]
		public int ID { get; set; }

		[Display(Name = "Ime")]
		[Required]
		public string FirstName { get; set; }

		[Display(Name = "Prezime")]
		[Required]
		public string LastName { get; set; }

		[Required]
		[EmailAddress(ErrorMessage = "Mora biti validna email adresa.")]
		[MaxLength(256)]
		public string Email { get; set; }

		[Display(Name = "Spol")]
		[Required]
		public char Gender { get; set; }

		[Display(Name = "Adresa")]
		[Required]
		public string Address { get; set; }

		[Display(Name = "Broj telefona")]
		[Required(ErrorMessage = "Mora biti validan broj telefona.")]
		[Phone]
		public string PhoneNumber { get; set; }

		[ForeignKey(nameof(City))]
		[Required]
		public int? CityID { get; set; }

		public City City { get; set; }

		public string FullName => $"{FirstName} {LastName}";

		public virtual ICollection<Meeting> Meetings { get; set; }

		[DisplayName("Radno iskustvo")]
		[Required]
		[Range(0, 100, ErrorMessage = "Radno iskustvo mora biti između 0 i 100.")]
		public int? WorkingExperience { get; set; }
	}
}
