using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DocSpy.Documents
{
    public class Document: AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
       
        public long Size { get; set; }

        public DateTimeOffset LastModified { get; set; }
        public OperateType OType { get; set; }
    }
}
