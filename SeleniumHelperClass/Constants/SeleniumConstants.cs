using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumHelperClass
{
    public class SeleniumConstants
    {
        public enum BrowserType
        {
            IE=0,
            Chrome =1,
            FireFox = 2
        }

        public enum FindElement
        {
            ByClassName = 0,
            ByCssSelector = 1,
            ById = 2,
            ByLinkText = 3,
            ByName = 4,
            ByPartialLinkText = 5,
            ByTagName = 6
        }

        public enum SelectBy
        {
            Text=0,
            Value=1,
            Index =2
        }
    }
}
