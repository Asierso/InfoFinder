using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO.Compression;

namespace InfoFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            var cdriverChecker = new ChromeDriverCheck();
            cdriverChecker.Update(cdriverChecker.CheckVersion());
            //Logo
            ConsoleTemplates.PrintLogo();

            if (args.Length == 0)
            {
                //Parameters
                ConsoleTemplates.PrintOptions(0);
                var search = Console.ReadLine();
                ConsoleTemplates.PrintOptions(1);
                var pages = Console.ReadLine();
                ConsoleTemplates.PrintOptions(2);
                var filters = Console.ReadLine();

                //Show info
                ConsoleTemplates.PrintStartMessage();

                //Create SearchDriver object
                var sdriver = new SearchDriver();
                sdriver.StartSearch(search, pages, filters);
            }
            else if (args[0] == "--search")
            {
                try
                {
                    //Parameters
                    ConsoleTemplates.PrintOptions(0, args[1] + "\n");
                    ConsoleTemplates.PrintOptions(1, args[2] + "\n");
                    ConsoleTemplates.PrintOptions(2, args[3] + "\n");
                    ConsoleTemplates.PrintStartMessage();

                    //Create SearchDriver object
                    var sdriver = new SearchDriver();
                    sdriver.StartSearch(args[1], args[2], args[3], args[4] + "/");
                }
                catch
                {
                    ConsoleTemplates.PrintMessage("Arguments can't be void",ConsoleColor.Red);
                }
            }
            else
            {
                //Read csv
                var file = File.ReadAllText(args[0]);

                //Divide file in lines and format it
                var lines = file.Split('\n');
                for (int i = 0; i < lines.Length; i++) if (!lines[i].Contains(";¶")) lines[i] = lines[i].TrimEnd('\n') + ";¶";

                //Divide lines in gaps
                var l1 = lines[0].Split(';');
                var l2 = lines[1].Split(';');
                var l3 = lines[2].Split(';');

                //Only testing purpose: File.WriteAllText(args[0], lines[0] + "\n" + lines[1] + "\n" + lines[2]);

                //Show info
                ConsoleTemplates.PrintOptions(0, l1[0] + "\n");
                ConsoleTemplates.PrintOptions(1, l2[0] + "\n");
                ConsoleTemplates.PrintOptions(2, l3[0] + "\n");
                ConsoleTemplates.PrintStartMessage();

                //Pages divisor by process
                for (int i = 1; i < l2.Length - 1; i++)
                {
                    var psi = new ProcessStartInfo();
                    psi.FileName = "InfoFinder.exe";
                    psi.Arguments = "--search " + "\"" + l1[i] + "\" \"" + l2[i] + "\" \"" + l3[i] + "\" \"" + Path.GetFileNameWithoutExtension(args[0]) + "\"";
                    Process.Start(psi);
                }

                //Create SearchDriver object
                var sdriver = new SearchDriver();
                sdriver.StartSearch(l1[0], l2[0], l3[0], Path.GetFileNameWithoutExtension(args[0]) + "/");
            }

            //Show info
            ConsoleTemplates.PrintExitMessage();
            Console.ReadLine();
        }
    }
    public class SearchDriver
    {
        private string GenerateKey(int length)
        {
            //Chars used by keygen
            string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            string key = "";
            var random = new Random();
            for(int i = 0;i<length;i++) //Add amount of chars to key string
            {
                if(random.Next(0,2) == 0) key += letters[random.Next(0, letters.Length)].ToUpper();
                else key += letters[random.Next(0, letters.Length)];
            }
            return key;
        }
        public void StartSearch(string search,string pages,string filters,string subpath = "")
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
                    ConsoleTemplates.PrintMessage("Getted List " + (i + 1).ToString() + " of related URL",ConsoleColor.Green);

                    //Click to next page
                    try
                    {
                        var webIndex = wd.FindElement(By.XPath("//a[@aria-label='Page " + (i + 2).ToString() + "']"));
                        webIndex.Click();
                    }
                    catch
                    {
                        ConsoleTemplates.PrintMessage("Not more results finded",ConsoleColor.Red);
                    }           
                }

                //URL List getted success
                ConsoleTemplates.PrintMessage("Getted all URL",ConsoleColor.Green);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n# Starting information getter #\n");
                Console.ResetColor();
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
                Task.Run(()=>File.WriteAllText(subpath + search + "/@Pages.txt", pagesFile));
                //Scrapper index reseted
                pageNum = 0;
                webs.ForEach((obj) =>
                {
                    var pageInfo = new List<string>();
                    try
                    {
                        if(Path.GetExtension(obj) == ".pdf") //Object is a pdf file
                        {
                            //Download pdf
                            var wclient = new WebClient();
                            if (!Directory.Exists(subpath + search + "/Pdf")) Directory.CreateDirectory(subpath + search + "/Pdf");
                            wclient.DownloadFileAsync(new Uri(obj), subpath + search + "/Pdf/Page " + pageNum + ".pdf");
                            //Show info
                            ConsoleTemplates.PrintMessage("Page " + pageNum + ".pdf of " + obj + " generated sucess",ConsoleColor.Green);
                        }
                        //Goto next URL
                        wd.Navigate().GoToUrl(obj);
                    }
                    catch (Exception ex)
                    {
                        //Show error code
                        ConsoleTemplates.PrintMessage("Host " + obj + " not accesible. " + ex.Message,ConsoleColor.Red);
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
                                //Show info
                                ConsoleTemplates.PrintMessage("Resources " + pageNum + " of " + obj + " generated sucess",ConsoleColor.Green);
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
                        Task.Run(()=>File.WriteAllText(subpath + search + "/Text/Page " + pageNum + ".txt", webFile));
                        //Show info
                        ConsoleTemplates.PrintMessage("Page " + pageNum + ".txt of " + obj + " generated sucess",ConsoleColor.Green);
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
                ConsoleTemplates.PrintMessage("Arguments can't be void",ConsoleColor.Red);
            }
            ConsoleTemplates.Exit(0);
        }
    }
    public class ConsoleTemplates
    {
        private static List<string> Logs = new List<string>();

        #region Menu items
        public static void PrintLogo()
        {
            //Intro message
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("");
            Console.WriteLine("   ####               ###             #######    ##                 ### ");
            Console.WriteLine("    ##               ## ##             ##   #                        ## ");
            Console.WriteLine("    ##     #####      #       ####     ## #     ###     #####        ##    ####    ###### ");
            Console.WriteLine("    ##     ##  ##   ####     ##  ##    ####      ##     ##  ##    #####   ##  ##    ##  ## ");
            Console.WriteLine("    ##     ##  ##    ##      ##  ##    ## #      ##     ##  ##   ##  ##   ######    ## ");
            Console.WriteLine("    ##     ##  ##    ##      ##  ##    ##        ##     ##  ##   ##  ##   ##        ## ");
            Console.WriteLine("   ####    ##  ##   ####      ####    ####      ####    ##  ##    ######   #####   #### ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nProgram created by Asierso\nInfoFinder Ver 1.3.2\nPress Ctrl + C to exit\n-------------------");
            Console.ResetColor();
        }

        public static void PrintOptions(int option, string addition = "")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            //Options cases
            switch (option)
            {
                case 0: Console.Write("Topic to search: " + addition); break;
                case 1: Console.Write("Max lists: " + addition); break;
                case 2: Console.Write("Filter tags (Use * for include basics): " + addition); break;
            }
            Console.ResetColor();
        }
        public static void PrintStartMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-------------------");
            Console.WriteLine("\n# Starting page finder #\n");
            Console.ResetColor();
        }

        public static void PrintExitMessage()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n# All done, you can close this window #\n");
            Console.ResetColor();
        }

        #endregion

        public static void PrintMessage(string text,ConsoleColor color)
        {
            //Set color and write text
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            //Reset text color
            Console.ResetColor();
            //Add text to logs
            Logs.Add(string.Format("[{0}]:{1} ",DateTime.Now,text));
        }
        public static void Exit(int exitCode = 0) //Save log file and exit
        {
            //Define usable vars
            string logFile = "logfile.log";
            string plainLogs = "";
            //List to save old logs
            var preLogs = new List<string>();
            //Read log file if exists
            if (File.Exists(logFile))
                preLogs.AddRange(File.ReadAllText(logFile).Split('\n'));
            //Add plained data to str
            preLogs.ForEach(log => plainLogs += log + "\n");
            Logs.ForEach(log => plainLogs += log + "\n");
            //Write logs
            File.WriteAllText(logFile, plainLogs);
            //Close cmd
            Environment.Exit(exitCode);
        }
    }

    public class ChromeDriverCheck
    {
        public struct SearchVersion
        {
            public string BrowserVersion { get; set; } //Version of local Google Chrome
            public string DriverVersion { get; set; } //Version of installed Chromedriver
        }

        private string VersionInfoFile = "cdver.inf";
        public SearchVersion CheckVersion()
        {
            //Define default value of cdriver (update it by default)
            string cdver = "0";
            if (File.Exists("cdver.inf") && File.Exists("chromedriver.exe")) //Detects inf file to load it
                cdver = File.ReadAllText(VersionInfoFile);
            return new SearchVersion()
            {
                //Get chrome version at regedit
                BrowserVersion = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Google\Chrome\BLBeacon", "version", null).ToString(),
                DriverVersion = cdver
            };
        }
        public void Update(SearchVersion searchVersion) //Updates chromedriver to last cdn version
        {
            if (searchVersion.DriverVersion != searchVersion.BrowserVersion && System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()) //Check network and outdated version
            {
                try
                {
                    //Prepare to download file
                    ConsoleTemplates.PrintMessage("Downloading and installing chromedriver, this may take a few seconds", ConsoleColor.Yellow);
                    var wclient = new WebClient();
                    var zipName = "chromedriver.zip";
                    var chromeVersionArray = searchVersion.BrowserVersion.Split('.');
                    //Get complete chromedriver version name at cdn
                    string seleniumVersion = wclient.DownloadString(String.Format("https://chromedriver.storage.googleapis.com/LATEST_RELEASE_{0}.{1}.{2}", chromeVersionArray[0], chromeVersionArray[1], chromeVersionArray[2]));
                    //Delete old version
                    File.Delete("chromedriver.exe");
                    //Download chromedriver from cdn
                    wclient.DownloadFile(String.Format("https://chromedriver.storage.googleapis.com/{0}/chromedriver_win32.zip", seleniumVersion), zipName);
                    //Install chromedriver (uncompress and update version in file)
                    File.SetAttributes(zipName, FileAttributes.Hidden);
                    ZipFile.ExtractToDirectory(zipName, ".");
                    File.Delete(zipName);
                    File.WriteAllText(VersionInfoFile, searchVersion.BrowserVersion);
                    ConsoleTemplates.PrintMessage("All done", ConsoleColor.Green);
                    Console.Clear();
                }
                catch(Exception ex)
                {
                    ConsoleTemplates.PrintMessage("Could not connect to the cdn (" + ex.Message + ")", ConsoleColor.Red); //Internet error
                }
            }
            else if(!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()) 
                ConsoleTemplates.PrintMessage("Could not connect to the cdn",ConsoleColor.Red); //Network error
            if (!File.Exists("chromedriver.exe"))
            {
                //chromedriver removed and no network
                ConsoleTemplates.PrintMessage("Broken references / Chromedriver.exe doesn't exits", ConsoleColor.Red);
                Console.ReadLine();
                ConsoleTemplates.Exit(1);
            }
        }
    }
}
