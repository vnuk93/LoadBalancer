using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Shared;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancer
{
    [RestResource]
    class LoadBalancerController
    {
        MainCore _ = new MainCore(Properties.Settings.Default.ip, "LoadBalancer");

        #region GET
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/api/loadbalancer/all")]
        public IHttpContext ReadAllContacto(IHttpContext context)
        {

            var data = _.ReadAll();

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse(json);
            return context;
        }

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/api/loadbalancer/one")]
        public IHttpContext ReadOneContacto(IHttpContext context)
        {

            var id = context.Request.QueryString["id"] ?? "what?"; //Si no id dara error
            var data = _.ReadId(id);

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse(json);
            return context;
        }
        #endregion

        #region POST

        [RestRoute(HttpMethod = HttpMethod.POST, PathInfo = "/api/loadbalancer/add")]
        public IHttpContext AddContacto(IHttpContext context)
        {

            string jsonRAW = context.Request.Payload;
            dynamic dataId = JsonConvert.DeserializeObject<object>(jsonRAW);

            MLoadBalancer data = new MLoadBalancer();

            data.typeService = dataId.typeService;
            data.url = dataId.url;
            data.status = dataId.status;

            _.Create(data);

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            context.Response.AppendHeader("Content-Type", "application/json");
            context.Response.SendResponse(json);
            return context;
        }

        #endregion

        #region PUT

        [RestRoute(HttpMethod = HttpMethod.PUT, PathInfo = "/api/loadbalancer/update")]
        public IHttpContext UpdateContacto(IHttpContext context)
        {

            string jsonRAW = context.Request.Payload;
            var id = context.Request.QueryString["id"] ?? "what?";

            dynamic dataId = JsonConvert.DeserializeObject<object>(jsonRAW);

            MLoadBalancer data = new MLoadBalancer();

            //data.fecha = DateTime.Now;
            data._id = ObjectId.Parse(id);



            _.Update(id, data);

            context.Response.SendResponse("Updated!");
            return context;

        }


        #endregion
    }
}
