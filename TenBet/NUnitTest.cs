using System;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using OpenQA.Selenium.Support.PageObjects;

namespace TenBet
{
    public class NUnitTest
    {
        public static IWebDriver driver;
        public MainPage mainPage = new MainPage();
        public BetrallyElements betrally = new BetrallyElements();
        public Configuration config = new Configuration();

        decimal stake;
        public OpenQA.Selenium.Interactions.Actions action;
        WebDriverWait wait10;
   /*     public NUnitTest()
        {
            driver = mainPage.driverInit();
            action = new OpenQA.Selenium.Interactions.Actions(driver);
            wait10 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
         //   PageFactory.InitElements(driver, betrally);
        }
     */   

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
            //     PageFactory.InitElements(driver, betrally);
            //     stake = decimal.Parse(MainPage.conf.items["Stake"]);
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
        [Ignore("is not a test, just template")]
        public void BugFixing()
        {
            stake = decimal.Parse(MainPage.conf.items["Stake"]);
            MainPage.Login(driver);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
          
            while (true)
            {

                mainPage.AddRandomLiveEvent(driver, wait10);
                mainPage.WaitForElementBy(driver, wait10,betrally.betslipBettingDetailsBy);
                if (mainPage.IsElementPresentBy(driver, betrally.betslipOverlayBy ))
                {
                     var elements = js.ExecuteScript("arguments[0].click();", betrally.betslipClose);

                }
                else
                {
                    break;
                }
            }
            

        }

[TearDown]
        public void EndTest()
        {
            driver.Quit();
        }
    }

}
