using DocSpy.CityFuntion;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace DocSpy.CityFunction
{
    public class CityDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        [Column(TypeName = "geography")]
        public PointDto Location { get; set; }
    }
}
