
using System;
using RestSharp;
using RestSharp.Authenticators;

namespace BaseSolution.Helpers.WebServices.RestSharpHelper
{
    internal class RestSharpSimple
    {
        public IRestResponse GetRestSharpResponse()
        {
            var client = new RestSharp.RestClient();
            client.BaseUrl = new Uri("http://twitter.com");
            client.Authenticator = new HttpBasicAuthenticator("shayansadullah2004@yahoo.co.uk", "0L1v301l");
            var request = new RestRequest();
            request.Resource = "/notifications";
            
            var foo = client.Execute(request);
            return foo;
        }   
    }
}
