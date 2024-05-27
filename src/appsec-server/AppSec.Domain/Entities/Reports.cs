using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSec.Domain.Entities
{
    public class Reports<T>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
       public string ProjectName { get; set; }
       public string ProjectId { get; set; }
       public DateTime DateAnalysis { get; set; }
       public string TypeAnalysis { get; set; }
       public string Guid { get; set; }
       public T Report { get; set;}                             
    }
}
