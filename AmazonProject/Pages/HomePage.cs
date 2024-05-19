using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonProject.Pages
{
    public class HomePage : Base
    {
        private By searchInputLocator = By.Id("twotabsearchtextbox");
        //private By searchInputSuggestion = By.XPath("//div[contains(@class,'s-suggestion-container')]");
        private By DeliveryToBtn = By.CssSelector("#glow-ingress-block");
        private By DropDownBtn = By.XPath("//select[@id='GLUXCountryList']");
        private By DoneBtn = By.XPath("//button[@name='glowDoneButton']");
     //   private By countryName = By.XPath("//a[text()='Philippines']");
        

        public HomePage(IWebDriver driver) : base(driver)
        {
            
        }

        public void NavigateToHomePage()
        {
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl("https://www.amazon.com/");
            Thread.Sleep(TimeSpan.FromSeconds(15));
          //  Driver.Navigate().Refresh();
            
        }
        public void ChangeDeliveryLocation(string country)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
            IWebElement deliveryTo = wait.Until(ExpectedConditions.ElementToBeClickable(DeliveryToBtn));
            deliveryTo.Click();

            // Find the dropdown element and set the country
            IWebElement dropDown = wait.Until(ExpectedConditions.ElementToBeClickable(DropDownBtn));

            SelectElement select = new SelectElement(dropDown);

            // Select by value
            select.SelectByValue(country);


            // Click "Done"
            IWebElement doneBtn = wait.Until(ExpectedConditions.ElementToBeClickable(DoneBtn));
            doneBtn.Click();
        }

        public void SearchForItem(string searchTerm)
        {
            // Execute JavaScript to perform the search
            ((IJavaScriptExecutor)Driver).ExecuteScript($"window.location.href = 'https://www.amazon.com/s?k={searchTerm}';");
        }





    }
}
