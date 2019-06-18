using Mongo.CRUD;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DMModel = LoadBalancer.MLoadBalancer;


namespace LoadBalancer
{
    class MainCore
    {
        public IMongoCRUD<DMModel> db;
        public MainCore(string server, string database)
        {
            db = new MongoCRUD<DMModel>(server, database);
        }

        #region CREATE

        public void Create(DMModel data)
        {
            db.Create(data);
        }

        #endregion

        #region READ
        public List<DMModel> ReadAll()
        {
            BsonDocument filter = new BsonDocument();
            filter.Add("_id", new BsonDocument()
                    .Add("$exists", new BsonBoolean(true))
            );

            var data = db.Search(filter).Documents.ToList();

            return data;

        }
          
        public DMModel ReadId(string id)
        {
            return db.Get(ObjectId.Parse(id));
        }

        // FALTA ES BUSCAR POR CAMPO? ESTA EN QUERY     
        //public List<DMModel> ReadValue(string fieldName, string fieldValue)
        //{


        //}
        #endregion

        #region UPDATE
        public void Update(string id, DMModel data)
        {
            DMModel document = new DMModel();

            document = db.Get(ObjectId.Parse(id));
            document = data;

            db.Update(data);

        }

        #endregion

    }
}
