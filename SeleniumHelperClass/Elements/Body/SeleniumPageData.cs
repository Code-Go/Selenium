using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SeleniumHelperClass.SeleniumConstants;

namespace SeleniumHelperClass
{
    public partial class AccessInformation : BaseClass, IDisposable
    {
        

        /// <summary>
        /// Finding the element
        /// </summary>
        /// <param name="parameter">search type</param>
        /// <param name="findElement">search by type</param>
        /// <param name="elementPosition">Position of the element</param>
        public IWebElement GetElement(string parameter, FindElement findElement,int elementPosition = 0)
        {
            try
            {
                IList<IWebElement> element = null;
                switch (findElement)
                {
                    case FindElement.ById:
                        {
                            wait.Until(d => d.FindElement(By.Id(parameter)));
                            element = driver.FindElements(By.Id(parameter));
                            break;
                        }
                    case FindElement.ByClassName:
                        {
                            wait.Until(d => d.FindElement(By.ClassName(parameter)));
                            element = driver.FindElements(By.ClassName(parameter));
                            break;
                        }
                    case FindElement.ByTagName:
                        {
                            wait.Until(d => d.FindElement(By.TagName(parameter)));
                            element = driver.FindElements(By.TagName(parameter));
                            break;
                        }
                    case FindElement.ByCssSelector:
                        {
                            wait.Until(d => d.FindElement(By.CssSelector(parameter)));
                            element = driver.FindElements(By.CssSelector(parameter));
                            break;
                        }
                    case FindElement.ByLinkText:
                        {
                            wait.Until(d => d.FindElement(By.LinkText(parameter)));
                            element = driver.FindElements(By.LinkText(parameter));
                            break;
                        }
                    case FindElement.ByName:
                        {
                            wait.Until(d => d.FindElement(By.Name(parameter)));
                            element = driver.FindElements(By.Name(parameter));
                            break;
                        }
                    case FindElement.ByPartialLinkText:
                        {
                            wait.Until(d => d.FindElement(By.PartialLinkText(parameter)));
                            element = driver.FindElements(By.PartialLinkText(parameter));
                            break;
                        }
                }
                //Returning the one element
                if (element != null && elementPosition > 0)
                {
                    return element.FirstOrDefault(); 
                }
                else
                {
                    return element.Skip(elementPosition).Take(1).FirstOrDefault();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }

        /// <summary>
        /// Set text to the element
        /// </summary>
        /// <param name="parameter">control parameter</param>
        /// <param name="text">text need to set</param>
        /// <param name="findElement">find control from parameter</param>
        /// <param name="elementPosition">position of the control</param>
        public void SetText(string parameter,string text, FindElement findElement, int elementPosition = 0)
        {
            try
            {
                IWebElement element = GetElement(parameter, findElement, elementPosition);
                if(element!=null)
                {
                    element.SendKeys(text);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Clicking the control
        /// </summary>
        /// <param name="parameter"></param>        
        /// <param name="findElement"></param>
        /// <param name="elementPosition"></param>
        public void Click(string parameter, FindElement findElement, int elementPosition = 0)
        {
            try
            {
                IWebElement element = GetElement(parameter, findElement, elementPosition);
                if (element != null)
                {
                    element.Click();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Select drop down value changing
        /// </summary>
        /// <param name="parameter">control parameter</param>
        /// <param name="text">text need to set</param>
        /// <param name="findElement">find control from parameter</param>
        /// <param name="selectBy">Select option</param>
        /// <param name="elementPosition">position of the control</param>
        public void Select(string parameter,string text, FindElement findElement,SelectBy selectBy, int elementPosition = 0)
        {
            try
            {
                IWebElement element = GetElement(parameter, findElement, elementPosition);
                if(element!=null)
                {
                    var selectElement = new SelectElement(element);
                    switch(selectBy)
                    {
                        case SelectBy.Text:
                            {
                                selectElement.SelectByText(text);
                                break;
                            }
                        case SelectBy.Value:
                            {
                                selectElement.SelectByValue(text);
                                break;
                            }
                        case SelectBy.Index:
                            {
                                selectElement.SelectByIndex(Convert.ToInt32(text));
                                break;
                            }
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string GetText(string parameter, FindElement findElement, int elementPosition = 0)
        {
            try
            {
                IWebElement element = GetElement(parameter, findElement, elementPosition);
                return element.Text;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
