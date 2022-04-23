using FluentAssertions;
using HelperLibrary.Models;
using HelperLibrary.StringFormatHelpers;
using HelperLibrary.VaultHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using Xunit;

namespace WebXTests
{
    public class ShortMoodleTests
    {
        [Fact]
        public void VerifyPageTitle()
        {
            //Arrange
            GetSut();
            using IWebDriver _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            //Act
            _driver.Navigate().GoToUrl("https://moodle.vilniustech.lt/login/index.php");
            //Assert
            _driver.Title.Should().Be("VILNIUSTECH MOODLE: Log in to the site");
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
