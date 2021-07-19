using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DocSpy.CountryFunction
{
    public interface ICountryService : ICrudAppService<CountryDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCountryDto>
    {
    }
}
