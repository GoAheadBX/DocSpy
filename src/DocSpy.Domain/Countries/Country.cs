using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace DocSpy.Countries
{
    public class Country : AuditedAggregateRoot<Guid>
    {
        public string CountryName { get; set; }

        //[Column(TypeName = "geography")]
        public Geometry Border { get; set; }

    }
}
