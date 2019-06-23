using RestSharp;
using System;
using System.Net;

namespace BaseSolution.Helpers.WebServices.RestSharpHelper
{
    public class RestsharpSimple3
    {
        private RestSharp.RestClient _client;
        private RestRequest _request;
        public IRestResponse _response;

        //Arrange
        public void GoToAddress(string address)
        {
            var client = new RestSharp.RestClient();
            client.ClearHandlers();
            client.BaseUrl = new Uri(address);
            this._client = client;
        }

        //Act
        public void ExecuteRequest(string postCode)
        {
            RestRequest request = new RestRequest(string.Format("/{0}", postCode), Method.GET);
            this._request = request;
            IRestResponse response = this._client.Execute(this._request);
            this._response = response;
        }

        //Assert
        public int GetResponse()
        {
            return (int)this._response.StatusCode;
        }
    }
}
