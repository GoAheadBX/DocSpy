using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DocSpy.CountryFunction
{
    public class CountryDto : AuditedEntityDto<Guid>
    {
        public string CountryName { get; set; }
        public MultiPolygonDto Border { get; set; }
    }
}
