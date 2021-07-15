using DocSpy.Documents;
using System;

namespace DocSpy.DocumentSpyFunction
{
    public class CreateUpdateDocDto
    {
        public string Name { get; set; }

        public long Size { get; set; }

        public DateTimeOffset LastModified { get; set; }

        public OperateType OType { get; set; }
    }
}