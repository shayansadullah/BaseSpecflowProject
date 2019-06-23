namespace BaseSolution.Hooks
{
    using TechTalk.SpecFlow;

    [Binding]
    public class TestRunHooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
             TestRunContext.Initialise();
             TestRunContext.WindowSetup();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            TestRunContext.Driver.Quit();
            TestRunContext.Driver.Dispose();
        }
    }
}
