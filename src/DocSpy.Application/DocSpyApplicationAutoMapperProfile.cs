using AutoMapper;
using DocSpy.DocumentSpyFunction;
using DocSpy.Documents;
using DocSpy.Cities;
using DocSpy.CityFunction;
using NetTopologySuite.Geometries;
using DocSpy.CityFuntion;

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
            CreateMap<Point, PointDto>();
            CreateMap<CreateUpdatePointDto, Point>();

        }
    }
}
