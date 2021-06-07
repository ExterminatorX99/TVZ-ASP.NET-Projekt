namespace Vjezba.Model
{
	public class ClientDTO
	{
		public int ID { get; set; }

		public string FullName { get; set; }

		public string Address { get; set; }

		public CityDTO City { get; set; }

		public string Email { get; set; }

		public static implicit operator ClientDTO(Client client) {
			return new ClientDTO {
				ID = client.ID,
				FullName = client.FullName,
				Address = client.Address,
				City = client.City,
				Email = client.Email
			};
		}
	}
}
