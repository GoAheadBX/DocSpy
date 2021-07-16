using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace DocSpy.Cities
{
    public class CityDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<City, Guid> _cityRepository;

        public CityDataSeedContributor(IRepository<City, Guid> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _cityRepository.GetCountAsync() <= 0)
            {
                await _cityRepository.InsertAsync(
                    new City
                    {
                        Name = "Beijing",
                        Location= new Point(122.333056, 47.609722, 12) { SRID = 4326 }
            },
                    autoSave: true
                );

                await _cityRepository.InsertAsync(
                    new City
                    {
                        Name = "Shanghai",
                        Location = new Point(122.333056, 23.609722, 13) { SRID = 4326 }
            },
                    autoSave: true
                );
            }
        }
    }
}
