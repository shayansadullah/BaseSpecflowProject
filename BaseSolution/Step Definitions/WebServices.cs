
using TechTalk.SpecFlow;

namespace BaseSolution.Step_Definitions
{
    [Binding]
    public class WebServices
    {
        // For additional details on SpecFlow step definitions see http://go.specflow.org/doc-stepdef

        
        [Given(@"I enter an End Point")]
        public void GivenIEnterAnEndPoint()
        {
            var client = new Helpers.WebServices.RestClient();
            client.EndPoint = @"http://jsonplaceholder.typicode.com";
            client.Method = Helpers.WebServices.HttpVerb.GET;
            var json = client.MakeRequest("/posts/1");
        }

        [Given(@"I enter a specific request")]
        public void GivenIEnterAspecificRequest()
        {
            var foo = new Helpers.WebServices.RestSharpHelper.RestSharpSimple2();
            var bar = foo.GetRestSharpResponse();

        }
    }
}
