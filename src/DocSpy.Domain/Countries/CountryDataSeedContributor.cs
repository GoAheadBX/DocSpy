using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;



namespace DocSpy.Countries
{
    public class CountryDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Country, Guid> _countryRepository;

        public CountryDataSeedContributor(IRepository<Country, Guid> cityRepository)
        {
            _countryRepository = cityRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _countryRepository.GetCountAsync() <= 0)
            {
                var countrya = new Country
                {
                    CountryName = "China"
                };
                Coordinate pointa = new Coordinate(0, 0);
                //pointa.Z = 4;
                Coordinate pointb = new Coordinate(0, 1);
                //pointb.Z = 4;
                Coordinate pointc = new Coordinate(1, 1);
                Coordinate pointd = new Coordinate(1, 0);
                Coordinate[] points = { pointa, pointb, pointc, pointd, pointa};
               

                LinearRing lineara = new LinearRing(points);

                Polygon polygon = new Polygon(lineara);
                Polygon[] polygons = { polygon};

                MultiPolygon multiPolygon = new MultiPolygon(polygons) { SRID = 4326 };

                countrya.Border = multiPolygon;

                await _countryRepository.InsertAsync(
                    countrya,
                    autoSave: true
                );                
            }
        }
    }
}
