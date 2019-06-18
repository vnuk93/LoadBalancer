using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
    [BsonIgnoreExtraElements]
    class MLoadBalancer
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonIgnoreIfNull]
        public string typeService { get; set; }
        [BsonIgnoreIfNull]
        public string url { get; set; }
        [BsonIgnoreIfNull]
        public int status { get; set; }
    }
}
