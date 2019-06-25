using System;
using System.Collections.Generic;
using System.Text;

namespace SandBox.DataAccess.DBContext.SeedObjects
{
    // "datasetid": "temperature-quotidienne-regionale",
    //"recordid": "b6109da9ea5dc5f708a1fe2188f2505d06d4476e",
    //"fields": {
    //  "code_insee_region": "93",
    //  "tmin": 12.02,
    //  "tmoy": 17.23,
    //  "region": "Provence-Alpes-C\u00f4te d'Azur",
    //  "date": "2019-05-29",
    //  "tmax": 22.45
    //},
    //"record_timestamp": "2019-06-01T01:00:00+02:00"
    internal class TmaxForRegion

    {
        public string datasetid { get; set; }
        public string recordid { get; set; }
        public Fields fields { get; set; }
        public string record_timestamp { get; set; }
    }
   
}
