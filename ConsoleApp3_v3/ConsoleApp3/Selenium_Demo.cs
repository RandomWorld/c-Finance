using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using OpenQA.Selenium.Edge;

namespace Selenium_Demo
{
    class Selenium_Demo
    {
        // String test_url = "https://www.google.com";
        //String test_url = "https://www.blackrock.com/es/profesionales/productos/lista-de-producto#!type=mutualFunds&style=All&view=perfDiscrete";

        //string test_url = "https://www.schroders.com/es/es/inversores-particulares/fondos/gfc/fund/schdr_f00000p9hs/schroder-international-selection-fund-bric-brazil-russia-india-china-a-distribution-eur-av/lu0858243842/profile/";
        String test_url = "https://bbvaassetmanagement.com/es/fondos/?ES0114341037/BBVA-BONOS-DOLAR-CORTO-PLAZO,-FI";
        
        IWebDriver driver;

        [SetUp]
        public void start_Browser()
        {
            // Local Selenium WebDriver
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void test_search()
        {
            driver.Url = test_url;

            System.Threading.Thread.Sleep(28000);

            //IWebElement searchText = driver.FindElement(By.CssSelector("[name = 'q']"));

         //   IWebElement searchFund = driver.FindElement(By.XPath("//*[contains(concat(' ', @class,' '), concat(' ', 'ng - valid',' ' ))]"));


            //IWebElement searchFund = driver.FindElement(By.XPath("//*[@id='root']/div/div/amid-layout/div/div/div/div/div[1]/div/fx-profile-hero-panel/div/div[2]/div/div/div[2]/div/div/div/p/span")) ;
            
            //IWebElement dsadsa = driver.FindElement(By.Id("number monetary"));
            IWebElement dsadsa = driver.FindElement(By.CssSelector("body"));


            //searchText.SendKeys("LambdaTest");
            //searchFund.SendKeys("Asian Tiger");

            //IWebElement searchButton = driver.FindElement(By.XPath("//*[contains(concat( ' ', @class, ' ' ), concat( ' ', 'gNO89b', ' ' ))]"));

            //searchButton.Click();

            System.Threading.Thread.Sleep(6000);

            Console.WriteLine("Test Passed");
        }

        [TearDown]
        public void close_Browser()
        {
            driver.Quit();
        }
    }


}
namespace EdgeDriverTests
    {
        public class Program
        {
            /*
            * This assumes you have added MicrosoftWebDriver.exe to your System Path.
            * For help on adding an exe to your System Path, please see:
            * https://msdn.microsoft.com/en-us/library/office/ee537574(v=office.14).aspx
            */
            static void tt(string[] args)
            {
                /* You can find the latest version of Microsoft WebDriver here:
                * https://developer.microsoft.com/en-us/microsoft-edge/tools/webdriver/
                */
                var driver = new EdgeDriver();

                // Navigate to Bing
                driver.Url = "https://www.bing.com/";

                // Find the search box and query for webdriver
          /*      var element = driver.FindElementById("sb_form_q");

                element.SendKeys("webdriver");
                element.SendKeys(Keys.Enter);
          */
                Console.ReadLine();
                driver.Quit();
            }
        }
    }
