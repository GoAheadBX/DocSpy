using NetTopologySuite.Geometries;
using System.Collections.Generic;

namespace DocSpy.CountryFunction
{
    public class MultiPolygonDto
    {
        //public PolygonDto polygon { get; set; }
        public PolygonDto[] Geometries { get; set; }

    }
}