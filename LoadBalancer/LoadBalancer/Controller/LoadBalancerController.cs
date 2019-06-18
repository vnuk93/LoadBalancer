using Grapevine.Interfaces.Server;
using Grapevine.Server;
using Grapevine.Server.Attributes;
using Grapevine.Shared;
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
        MainCore _ = new MainCore("mongodb://51.83.73.69:27017", "LoadBalancer");
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
    }
}
