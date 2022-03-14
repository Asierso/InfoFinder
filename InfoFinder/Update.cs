using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InfoFinder
{
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
                }
                catch (Exception ex)
                {
                    //Conection error
                }
            }
            else if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                //Network error
            if (!File.Exists("chromedriver.exe"))
            {
                //References error
            }
        }
    }
}
