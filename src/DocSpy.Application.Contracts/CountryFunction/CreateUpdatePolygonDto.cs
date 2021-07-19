using System;
using System.Collections.Generic;
using System.Text;

namespace DocSpy.CountryFunction
{
    public class CreateUpdatePolygonDto
    {
        public double Length { get; }

        public double Area { get; }

        public CreateUpdateLinearRingDto linearRing { get; set; }
    }
}
