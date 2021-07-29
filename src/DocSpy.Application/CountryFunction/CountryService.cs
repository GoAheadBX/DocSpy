using AutoMapper;
using DocSpy.Countries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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

        public virtual async Task<CountryDto> GetByNameAsync(string name)
        {
            await CheckGetPolicyAsync();

            var entity = await Repository.GetAsync(x => x.CountryName.ToLower().Equals(name.ToLower()));

            return await MapToGetOutputDtoAsync(entity);
        }

        public void PutCountryBorder(string Name, CreateUpdateLinearRingDto points)
        {
            // 可以采用另一种方法，见 PutCountryBorderByJsonFileAsync 
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

        public async Task<List<CountryDto>> PostCountryBorderByJsonFileAsync(IFormFile jsonFile)
        {

            JObject JsonObject;

            using (var stream = jsonFile.OpenReadStream())
            {
                StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                JsonObject = JObject.Parse(streamReader.ReadToEnd());
                streamReader.Dispose();
            }

            //string JsonFile 当输入JsonFile的为文件路径时
            //StreamReader streamReader = new StreamReader(jsonFile, Encoding.UTF8);

            JToken jfts = JsonObject["features"];
            List<JToken> jlst = jfts.ToList();
            List<Country> listCountries = new List<Country>();

            for (int i = 0; i < jlst.Count; i++)
            {
                JToken gprop = jlst[i]["properties"];
                JProperty JName = (JProperty)gprop.ElementAt(1);

                //筛选国家，当国家名不重复，两种方法等价
                /*IEnumerable<Country> SelectedCountry =
                from country in Repository
                where country.CountryName == JName.Value.ToString()
                select country;*/

                // 忽略大小写
                Country selectedCountry = await Repository.FindAsync(x => x.CountryName.ToLower().Equals(JName.Value.ToString().ToLower()));
                if (selectedCountry == null)
                {
                    selectedCountry = new Country();
                    selectedCountry.CountryName = JName.Value.ToString();
                    SetIdForGuids(selectedCountry);
                    await Repository.InsertAsync(selectedCountry, true);
                }

                listCountries.Add(selectedCountry);

                JToken geoinf = jlst[i]["geometry"];
                JProperty Jcoordinates = (JProperty)geoinf.ElementAt(1);

                var Polygons = JsonConvert.DeserializeObject<List<List<List<List<double>>>>>(Jcoordinates.Value.ToString());                
                var PloygonList = new List<Polygon>();

                foreach (var PolygonList in Polygons)
                {                    
                    foreach (var coordinates in PolygonList)
                    {
                        var Coordinates = new List<Coordinate>();
                        foreach (var coordinate in coordinates)
                        {
                            var oneCoordinate = new Coordinate(coordinate[0], coordinate[1]);
                            Coordinates.Add(oneCoordinate);
                        }
                        LinearRing OneLinearRing = new LinearRing(Coordinates.ToArray());
                        Polygon OnePloygon = new Polygon(OneLinearRing);
                        PloygonList.Add(OnePloygon);
                    }
                }

                MultiPolygon MultiPolygon = new MultiPolygon(PloygonList.ToArray());
                try
                {
                    selectedCountry.Border = MultiPolygon;
                    await Repository.UpdateAsync(selectedCountry);
                }
                catch
                {
                    throw new NotImplementedException();
                }
            }
            List<CountryDto> listCountriesDto = await MapToGetListOutputDtosAsync(listCountries);
            return listCountriesDto;
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
