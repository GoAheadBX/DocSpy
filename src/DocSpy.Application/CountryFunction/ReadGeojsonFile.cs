using AutoMapper.Configuration;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DocSpy.CountryFunction
{
class ReadGeojsonFile 
    {
        string jsonfile = "D://API//DocSpy//shpFile//mac.geojson"; //D:\API\DocSpy\shpFile\mac.geojson
        protected IServiceScopeFactory ServiceScopeFactory { get; set; }
        public void ReadGeojson(IServiceScopeFactory serviceScopeFactory)
        {
            ServiceScopeFactory = serviceScopeFactory;

            StreamReader streamReader = new StreamReader(jsonfile, Encoding.UTF8);
            JObject JsonObject = JObject.Parse(streamReader.ReadToEnd());
            
            WriteintoSql(JsonObject);       
        }
        private async void WriteintoSql(JObject JsonObject)
        {
            JToken jfts = JsonObject["features"];
            List<JToken> jlst = jfts.ToList();
            for (int i = 0; i < jlst.Count; i++)
            {
                using (var scope = ServiceScopeFactory.CreateScope())
                {
                    var fileService = scope.ServiceProvider.GetRequiredService<ICountryService>();

                    JToken gprop = jlst[i]["NAME_0"];
                    
                    JProperty JName = (JProperty)gprop.ElementAt(0);

                    MultiPolygon multiPolygon = jlst[i]["coordinates"].ToObject<MultiPolygon>();

                    
                    await fileService.CreateAsync(new CreateUpdateCountryDto
                    {
                        CountryName = JName.Value.ToString(),                        
                        //Border = multiPolygon
                    });
                }
            }
            
        }
    }
}
