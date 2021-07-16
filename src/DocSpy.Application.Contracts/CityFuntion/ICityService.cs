using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DocSpy.CityFunction
{
    public interface ICityService : ICrudAppService<CityDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCityDto>
    {
    }
}
