using System.ComponentModel.DataAnnotations;

namespace Vjezba.Web.Mock
{
	public class City
	{
		[Key]
		public int ID { get; set; }

		public string Name { get; set; }
	}
}
