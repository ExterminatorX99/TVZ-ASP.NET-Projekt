using System.ComponentModel.DataAnnotations;

namespace Vjezba.Web.Mock
{
	public class City
	{
		public int ID { get; set; }
		[Display(Name = "Grad")]
		public string Name { get; set; }
	}
}
