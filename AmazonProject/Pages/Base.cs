using OpenQA.Selenium;

namespace AmazonProject.Pages
{
    public class Base
    {
        protected IWebDriver Driver;

        public Base(IWebDriver driver)
        {
            Driver = driver;
        }
    }
}