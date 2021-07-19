using System;
using System.Collections.Generic;
using System.Text;

namespace DocSpy.CountryFunction
{
    public class PolygonDto
    {
        public double Length { get; }
 
        public double Area { get; }

        public LinearRingDto linearRing { get; set; }
    }
}
