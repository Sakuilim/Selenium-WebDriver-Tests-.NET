using FluentAssertions;
using HelperLibrary.Models;
using HelperLibrary.Shared;
using HelperLibrary.StringFormatHelpers;
using HelperLibrary.VaultHelpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using Xunit;
using Xunit.Priority;

namespace WebXTests
{
    public class MoodleFileTests
    {

        [Theory, Priority(1)]
        [InlineData("test.txt")]
        [InlineData("testpic.jpg")]
        [InlineData("testexcel.xlsx")]
        [InlineData("pdftest.pdf")]
        public void MoodleFileUploadTest(string filename)
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
            _driver.FindElement(By.LinkText("Manage private files...")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("a[title='Add...']")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='px-3'] input:nth-of-type(1)")).SendKeys(ItemPathStrings.TestsPath + filename);
            _driver.FindElement(By.CssSelector("div[class='mdl-align'] button:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='fp-filename-field'] div:nth-of-type(1)")).Click();
            IWebElement body = _driver.FindElement(By.TagName("body"));
            //Assert
            body.Should().NotBeNull();
            body.Text.Should().Contain(filename);
            body.Text.Should().Contain("Marius Milius");
            body.Text.Should().Contain($"Edit {filename}");
            _driver.Title.Should().Be("Dashboard");
            _driver.Quit();
        }

        [Theory, Priority(2)]
        [InlineData("test.txt")]
        [InlineData("testpic.jpg")]
        [InlineData("testexcel.xlsx")]
        [InlineData("pdftest.pdf")]
        public void MoodleFileUploadAndDeleteTest(string filename)
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
            _driver.FindElement(By.LinkText("Manage private files...")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("a[title='Add...']")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='px-3'] input:nth-of-type(1)")).SendKeys($"C:/Users/Marius Milius/Desktop/TestingItems/{filename}");
            _driver.FindElement(By.CssSelector("div[class='mdl-align'] button:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='modal-footer'] button:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.LinkText("Manage private files...")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='fp-filename-field'] div:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='form-group mx-0'] button:nth-of-type(2)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='filemanager fp-dlg'] button:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='modal-footer'] button:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            IWebElement body = _driver.FindElement(By.TagName("body"));
            //Assert
            body.Should().NotBeNull();
            body.Text.Should().NotContain(filename);
            _driver.Title.Should().Be("Dashboard");
            _driver.Quit();
        }
        [Fact,Priority(3)]
        public void MoodleUrlDownloaderTests()
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
            _driver.FindElement(By.LinkText("Manage private files...")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("a[title='Add...']")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='fp-repo nav-item odd active'] a:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='fp-login-input form-group'] input:nth-of-type(1)")).SendKeys("https://wp-assets.airbrake.io/wp-content/uploads/2015/03/16225626/7.png");
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("p[class='mdl-align'] button:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='fp-filename-field'] p:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='fp-select-buttons'] button:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='modal-footer'] button:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.LinkText("Manage private files...")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='fp-filename-field'] div:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='form-group mx-0'] button:nth-of-type(2)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='filemanager fp-dlg'] button:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            _driver.FindElement(By.CssSelector("div[class='modal-footer'] button:nth-of-type(1)")).Click();
            Thread.Sleep(1500);
            IWebElement body = _driver.FindElement(By.TagName("body"));
            //Assert
            body.Should().NotBeNull();
            body.Text.Should().NotContain("7.png");
            _driver.Title.Should().Be("Dashboard");
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