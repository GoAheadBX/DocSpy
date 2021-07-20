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
            Polygon polygon;
            List<Polygon> polygons = new List<Polygon>();

            int j = 0;
            for (int i =0; i < points.coordinate.Length; i++)
            {
                CoorPoint[i] = ObjectMapper.Map<CreateUpdateCoordinateDto, Coordinate>(points.coordinate[i]);

                if (CoorPoint[i].X == CoorPoint[j].X && CoorPoint[i].Y == CoorPoint[j].Y && i != j)
                {
                    Coordinate[] coorpoint = new Coordinate[i - j + 1];
                    Array.ConstrainedCopy(CoorPoint, j, coorpoint, 0, i - j + 1);
                    polygon = new Polygon(new LinearRing(coorpoint));
                    polygons.Add(polygon);
                    j = i + 1;
                }
            }

            try
            {
                MultiPolygon multiPolygon = new MultiPolygon(polygons.ToArray()) { SRID = 4326 };
                var TheCountry = SelectedCountry.ToList();
                TheCountry[0].Border = multiPolygon;
            }
            catch
            {
                throw new NotImplementedException();
            }
            
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
