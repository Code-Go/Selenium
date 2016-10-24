using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SeleniumHelperClass.SeleniumConstants;
using OpenQA.Selenium.Support.UI;

namespace SeleniumHelperClass
{
    partial class AccessInformation: BaseClass,IDisposable
    {        
        public void Dispose()
        {
            //if (driver != null)
                //driver.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">Open this site</param>
        /// <param name="browserType">Open on this browser</param>        
        public void OpenUrl(string url, BrowserType browserType)
        {            
            try
            {                
                switch (browserType)
                {
                    case BrowserType.IE:
                        {
                            //driver = new 
                            break;
                        }
                    case BrowserType.Chrome:
                        {                            
                            driver = new ChromeDriver("Resources/");
                            break;
                        }
                    case BrowserType.FireFox:
                        {
                            driver = new FirefoxDriver();
                            break;
                        }
                }
                driver.Url = url;
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
