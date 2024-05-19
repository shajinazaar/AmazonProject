using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace AmazonProject.Pages
{
    public class SearchResultPage : Base
    {
        private By addCartBtn = By.XPath("//input[@id='add-to-cart-button']");
        private By SearchResultsItems = By.CssSelector("div[data-component-type='s-search-result']");
        private By itemElements = By.CssSelector("span.a-text-normal");
        private By nextPageBtn = By.XPath("//a[text()='Next']");
        private By noOffersMessage = By.XPath("/html[1]/body[1]/div[1]/div[1]/div[9]/div[5]/div[1]/div[5]/div[1]/div[1]/div[1]/span[1]");
        public SearchResultPage(IWebDriver driver) : base(driver)
        {
        }

        public void ClickOnFirstSearchResult()
        {

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.ElementExists(SearchResultsItems));

            var specificKeyword = "AX1800";

            var matchedItems = new List<string>();


            if (!IsKeywordPresentInCurrentPage(specificKeyword, matchedItems))
            {

                var nextPageButton = Driver.FindElement(nextPageBtn);
                if (nextPageButton != null)
                {
                    nextPageButton.Click();
                    wait.Until(ExpectedConditions.ElementExists(SearchResultsItems));

                    if (IsKeywordPresentInCurrentPage(specificKeyword, matchedItems))
                    {
                        Console.WriteLine("Keyword found on the second page.");
                    }
                    else
                    {
                        Console.WriteLine("Keyword not found on the first two pages.");
                    }
                }
            }

            Console.WriteLine("Matched Items:");
            foreach (var item in matchedItems)
            {
                Console.WriteLine(item);
            }
        }

        public bool IsKeywordPresentInCurrentPage(string keyword, List<string> matchedItems)

        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
            var items = Driver.FindElements(SearchResultsItems);
            foreach (var item in items)
            {
                var itemNameElement = item.FindElement(itemElements);

                var itemName = itemNameElement.Text;
                if (itemName.Contains(keyword))
                {
                    // Scroll to the element to ensure it's visible
                    ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", itemNameElement);
                    wait.Until(ExpectedConditions.ElementToBeClickable(itemNameElement)).Click();
                    try
                    {
                        WebDriverWait addCartWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                        IWebElement addCartElement = addCartWait.Until(ExpectedConditions.ElementToBeClickable(addCartBtn));
                        addCartElement.Click();
                    }
                    catch (WebDriverTimeoutException)
                    {
                        try
                        {
                            IWebElement noOfferMessageElement = wait.Until(ExpectedConditions.ElementIsVisible(noOffersMessage));
                            if (noOfferMessageElement.Text.Contains("No featured offers available"))
                            {
                                Console.WriteLine("No featured offers available.");
                            }
                            else
                            {
                                Console.WriteLine("Unable to locate the 'Add to Cart' button, 'Not Available', or 'No Offers' message.");
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            Console.WriteLine("Unable to locate the 'Add to Cart' button and 'No featured offers available' message.");
                        }
                    }

                    return true;
                }
            }
            return false;
        }


    }
}

