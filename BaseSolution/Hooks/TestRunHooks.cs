namespace BaseSolution.Hooks
{
    using TechTalk.SpecFlow;
    using Helpers;

    [Binding]
    public class TestRunHooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
             Helpers.HookHelper.KillBrowsers();
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
