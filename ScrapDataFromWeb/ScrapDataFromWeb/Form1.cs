using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ScrapDataFromWeb
{
    public partial class Form1 : Form
    {
        public IWebDriver driver;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        public void Open_Browser()
        {
            
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;

            //var options = new ChromeOptions();
            //options.AddArgument("headless");

            driver = new ChromeDriver(driverService);


            try
            {
                driver.Navigate().GoToUrl("https://money.cnn.com/data/hotstocks/index.html");
            }
            catch
            {
                throw;
            }

            
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            Open_Browser();
        }


        private void Find_Data()
        {
            IList<IWebElement> searchElements = driver.FindElements(By.TagName("tbody"));

            foreach (IWebElement i in searchElements)
            {

                HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                var text = i.GetAttribute("innerHTML");
                htmlDocument.LoadHtml(text);

                var inputs = htmlDocument.DocumentNode.Descendants("tr").ToList();

                foreach (var items in inputs)
                {
                    HtmlAgilityPack.HtmlDocument htmlDocument1 = new HtmlAgilityPack.HtmlDocument();
                    htmlDocument1.LoadHtml(items.InnerHtml);

                    var tds = htmlDocument1.DocumentNode.Descendants("td").ToList();

                    if(tds.Count != 0)
                        txtResults.AppendText(tds[0].InnerText + " " + tds[1].InnerText +" " + tds[2].InnerText + " " + tds[3].InnerText + "\t\r");
                }

                txtResults.AppendText("\t\r");

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if(driver!= null)
                driver.Quit();
        }

        private void btnScrap_Click(object sender, EventArgs e)
        {
            Find_Data();
        }
    }
}
