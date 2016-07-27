
using System;
using RestSharp;
using RestSharp.Authenticators;

namespace BaseSolution.Helpers.WebServices.RestSharpHelper
{
    internal class RestSharpHelperAdvanced
    {
        readonly string _accountSid;
        readonly string _secretKey;

        public RestSharpHelperAdvanced(string accountSid, string secretKey)
        {
            _accountSid = accountSid;
            _secretKey = secretKey;
        }

        public Call GetCall(string callSid)
        {
            var request = new RestRequest();
            request.Resource = "Accounts/{AccountSid}/Calls/{CallSid}";
            request.RootElement = "Call";

            request.AddParameter("CallSid", callSid, ParameterType.UrlSegment);

            return Execute<Call>(request);
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestSharp.RestClient();
            client.BaseUrl = new Uri("https://api.twilio.com/2008-08-01");
            client.Authenticator = new HttpBasicAuthenticator(_accountSid, _secretKey);
            request.AddParameter("AccountSid", _accountSid, ParameterType.UrlSegment); // used on every request
            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var twilioException = new ApplicationException(message, response.ErrorException);
                throw twilioException;
            }
            return response.Data;
        }

    }
}
