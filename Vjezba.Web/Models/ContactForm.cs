namespace Vjezba.Web.Models
{
	public class ContactForm
	{
		private string _newsletter;
		public string Ime { get; set; }
		public string Prezime { get; set; }
		public string Email { get; set; }
		public string Poruka { get; set; }
		public string Tip { get; set; }
		public string Toggle { get; set; }

		public string Newsletter
		{
			get => _newsletter == "on" ? "obavijestit ćemo vas" : "nećemo vas obavijestiti";
			set => _newsletter = value;
		}
	}
}
