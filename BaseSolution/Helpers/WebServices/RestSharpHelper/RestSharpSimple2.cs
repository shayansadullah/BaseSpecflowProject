
using RestSharp;
using RestSharp.Deserializers;
using System;

namespace BaseSolution.Helpers.WebServices.RestSharpHelper
{
    internal class RestSharpSimple2
    {
        public IRestResponse GetRestSharpResponse()
        {
            var client = new RestSharp.RestClient();
            client.ClearHandlers();
            client.BaseUrl = new Uri("http://carma.org");
            client.AddHandler("text/html", new JsonDeserializer());            
            var request = new RestRequest();
            request.Resource = "api/1.1/searchPlants";
            request.AddParameter("location", 5332921);
            request.AddParameter("limit", 10);  
            request.AddParameter("color", "red");
            //request.AddParameter("format", "json");
            var plants = client.Execute<PowerPlantsDTO>(request);
            return plants;
        }


    }
}
