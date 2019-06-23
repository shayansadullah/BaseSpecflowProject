
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace BaseSolution.Step_Definitions
{
    using Helpers.WebServices.RestSharpHelper;

    [Binding]
    public class WebServices
    {
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef

        private RestsharpSimple3 restSharpSimple3;
        
        [StepDefinition(@"I enter the (.*) site")]
        public void IEnterTheSite(string address)
        {
            var restSharpSimple3 = new RestsharpSimple3();
            restSharpSimple3.GoToAddress(address);
            this.restSharpSimple3 = restSharpSimple3;
        }

        [StepDefinition(@"I enter the postcode (.*)")]
        public void IEnterThePostCode(string postCode)
        {
            this.restSharpSimple3.ExecuteRequest(postCode);
        }

        [StepDefinition(@"the response code is: (.*)")]
        public void ResponseCodeIs(int responseCode)
        {
            Assert.That(this.restSharpSimple3.GetResponse(), Is.EqualTo(responseCode));
        }

        [Given(@"I enter an End Point")]
        public void GivenIEnterAnEndPoint()
        {
            var client = new Helpers.WebServices.RestClient();
            client.EndPoint = @"http://api.zippopotam.us";
            client.Method = Helpers.WebServices.HttpVerb.GET;
            var json = client.MakeRequest("/90210");
        }

        [Given(@"I enter a specific request")]

        public void GivenIEnterAspecificRequest()
        {
            var foo = new RestSharpSimple2();
            var bar = foo.GetRestSharpResponse();
        }
    }
}
