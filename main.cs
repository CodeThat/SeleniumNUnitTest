using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.ObjectModel;
using System.IO;
using NLog;

namespace SeleniumCsharp
{
    public class Tests
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger(); // Initialize logger
        IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            try
            {
                // Log the test initialization
                logger.Info("Test Setup: Initializing WebDriver and setting up the browser.");
                
                // Get the drivers folder path dynamically
                string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
                logger.Info($"Driver Path: {path}");

                // Creates the ChromeDriver object
                driver = new ChromeDriver(path + @"\drivers\");
                logger.Info("ChromeDriver initialized successfully.");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred during WebDriver setup.");
                throw;  // Re-throw exception to make the test fail if the setup fails
            }
        }

        [Test]
        public void verifyLogo()
        {
            try
            {
                logger.Info("Starting verifyLogo test.");
                driver.Navigate().GoToUrl("https://brigadoontechnology.com/");
                logger.Info("Navigated to Brigadoon Technology homepage.");

                // Verify that the logo is displayed
                IWebElement logo = driver.FindElement(By.XPath("//img[contains(@class, 'logo')]"));
                Assert.IsTrue(logo.Displayed, "Logo is not displayed.");
                logger.Info("Logo verified successfully.");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in verifyLogo test.");
                throw;  // Ensure the test fails if an error occurs
            }
        }

        [Test]
        public void verifyMenuItemCount()
        {
            try
            {
                logger.Info("Starting verifyMenuItemCount test.");
                driver.Navigate().GoToUrl("https://brigadoontechnology.com/");
                logger.Info("Navigated to Brigadoon Technology homepage.");

                // Check the number of items in the main navigation menu
                ReadOnlyCollection<IWebElement> menuItems = driver.FindElements(By.XPath("//nav//ul/li"));
                logger.Info($"Menu items found: {menuItems.Count}");

                Assert.AreEqual(5, menuItems.Count, "The menu item count is incorrect.");
                logger.Info("Menu item count verified successfully.");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in verifyMenuItemCount test.");
                throw;
            }
        }

        [Test]
        public void verifyHomepageText()
        {
            try
            {
                logger.Info("Starting verifyHomepageText test.");
                driver.Navigate().GoToUrl("https://brigadoontechnology.com/");
                logger.Info("Navigated to Brigadoon Technology homepage.");

                // Verify a specific text on the homepage (can be a tagline or section heading)
                IWebElement homePageHeader = driver.FindElement(By.XPath("//h1[contains(text(), 'Innovation for Tomorrow')]"));
                logger.Info($"Homepage Header Text: {homePageHeader.Text}");

                Assert.IsTrue(homePageHeader.Text.Contains("Innovation for Tomorrow"), 
                    "Homepage header does not contain the expected text.");
                logger.Info("Homepage header text verified successfully.");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred in verifyHomepageText test.");
                throw;
            }
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            try
            {
                logger.Info("Test Teardown: Quitting WebDriver.");
                driver.Quit();
                logger.Info("WebDriver quit successfully.");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error occurred during WebDriver teardown.");
                throw;
            }
        }
    }
}
