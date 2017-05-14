//using System;
//using NUnit.Framework;
//using AventStack.ExtentReports;

//namespace TenBet.helpers
//{
//    extern alias ble;
//    public class Reporter
//    {
//        public void LogInfo(string info)
//        {
//            string formatedInfo = String.Format("{0} INFO: {1}", DateTime.Now, info);
//            TestContext.Out.WriteLine(formatedInfo);
//            var extentTest = (ExtentTest)TestContext.CurrentContext.Test.Properties.Get("htmlLogger");
//            extentTest.Log(Status.Info, info);
//        }
//        public void LogError(string error)
//        {
//            string formatedInfo = String.Format("{0} INFO: {1}", DateTime.Now, error);
//            TestContext.Out.WriteLine(formatedInfo);
//            var extentTest = (ExtentTest)TestContext.CurrentContext.Test.Properties.Get("htmlLogger");
//            extentTest.Log(Status.Error, error);
//        }
//        public void Log(Status logstatus, string status)
//        {
//            var extentTest = (ExtentTest)TestContext.CurrentContext.Test.Properties.Get("htmlLogger");
//            extentTest.Log(logstatus, status);
//        }


//    }
//}
