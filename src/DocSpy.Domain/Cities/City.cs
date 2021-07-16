using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DocSpy.Cities
{
    public class City: AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        [Column(TypeName = "geography")]
        public Point Location { get; set; }


    }
}
