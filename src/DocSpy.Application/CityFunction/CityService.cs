using DocSpy.Cities;
using DocSpy.CityFuntion;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace DocSpy.CityFunction
{
    public class CityService : CrudAppService<City, CityDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCityDto>,
        ICityService
    {
        public CityService(IRepository<City, Guid> repository) : base(repository)
        {
        }

        public void PutCityFourDemsion(string Name, double m)
        {
            IEnumerable<City> SelectedCity =
                from city in Repository
                where city.Name == Name
                select city;

            if (SelectedCity == null)
            {
                throw new NotImplementedException();
            }

            var entity = SelectedCity.ToList();
            entity[0].Location.M = m;
        }
        public CityDto GetNearestCity(CreateUpdatePointDto currentLocation)
        {  
            var nearestCity = Repository.OrderBy(c => c.Location.Distance(ObjectMapper
                .Map<CreateUpdatePointDto, Point>(currentLocation))).FirstOrDefault();
            
            if (nearestCity == null)
            {
                throw new NotImplementedException();
            }

            var NearestCity = MapToGetOutputDto(nearestCity);
            return NearestCity;
        }

        public async Task<List<CityDto>> GetCityLocation(string Name)
        {

            IEnumerable<City> SelectedCity =
                from city in Repository
                where city.Name == Name
                select city;

            if (SelectedCity == null)
            {
                throw new NotImplementedException();
            }

            var entityDto = await MapToGetListOutputDtosAsync(SelectedCity.ToList());
            return entityDto;
        }


    }
}
