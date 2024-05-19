using AmazonProject.Pages;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AmazonProject.FeatureFiles.StepsDefinition
{
    [Binding]
    public class AmazonShoppingSteps
    {
        private IWebDriver driver;
        private HomePage homePage;
        private SearchResultPage searchResultPage;
        private CartPage cartPage;

        [BeforeScenario]
        public void BeforeScenario()
        {
            driver = new ChromeDriver();
            homePage = new HomePage(driver);
            searchResultPage = new SearchResultPage(driver);
            cartPage = new CartPage(driver);
        }

        [Given(@"I am on the Amazon homepage as an unregistered user")]
        public void GivenIAmOnTheAmazonHomepageAsAnUnregisteredUser()
        {
            homePage.NavigateToHomePage();
            homePage.ChangeDeliveryLocation("PH");
        }

        [When(@"I search for ""(.*)""")]
        public void WhenISearchFor(string searchTerm)
        {
            homePage.SearchForItem(searchTerm);
        }

        [When(@"I add the corresponding item to the cart")]
        public void WhenIAddTheCorrespondingItemToTheCart()
        {
            searchResultPage.ClickOnFirstSearchResult();
        }

        [When(@"I navigate to the cart")]
        public void WhenINavigateToTheCart()
        {
            cartPage.NavigateToCart();
        }

        [Then(@"I validate that the correct item and amount are displayed")]
        public void ThenIValidateThatTheCorrectItemAndAmountAreDisplayed()
        {
            cartPage.ValidateCartItemAndAmount();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
        }
    }
}
