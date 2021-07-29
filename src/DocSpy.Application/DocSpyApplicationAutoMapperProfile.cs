using AutoMapper;
using DocSpy.DocumentSpyFunction;
using DocSpy.Documents;
using DocSpy.Cities;
using DocSpy.CityFunction;
using NetTopologySuite.Geometries;
using DocSpy.CityFuntion;
using DocSpy.CountryFunction;
using DocSpy.Countries;

namespace DocSpy
{
    public class DocSpyApplicationAutoMapperProfile : Profile
    {
        public DocSpyApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Document, DocDto>();
            CreateMap<CreateUpdateDocDto, Document>();
            CreateMap<City, CityDto>();
            CreateMap<CreateUpdateCityDto, City>();           
            CreateMap<Country, CountryDto>();
            CreateMap<CreateUpdateCountryDto, Country>();
            CreateMap<Point, PointDto>();
            CreateMap<CreateUpdatePointDto, Point>();
            CreateMap<MultiPolygon, MultiPolygonDto>();
            CreateMap<CreateUpdateMultiPolygonDto, MultiPolygon>();
            CreateMap<Polygon, PolygonDto>();
            CreateMap<CreateUpdatePolygonDto, Polygon>();
            CreateMap<Coordinate, CoordinateDto>();
            CreateMap<CreateUpdateCoordinateDto, Coordinate>();
        }
    }
}
