using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InfoFinder
{
    public class SearchDriver
    {
        private string GenerateKey(int length)
        {
            //Chars used by keygen
            string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            string key = "";
            var random = new Random();
            for (int i = 0; i < length; i++) //Add amount of chars to key string
            {
                if (random.Next(0, 2) == 0) key += letters[random.Next(0, letters.Length)].ToUpper();
                else key += letters[random.Next(0, letters.Length)];
            }
            return key;
        }
        public void StartSearch(string search, string pages, string filters, string subpath = "")
        {
            if (search != "" && pages != "" && filters != "")
            {
                if (subpath != "") if (!Directory.Exists(subpath)) Directory.CreateDirectory(subpath);
                //Define list of getted websites
                var webs = new List<string>();
                //Define webdriver
                IWebDriver wd = new ChromeDriver();
                wd.Manage().Window.Minimize();
                //Start scrapping search in google
                wd.Navigate().GoToUrl("https://google.com");
                //Close terms and conditions alert
                wd.FindElement(By.Id("L2AGLb")).Click();
                //Introduce search context in google searchbar
                var input = wd.FindElement(By.Name("q"));
                input.SendKeys(search);
                input.Submit();
                for (int i = 0; i < int.Parse(pages); i++)
                {
                    //Make querry to all href elements with specific selector of all page
                    var webCitiesDiv = wd.FindElements(By.CssSelector(".yuRUbf"));
                    webCitiesDiv.ToList().ForEach((obj) =>
                    {
                        //Get link and add it to web list
                        var webCites = obj.FindElement(By.TagName("a"));
                        webs.Add(webCites.GetAttribute("href"));
                    });

                    //Show info
                    

                    //Click to next page
                    try
                    {
                        var webIndex = wd.FindElement(By.XPath("//a[@aria-label='Page " + (i + 2).ToString() + "']"));
                        webIndex.Click();
                    }
                    catch
                    {
                        //Page not exists
                    }
                }

                //URL List getted success
                //FileCreation
                Directory.CreateDirectory(subpath + search);
                string pagesFile = "";
                int pageNum = 0;
                webs.ForEach((obj) =>
                {
                    pagesFile += "Page " + pageNum + " : " + obj + "\n";
                    pageNum++;
                });
                //Write page asyncronously in pages collection
                Task.Run(() => File.WriteAllText(subpath + search + "/@Pages.txt", pagesFile));
                //Scrapper index reseted
                pageNum = 0;
                webs.ForEach((obj) =>
                {
                    var pageInfo = new List<string>();
                    try
                    {
                        if (Path.GetExtension(obj) == ".pdf") //Object is a pdf file
                        {
                            //Download pdf
                            var wclient = new WebClient();
                            if (!Directory.Exists(subpath + search + "/Pdf")) Directory.CreateDirectory(subpath + search + "/Pdf");
                            wclient.DownloadFileAsync(new Uri(obj), subpath + search + "/Pdf/Page " + pageNum + ".pdf");
                        }
                        //Goto next URL
                        wd.Navigate().GoToUrl(obj);
                    }
                    catch (Exception ex)
                    {
                        //Error
                    }
                    try
                    {
                        //Change asterisc to specific HTML elements
                        if (filters == "*") filters = "h1 h2 h3 h4 p img";
                        filters.Split(' ').ToList().ForEach((obj2) =>
                        {
                            if (obj2 == "img") //Object getted is an image
                            {
                                wd.FindElements(By.XPath("//" + obj2)).ToList().ForEach((obj3) =>
                                {
                                    //Download image and save it with a generated name
                                    var webClient = new WebClient();
                                    //Generate name
                                    string key = GenerateKey(20);
                                    //Creating folder structure to save images
                                    if (!Directory.Exists(subpath + search + "/Resources")) Directory.CreateDirectory(subpath + search + "/Resources");
                                    if (!Directory.Exists(subpath + search + "/Resources/Page " + pageNum)) Directory.CreateDirectory(subpath + search + "/Resources/Page " + pageNum);
                                    var extension = Path.GetExtension(obj3.GetAttribute("src"));
                                    if (extension == "") extension = ".png";
                                    //Download async the image file
                                    webClient.DownloadFileAsync(new Uri(obj3.GetAttribute("src")), subpath + search + "/Resources/Page " + pageNum + "/" + key + extension);
                                });
                                //Image generated
                            }
                            else if (obj2 != "") wd.FindElements(By.XPath("//" + obj2)).ToList().ForEach((obj3) => pageInfo.Add(obj3.Text)); //Find elemento using xpath and add it to page info

                        });
                    }
                    catch { }
                    //Get plain text
                    string webFile = "";
                    pageInfo.ForEach((obj2) => webFile += obj2 + "\n");
                    if (!Directory.Exists(subpath + search + "/Text")) Directory.CreateDirectory(subpath + search + "/Text"); //Create folder structure to save plain text
                    if (webFile != "")
                    {
                        //Write asyncronously plain text
                        Task.Run(() => File.WriteAllText(subpath + search + "/Text/Page " + pageNum + ".txt", webFile));
                        //Page generated
                    }
                    //Next page
                    pageNum++;
                });
                //Close webdriver
                wd.Quit();
            }
            else
            {
                //Arguments error
            }
        }
    }
}
