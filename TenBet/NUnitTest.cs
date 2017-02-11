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

        decimal stake;
        public OpenQA.Selenium.Interactions.Actions action;
        WebDriverWait wait10;
        public NUnitTest()
        {
            driver = mainPage.driverInit();
            action = new OpenQA.Selenium.Interactions.Actions(driver);
            wait10 = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }


        [SetUp]
        public void Initialize()
        {
            mainPage.Maximize(driver);
            mainPage.NavigateToURL(driver);
            mainPage.LoginPopUp(driver, wait10);
         }
        [Test]
        public void PlaceHighlightBetAndCheckBalance()
        {
             stake = decimal.Parse(MainPage.conf.items["Stake"]);
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
            stake = decimal.Parse(MainPage.conf.items["Stake"]);
            MainPage.Login(driver);
            decimal sportBalance = mainPage.GetBalance(driver, action, wait10);
            while (true)
            {
                mainPage.AddRandomLiveEvent(driver, wait10);

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
        public void BugFixing()
        {
            string s;
            PageFactory.InitElements(driver, betrally);
            stake = decimal.Parse(MainPage.conf.items["Stake"]);
            MainPage.Login(driver);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
          
            while (true)
            {

                mainPage.AddRandomLiveEvent(driver, wait10);

                var sd = betrally.betslipClose;
                //        [FindsBy(How = How.CssSelector, Using = "#bet-slip-container > li[id^=sce] >img.close")]
        ///                 public IWebElement betslipClose;

       // var k = driver.FindElement(betrally.betslipCloseBy);
                if (mainPage.IsElementPresentElement(driver, betrally.betslipOverlayBy ))
                {
                   var elements = js.ExecuteScript("arguments[0].click();", sd);
                }
                else
                {
                    break;
                }
            }
            ;

        }

[TearDown]
        public void EndTest()
        {
            driver.Quit();
        }
    }

}
