using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CsvHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace RitualsScraperUK
{
    class Scraper
    {
        private string categoryUrl;
        private string categoryName;
        private List<Product> productList;

        public Scraper(string categoryUrl, string categoryName)
        {
            this.categoryUrl = categoryUrl;
            this.categoryName = categoryName;
            productList = new List<Product>();
        }

        public void NavigateToCategoryPage()
        {
            var webDriver = new ChromeDriver();
            webDriver.Navigate().GoToUrl(categoryUrl);

            NavigateToSubCategories(webDriver);
            WriteListToCsv();
        }

        private void NavigateToSubCategories(ChromeDriver webDriver)
        {
            var subCategoryList = GetSubCategories(webDriver.FindElementByClassName("refinement"));

            foreach (var subCategory in subCategoryList)
            {
                var subCategoryName = subCategory.GetAttribute("title").Substring(16);
                var subCategoryUrl = subCategory.GetAttribute("href");

                var subWebDriver = new ChromeDriver();
                subWebDriver.Navigate().GoToUrl(subCategoryUrl);

                ScrollToBottom(subWebDriver);
                Scrape(subWebDriver, subCategoryName);
                subWebDriver.Close();
            }

        }

        private void ScrollToBottom(ChromeDriver webDriver)
        {
            int productCountBefore;
            int productCountAfter;

            if (webDriver.FindElementsByClassName("wrapper__seo-block").Count > 0)
            {
                do
                {
                    productCountBefore = webDriver.FindElementsByClassName("grid-tile").Count;
                    var bottomElement = webDriver.FindElementByClassName("wrapper__seo-block");
                    Actions action = new Actions(webDriver);
                    action.MoveToElement(bottomElement).Perform();
                    Thread.Sleep(1000);
                    productCountAfter = webDriver.FindElementsByClassName("grid-tile").Count;

                } while (productCountBefore < productCountAfter);
            }
            else
            {
                do
                {
                    productCountBefore = webDriver.FindElementsByClassName("grid-tile").Count;
                    var bottomElement = webDriver.FindElementByClassName("stay-informed");
                    Actions action = new Actions(webDriver);
                    action.MoveToElement(bottomElement).Perform();
                    Thread.Sleep(400);
                    productCountAfter = webDriver.FindElementsByClassName("grid-tile").Count;

                } while (productCountBefore < productCountAfter);
            }


            
        }

        private void Scrape(ChromeDriver webDriver, string subCategoryName)
        {
            var productTiles = webDriver.FindElementsByClassName("grid-tile");

            foreach (var productTile in productTiles)
            {
                var productUrl = productTile.FindElement(By.ClassName("name-link")).GetAttribute("href");
                var productName =
                    productTile.FindElement(By.ClassName("product-name")).FindElement(By.TagName("a")).Text;
                string productPrice;
                string productPromoPrice;

                if (productTile.FindElements(By.ClassName("product-standard-price")).Count > 0)
                {
                    productPrice = productTile.FindElement(By.ClassName("product-standard-price")).Text;
                    productPromoPrice = productTile.FindElement(By.ClassName("product-sales-price")).Text;
                }
                else
                {
                    productPrice = productTile.FindElement(By.ClassName("product-sales-price")).Text;
                    productPromoPrice = null;
                }


                var productWebDriver = new ChromeDriver();
                productWebDriver.Navigate().GoToUrl(productUrl);

                var productSize = GetSizeInfo(productWebDriver);

                CreateNewProduct(productName, productPrice, productPromoPrice, productSize, productUrl, subCategoryName);
                productWebDriver.Close();
            }
        }

        private string GetSizeInfo(ChromeDriver webDriver)
        {

            try

            {
                webDriver.FindElementById("unit").Click();
                return webDriver.FindElementByXPath("//*[@id=\"ui-accordion-1-panel-1\"]").Text;
            }
            catch (Exception e)
            {
                if (webDriver.FindElementsById("unit").Count > 0)
                {
                    webDriver.Manage().Window.Maximize();
                    //var unitSize = webDriver.FindElementById("unit");
                    //Actions action = new Actions(webDriver);
                    //action.MoveToElement(unitSize).Perform();
                    Thread.Sleep(1000);
                    webDriver.FindElementById("unit").Click();
                    return webDriver.FindElementByXPath("//*[@id=\"ui-accordion-1-panel-1\"]").Text;
                }
                else
                {
                    return null;
                }
               
            }

        }

        private ReadOnlyCollection<IWebElement> GetSubCategories(IWebElement element)
        {            
            return element.FindElements(By.ClassName("refinement-link "));
        }

        private void CreateNewProduct(string productName, string listPrice, string productSalePrice,
            string productSize, string productUrl, string subCategoryName)
        {
            var product = new Product()
            {
                ListPrice = listPrice,
                ProductName = productName,
                ProductSalePrice = productSalePrice,
                ProductSize = productSize,
                ProductUrl = productUrl,
                Category = categoryName,
                SubCategory = subCategoryName

            };

            productList.Add(product);
        }

        public void WriteListToCsv()
        {
            using (TextWriter writer = new StreamWriter(@"C:\Users\Alexander.Glassman\Documents\RitualsScraperUK\" + categoryName + "Results.csv"))
            {
                var csv = new CsvWriter(writer);
                csv.WriteRecords(productList);
            }
        }
    }
}
