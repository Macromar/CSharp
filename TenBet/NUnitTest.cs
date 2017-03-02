using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using System;
using System.IO;
using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;

namespace TenBet
{
    public class NUnitTest
    {

        public ExtentReports extent;
        public static IWebDriver driver;
        public MainPage mainPage = new MainPage();
        public BetrallyElements betrally = new BetrallyElements();
        public Configuration config = new Configuration();

        decimal stake;
        public OpenQA.Selenium.Interactions.Actions action;
        WebDriverWait wait10;

        public object OutputType { get; private set; }

        public NUnitTest()
        {
            extent = new ExtentReports("Results\\Extent.html", false, DisplayOrder.OldestFirst);
        }

        [SetUp]
        public void Initialize()
        {
            driver = mainPage.driverInit();
            action = new OpenQA.Selenium.Interactions.Actions(driver);
            wait10 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, betrally);
            mainPage.Maximize(driver);
            mainPage.NavigateToURL(driver);
            mainPage.LoginPopUp(driver, wait10);
         }
        [Test]
        public void PlaceHighlightBetAndCheckBalance()
        {
            stake = decimal.Parse(config.GetConfigValue("Stake"));
            MainPage.Login(driver);
            decimal sportBalance = mainPage.GetBalance(driver, action, wait10);
            mainPage.AddRandomHighlightEvent(driver, wait10);
            mainPage.PlaceStake(driver, wait10 , stake);
             string result;
            if (mainPage.WaitForStakeStatus(driver, wait10).Contains("accepted"))
            {
                if (mainPage.ChangedBalance(driver, wait10, action, sportBalance, stake) + stake == sportBalance)
                   result= "Pass. With accepted bet";
                else result= "Fail. With accepted bet";
            }
            else
            {
                if (mainPage.ChangedBalance(driver, wait10, action, sportBalance, stake)== sportBalance)
                    result = "Pass. With rejected bet";
                else result = "Fail. With rejected bet";
            }
            if (result.Contains("Pass"))
                Assert.Pass();
            else
                Assert.Fail();
        }
        [Test]
        public void PlaceLiveAndCheckBalance()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            stake = decimal.Parse(MainPage.conf.items["Stake"]);
            MainPage.Login(driver);
            decimal sportBalance = mainPage.GetBalance(driver, action, wait10);
            while (true)
            {

                mainPage.AddRandomLiveEvent(driver, wait10);
                mainPage.WaitForElementBy(driver, wait10, betrally.betslipBettingDetailsBy);
                if (mainPage.IsElementPresentBy(driver, betrally.betslipOverlayBy))
                {
                    var elements = js.ExecuteScript("arguments[0].click();", betrally.betslipClose);

                }
                else
                {
                    break;
                }
            }
            mainPage.PlaceStake(driver, wait10, stake);
            string result;
            if (mainPage.WaitForStakeStatus(driver, wait10).Contains("accepted"))
            {
                if (mainPage.ChangedBalance(driver, wait10, action, sportBalance, stake) + stake == sportBalance)
                    result = "Pass. With accepted bet";
                else result = "Fail. With accepted bet";
            }
            else
            {
                if (mainPage.ChangedBalance(driver, wait10, action, sportBalance, stake) == sportBalance)
                    result = "Pass. With rejected bet";
                else result = "Fail. With rejected bet";
            }
            if (result.Contains("Pass"))
                Assert.Pass();
            else
                Assert.Fail();
        }
        [Test]
  //      [Ignore("is not a test, just template")]
        public void BugFixing()
        {
            ITakesScreenshot shot = (ITakesScreenshot)driver;
            DateTime localDate = DateTime.Now;
            string img;
            string s="45";
            ExtentTest test = extent.StartTest("Start Test"); ;
            try
            {

                s = Regex.Replace(localDate.ToString(), "[\\/.,: ]", "");
                s = "images\\" + s + ".Jpeg";
                test.AssignCategory("Regression");
                test.Log(LogStatus.Info, test.GetTest().ToString());
                test.Log(LogStatus.Info, "Some Info");
                shot.GetScreenshot().SaveAsFile("Results\\" + s, ScreenshotImageFormat.Jpeg);
                img = test.AddScreenCapture(s);
                test.Log(LogStatus.Info, "image description" + img);
                //extent.EndTest(test);
                s = Regex.Replace(localDate.ToString(), "[\\/.,: ]", "");
                s = "images\\" + s + ".Jpeg";
                driver.FindElement(By.Id("fgdfg")).Click();

            }
            catch (Exception ex)
            {
                test.Log(LogStatus.Info, "Stack trace is:        " + Environment.StackTrace);
                test.Log(LogStatus.Info, ex.Message);

                if (TestContext.CurrentContext.Test.Name == "BugFixing")
                {
                }
                
                extent.EndTest(test);
                extent.Flush();
            }

        }

[TearDown]
        public void EndTest()
        {
            
            driver.Quit();
        }
    }

}
