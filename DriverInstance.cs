using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    enum TagType { ByName = 1, ById, ByClass, ByXPath  };
    enum ElementType { Div = 1, TextBox };
    enum WebDriver { Firefox = 1, Chrome, IE, Apple }
    class DriverInstance
    {
        static IWebDriver webdriver;
        string url = "https://vast-dawn-73245.herokuapp.com";

        public DriverInstance(WebDriver driverType)
        {
            _WebDriver(driverType);
        }

        private void _WebDriver(WebDriver driverType)
        {
            if (webdriver == null)
            {
                switch (driverType)
                {
                    case WebDriver.Firefox:
                        webdriver = new FirefoxDriver();
                        break;
                    case WebDriver.Chrome:
                        webdriver = new ChromeDriver();
                        break;
                    case WebDriver.IE:
                        //webdriver = new IEDriver()
                        break;
                    case WebDriver.Apple:
                        break;
                    default:
                        break;
                }
                
            }
            _ConfigureDriver();
        }

        private void _ConfigureDriver()
        {
            webdriver.Url = url;
            webdriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        /// <summary>
        /// this method used to find a specific web element by its name and set a value
        /// </summary>
        /// <param name="ElementName">Name of the DOM element</param>
        /// <param name="value">Value to pass to the DOM element</param>
        public void SetElementValue(string ElementName, string value)
        {
            IWebElement elm = webdriver.FindElement(By.Name(ElementName));
            if (elm == null)
            {
                throw new NoSuchElementException();
            }
            else
            {
                elm.SendKeys(value);
            }
        }
        

        /// <summary>
        /// this method used to click any submit button available and to post the form data
        /// </summary>
        /// <param name="ButtonName">available submit button name</param>
        /// OR
        /// <param name="className">submit button class name</param>
        public void SubmitForm(string ButtonName, string className = null)
        {
            IWebElement elm;

            if (className != null)
            {
                elm = webdriver.FindElement(By.ClassName(className));
            }
            else
            {
                elm = webdriver.FindElement(By.ClassName(className));
            }

            if (elm != null)
            {
                elm.Submit();
            }
            else
            {
                throw new NoSuchElementException();
            }
        }

        
        public string ReadValue(TagType tagType, string elementIdentity, ElementType elementType )
        {
            IWebElement elm = null;
            switch (tagType)
            {
                case TagType.ByXPath:
                    elm = webdriver.FindElement(By.XPath(elementIdentity));
                    break;
                case TagType.ByName:
                    elm = webdriver.FindElement(By.Id(elementIdentity));
                    break;
            }

            if (elm != null)
            {
                switch (elementType)
                {
                    case ElementType.Div:
                        return elm.Text;
                    case ElementType.TextBox:
                        return elm.GetAttribute("value");
                    default:
                        break;
                }
            }
            else
                throw new NotFoundException();

            return string.Empty;
        }

        public void ClearTextBox(TagType tagType, string elementIdentity)
        {
            IWebElement elm = null;
            switch (tagType)
            {
                case TagType.ByName:
                    elm = webdriver.FindElement(By.Name(elementIdentity));

                    break;
                case TagType.ById:
                    break;
                case TagType.ByClass:
                    break;
                case TagType.ByXPath:
                    break;
                default:
                    break;
            }

            if (elm != null)
            {
                elm.SendKeys("");
            }
            else
            {
                throw new NoSuchElementException();
            }
        }
    }
}
