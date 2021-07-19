using AutoMapper;
using DocSpy.Countries;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace DocSpy.CountryFunction
{
    public class CountryService : CrudAppService<Country,CountryDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCountryDto>,
        ICountryService
    {
        public CountryService(IRepository<Country, Guid> repository) : base(repository)
        {
        }
        
        public void PutCountryBorder(string Name, CreateUpdateLinearRingDto points)
        {
            IEnumerable<Country> SelectedCountry =
                from country in Repository
                where country.CountryName == Name
                select country;

            if (SelectedCountry == null)
            {
                throw new NotImplementedException();
            }

            Coordinate[] CoorPoint = new Coordinate[points.coordinate.Length];

            for (int i =0; i < points.coordinate.Length; i++)
            {
                CoorPoint[i] = ObjectMapper.Map<CreateUpdateCoordinateDto, Coordinate>(points.coordinate[i]);
            }

            LinearRing lineara = new LinearRing(CoorPoint);

            Polygon polygon = new Polygon(lineara);
            Polygon[] polygons = { polygon };

            MultiPolygon multiPolygon = new MultiPolygon(polygons) { SRID = 4326 };
            var TheCountry = SelectedCountry.ToList();
            TheCountry[0].Border = multiPolygon ;
        }

        public double GetCountryArea(string Name)
        {

            IEnumerable<Country> SelectedCountry =
                from country in Repository
                where country.CountryName == Name
                select country;

            if (SelectedCountry == null)
            {
                throw new NotImplementedException();
            }
            var TheCountry = SelectedCountry.ToList();
            return TheCountry[0].Border.Area;
        }
    }
}
