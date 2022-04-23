using FluentAssertions;
using HelperLibrary.Models;
using HelperLibrary.StringFormatHelpers;
using HelperLibrary.VaultHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebXTests.Shared;
using Xunit;

namespace WebXTests
{
    public class FileExportTests
    {


        [Fact]
        public void CalendarExportTests()
        {
            //Arrange
            GetSut();
            using IWebDriver _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://moodle.vilniustech.lt/login/index.php");
            _driver.Manage().Window.Maximize();
            _driver.FindElement(By.Id("username")).SendKeys(GetLoginData().Username);
            _driver.FindElement(By.Id("password")).SendKeys(GetLoginData().Password);
            _driver.FindElement(By.Id("loginbtn")).Click();
            //Act
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("span[class='current'] a:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("form[action='https://moodle.vilniustech.lt/calendar/export.php'] button:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='d-flex flex-wrap align-items-center'] label:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("input[id='id_period_timeperiod_weeknow'")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("span[data-fieldtype='submit'] input[id='id_export']")).Click();
            Thread.Sleep(1500);
            IWebElement body = _driver.FindElement(By.TagName("body"));
            //Assert
            body.Should().NotBeNull();
            DownloadHelper.CheckFileDownloaded("icalexport").Should().BeTrue();
            _driver.Title.Should().Be("MOODLE: Calendar: Export");
            _driver.Url.Should().Be("https://moodle.vilniustech.lt/calendar/export.php?");
            _driver.Quit();
        }

        [Fact]
        public void CalendarEventCreation()
        {
            //Arrange
            GetSut();
            using IWebDriver _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("https://moodle.vilniustech.lt/login/index.php");
            _driver.Manage().Window.Maximize();
            _driver.FindElement(By.Id("username")).SendKeys(GetLoginData().Username);
            _driver.FindElement(By.Id("password")).SendKeys(GetLoginData().Password);
            _driver.FindElement(By.Id("loginbtn")).Click();
            //Act
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("span[class='current'] a:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='header d-flex flex-wrap p-1'] button[data-action='new-event-button']")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='col-md-9 form-inline align-items-start felement'] input:nth-of-type(1)")).SendKeys("test");
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='modal-footer'] button:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("span[class='calendar-circle calendar_event_user']")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='modal-footer'] button[data-action='delete']")).Click();
            Thread.Sleep(2500);
            _driver.FindElement(By.CssSelector("div[class='modal-footer'] button[data-action='save']:not([tabindex*='-1'")).Click();
            //Assert
            IWebElement body = _driver.FindElement(By.TagName("body"));
            body.Should().NotBeNull();
            _driver.Title.Should().Be($"MOODLE: Calendar: Detailed month view: {DateTime.Now.ToString("MMMM")} {DateTime.Now.Year}");
            _driver.Url.Should().Contain("https://moodle.vilniustech.lt/calendar/view.php");
            _driver.Quit();
        }

        private string GetSut()
        {
            return new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
        }

        private UserModel GetLoginData()
        {
            var parser = new FileParser();
            var vaultDataService = new VaultDataService(parser);
            var user = new UserModel();
            return vaultDataService.GetUserData(user);
        }
    }
}
