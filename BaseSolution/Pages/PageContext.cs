namespace BaseSolution.Pages
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Interactions;

    public class PageContext
    {
        public IWebDriver Driver { get; private set; }

        public Actions Actions { get; set; }

        public PageContext(IWebDriver driver)
        {
            this.Driver = driver;
        }
    }
}