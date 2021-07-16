using DocSpy.CityFuntion;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DocSpy.CityFunction
{
    public class CreateUpdateCityDto
    {
        public string Name { get; set; }

        [Column(TypeName = "geography")]
        public CreateUpdatePointDto Location { get; set; }
    }
}
