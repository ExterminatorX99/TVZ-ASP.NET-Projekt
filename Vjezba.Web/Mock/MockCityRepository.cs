using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Vjezba.Web.Mock
{
	public class MockCityRepository
	{
		private static List<City> _cache;

		private static MockCityRepository _instance;
		private string _xmlPath;

		public static MockCityRepository Instance => _instance ?? (_instance = new MockCityRepository());

		private MockCityRepository() {
			// Ne mijenjati.
		}

		public void Initialize(string xmlFolderPath) {
			_xmlPath = Path.Combine(xmlFolderPath, "cities.xml");
		}

		public IQueryable<City> All() {
			if (_cache != null)
				return _cache.AsQueryable();

			XDocument xDoc = XDocument.Load(_xmlPath);

			IQueryable<City> allNodes = xDoc.Root.Descendants("city").Select(p => new City {
				ID = int.Parse(p.Descendants("id").First().Value),
				Name = p.Descendants("name").First().Value
			}).AsQueryable();

			_cache = allNodes.ToList();

			return allNodes;
		}

		public City FindByID(int? cityId) {
			return All().FirstOrDefault(p => p.ID == cityId);
		}

		public bool InsertOrUpdate(City entity) =>
			//This is just mock repository
			true;

		public bool Delete(int cityId) =>
			//This is just mock repository
			true;
	}
}
