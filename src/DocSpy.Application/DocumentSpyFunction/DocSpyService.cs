using DocSpy.DocumentSpyFunction;
using DocSpy.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DocSpy.DocumentSpyFunction
{
    public class DocSpyService: CrudAppService<Document, DocDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateDocDto>,
        IDocSpyService
    {
        public DocSpyService(IRepository<Document, Guid> repository) : base(repository)
        {
        }

        [HttpGet("{Name}")]
        public async Task<List<DocDto>> SqlQuery(string Name)
        {
            IEnumerable<Document> SelectedDocument =
                from document in Repository
                where document.Name == Name
                select document;

            if (SelectedDocument == null)
            {
                throw new NotImplementedException();
            }

            var entityDto = await MapToGetListOutputDtosAsync(SelectedDocument.ToList());
            return entityDto;
        }

        public async Task<List<DocDto>> GetDocByModifiedtimeAsync(DateTime start,DateTime end)
        {
            var entities = ReadOnlyRepository.Where(doc => doc.LastModified <= end && doc.LastModified > start);
            
            if (entities == null)
            {
                throw new NotImplementedException();
            }

            var entityDto = await MapToGetListOutputDtosAsync(entities.ToList());
            return entityDto;
        }


        /*        public async Task<List<DocDto>> CreatemanyAsync(IEnumerable<CreateUpdateDocDto> input)
       {
           await CheckCreatePolicyAsync();
           List<Document> eneities = new List<Document>();

           foreach (CreateUpdateDocDto singleinput in input)
           {
               var entity = await MapToEntityAsync(singleinput);
               TryToSetTenantId(entity);
               eneities.Add(entity);

           }
           await Repository.InsertManyAsync(eneities, autoSave: true);
           return await MapToGetListOutputDtosAsync(eneities); 
       }*/

    }
}
