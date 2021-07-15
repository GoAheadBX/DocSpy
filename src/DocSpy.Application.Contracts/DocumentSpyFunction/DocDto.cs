using DocSpy.Documents;
using System;
using Volo.Abp.Application.Dtos;

namespace DocSpy.DocumentSpyFunction
{
    public class DocDto: AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public DateTimeOffset LastModified { get; set; }

        public OperateType OType { get; set; }
    }
}