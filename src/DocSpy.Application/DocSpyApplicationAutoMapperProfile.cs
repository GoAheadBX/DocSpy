using AutoMapper;
using DocSpy.DocumentSpyFunction;
using DocSpy.Documents;

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
        }
    }
}
