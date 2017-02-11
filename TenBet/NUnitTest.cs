using System;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

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
            mainPage.AddRandomHighlightEvent(driver, wait10);
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
            string s = "#new_live_betting #live_betting_br_1 .live_betting_table tr";
            //s = "#_me_highlights_" + rnd.Next(0, i) + ">td.bet_name>a:nth-child(" + rnd.Next(1, 2).ToString() + ")>span.num_right";
            var elements = driver.FindElements(By.CssSelector(s));

//            if (conf.items.ContainsKey("Login"))
        }

[TearDown]
        public void EndTest()
        {
            driver.Quit();
        }
    }

}
