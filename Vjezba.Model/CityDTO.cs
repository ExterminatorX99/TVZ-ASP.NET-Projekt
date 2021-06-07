namespace Vjezba.Model
{
	public class CityDTO
	{
		public int ID { get; set; }

		public string Name { get; set; }

		public static implicit operator CityDTO(City city) {
			CityDTO dto = null;
			if (city != null)
				dto = new CityDTO {
					ID = city.ID,
					Name = city.Name
				};
			return dto;
		}
	}
}
