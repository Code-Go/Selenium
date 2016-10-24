using DirectProcess.EntityList;
using ScreenShotDemo;
using SeleniumHelperClass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SeleniumHelperClass.SeleniumConstants;

namespace DirectProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AccessInformation accessInformation = new AccessInformation())
            {
                string url = "http://prd.int.web.pharmerica.net/extranet";
                string username = "leo.fernandez";
                string password = "leofernandez";

                List<RxInformation> listRxInformation = new List<RxInformation>();

                RxInformation rxInformation = new RxInformation();

                rxInformation = new RxInformation();
                rxInformation.RxNumber = "5116039.05";
                rxInformation.PharmacyCode = "0001";
                listRxInformation.Add(rxInformation);

                //rxInformation = new RxInformation();
                //rxInformation.RxNumber = "7767348.06";
                //rxInformation.PharmacyCode = "7109";
                //listRxInformation.Add(rxInformation);

                //rxInformation = new RxInformation();
                //rxInformation.RxNumber = "7758893.05";
                //rxInformation.PharmacyCode = "7109";
                //listRxInformation.Add(rxInformation);
                
                string remainingQty = string.Empty;
                bool qtyFlag = true;


                accessInformation.OpenUrl(url, SeleniumConstants.BrowserType.Chrome);

                //entering username
                accessInformation.SetText("PageBody_txtLogin", username, FindElement.ById);

                //entering password
                accessInformation.SetText("PageBody_txtPassword", password, FindElement.ById);

                //clicking the login button
                accessInformation.Click("PageBody_btnLogin", FindElement.ById);

                foreach (RxInformation _rxInformation in listRxInformation)
                {

                    //clicking the VMRX home link
                    accessInformation.Click("HomeLink", FindElement.ById);

                    //Clicking the VMRX administrator link
                    accessInformation.Click("ViewMasteRxAdministrationLINK", FindElement.ById);

                    //clicking the user search link
                    accessInformation.Click("UserSearchLINK", FindElement.ById);

                    //Setting the user name
                    accessInformation.SetText("PageBody_txtSearch", username, FindElement.ById);

                    //clicking the search button
                    accessInformation.Click("PageBody_btnSearch", FindElement.ById);

                    //editing the user
                    accessInformation.Click("PageBody_UserSearchGrid1_grdUsers_cmdEdit_0", FindElement.ById);

                    //selecting the pharmacy
                    accessInformation.Select("PageBody_CPF_PharmacyList", _rxInformation.PharmacyCode, FindElement.ById, SelectBy.Value);

                    //selecting the facility
                    accessInformation.Select("PageBody_CPF_FacilityList", "1", FindElement.ById, SelectBy.Index);

                    //click save button
                    accessInformation.Click("PageBody_cmdSave", FindElement.ById);

                    //clicking the VMRX home link
                    accessInformation.Click("HomeLink", FindElement.ById);

                    //clicking the return link
                    accessInformation.Click("PharmacyReturnsServicesLINK", FindElement.ById);

                    //clicking the pharamcy return link
                    accessInformation.Click("PharmacyReturnsServicesLINK", FindElement.ById);



                    qtyFlag = true;
                    for (int qtyCounter = 0; qtyCounter < 2; qtyCounter++)
                    {

                        //entering the Rx information
                        accessInformation.SetText("PageBody_CaptureRx1_txtScannedRx", _rxInformation.RxNumber, FindElement.ById);

                        //click the search button
                        accessInformation.Click("PageBody_CaptureRx1_btnSearchRx", FindElement.ById);

                        //getting the remaining dispence quantity
                        remainingQty = accessInformation.GetText("PageBody_CaptureRx1_lblRemainingQuantity", FindElement.ById);


                        //entering the return quantity
                        if (qtyFlag)
                        {
                            accessInformation.SetText("PageBody_CaptureRx1_txtReturnQuantity", (Convert.ToInt32(Convert.ToDecimal((remainingQty))) - 1).ToString(), FindElement.ById);

                        }
                        else
                        {
                            accessInformation.SetText("PageBody_CaptureRx1_txtReturnQuantity", remainingQty, FindElement.ById);
                        }
                        //clicking the return eligibility
                        accessInformation.Click("PageBody_CaptureRx1_btnCheckReturnEligibility", FindElement.ById);

                        //Setting the text to the Note
                        accessInformation.SetText("PageBody_CaptureRx1_txtReturnReasonNotes", "test", FindElement.ById);

                        TakeScreenCapture(_rxInformation.RxNumber, qtyFlag);
                        qtyFlag = false;

                        //click the reset button
                        accessInformation.Click("PageBody_CaptureRx1_btnSearchRx", FindElement.ById);
                    }



                }


                //click the logg off
                accessInformation.Click("A1", FindElement.ById);
            }
        }

        private static void TakeScreenCapture(string rxNumber,bool isPartial)
        {
            string _path = @"c:\temp\C&RProductionIssues\" + DateTime.Now.ToString("MMddyy") + "/";
            string _partial = "_partial";
            string _full = "_full";
            string _filePath = string.Empty;
            

            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);

            using (ScreenCapture sc = new ScreenCapture())
            {
                Image img = ScreenCapture.CaptureActiveWindow();
                if (isPartial)
                {
                    _filePath = @_path + "/" + rxNumber + _partial + ".jpg";
                }
                else
                {
                    _filePath = @_path + "/" + rxNumber + _full + ".jpg";
                }

                img.Save(_filePath, ImageFormat.Jpeg);
            }

        }
    }
}
