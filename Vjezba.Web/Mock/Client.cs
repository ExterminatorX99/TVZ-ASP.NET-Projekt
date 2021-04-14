using System.ComponentModel.DataAnnotations;

namespace Vjezba.Web.Mock
{
	public class Client
	{
		[Display(Name = "ID")]
		public int ID { get; set; }
		[Display(Name = "Ime")]
		public string FirstName { get; set; }
		[Display(Name = "Prezime")]
		public string LastName { get; set; }
		[Display(Name = "Email")]
		public string Email { get; set; }
		[Display(Name = "Spol")]
		public char Gender { get; set; }
		[Display(Name = "Adresa")]
		public string Address { get; set; }
		[Display(Name = "Tel.")]
		public string PhoneNumber { get; set; }

		public int? CityID { get; set; }
		public City City { get; set; }

		[Display(Name = "Ime i prezime")]
		public string FullName => $"{FirstName} {LastName}";

	}
}
