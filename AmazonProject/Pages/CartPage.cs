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
    internal class CartPage : Base
    {
        private By goToCartBtn = By.XPath("//a[contains(text(), 'Go to Cart')]");
        private By addedCartItem = By.XPath("/html[1]/body[1]/div[1]/div[1]/div[3]/div[4]/div[1]/div[2]/div[1]/div[1]/form[1]/div[2]/div[3]/div[4]/div[1]/div[2]/ul[1]/li[1]/span[1]/a[1]/span[1]/span[1]/span[2]");
        private By addedCartItemAmount = By.XPath("//span[@id='sc-subtotal-amount-activecart']");
        
        public CartPage(IWebDriver driver) : base(driver)
        {
           
        }

        public void NavigateToCart()

        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));


            // Wait for the element to be clickable
            IWebElement addCartElement = wait.Until(ExpectedConditions.ElementToBeClickable(goToCartBtn));

            // Click on the element using JavaScript
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", addCartElement);


        }

        public void ValidateCartItemAndAmount()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
                wait.Until(ExpectedConditions.ElementIsVisible(addedCartItem));
                IWebElement addedCartElement = Driver.FindElement(addedCartItem);
                var addedCartText = addedCartElement.Text;

                Assert.IsTrue(addedCartText.Contains("AX1800"), "Keyword not found in added item name");
                IWebElement addedCartItemAmountElement = Driver.FindElement(addedCartItemAmount);
                string addedCartItemAmountText = addedCartItemAmountElement.Text;


                Assert.IsTrue(addedCartItemAmountText.Contains("$73.95"), "Expected amount not found in added item amount");
            }
            catch (NoSuchElementException)
            {
                // Handle element not found gracefully
                Console.WriteLine("Added cart item not found.");
                Assert.Fail("Added cart item not found.");
            }

         
        }
    }
}
