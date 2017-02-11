using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TenBet
{
    public class BetrallyElements
    {
       public string editStake, placeBetButton, betSlipStatus;
        public BetrallyElements()
        {
            editStake = "stake_0";
            placeBetButton = "PlaceBetButton";
            betSlipStatus = ".betSlip-purchases";
        }

        [FindsBy(How = How.Id, Using = "NewHeaderUsername")]
        public IWebElement login = null;
        [FindsBy(How = How.Id, Using = "NewHeaderPassword")]
        public IWebElement password = null;
        [FindsBy(How = How.Id, Using = "NewHeaderLoginButton")]
        public IWebElement buttonLogin = null;
        [FindsBy(How = How.Id, Using = "headerSportsBalanceDrop")]
        public IWebElement headerSportsBalanceDrop;
        [FindsBy(How = How.CssSelector, Using = "#_me_highlights_1>td.bet_name>a:nth-child(1)>span.num_right")]
        public IWebElement firstHighlightOnMain;
        [FindsBy(How = How.Id, Using = "stake_0")]
        public IWebElement editBoxStake;
        [FindsBy(How = How.Id, Using = "PlaceBetButton")]
        public IWebElement placeBetButtonElement;
        [FindsBy(How = How.CssSelector, Using = ".betSlip-purchases")]
        public IWebElement betSlipStatusElement;

    }
}
