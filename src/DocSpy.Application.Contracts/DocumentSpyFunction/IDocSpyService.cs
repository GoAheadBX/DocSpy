using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace DocSpy.DocumentSpyFunction
{
    public interface IDocSpyService : ICrudAppService<DocDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateDocDto>
    {
        //  Task<List<DocDto>> CreatemanyAsync(IEnumerable<CreateUpdateDocDto> enumerable);
        public Task<List<DocDto>> SqlQuery(string Name);
    }
}
