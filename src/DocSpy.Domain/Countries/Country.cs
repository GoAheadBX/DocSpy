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

        //[Column(TypeName = "MultiPolygon")]
        //public Geometry Border { get; set; }
        public MultiPolygon Border { get; set; }
        //public List<Polygon> Borders { get; set; }

    }
}
