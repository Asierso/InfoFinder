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
using System.Windows.Forms;

namespace InfoFinder
{
    class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private extern static int ShowWindow(System.IntPtr hWnd, int nCmdShow);

        static void Main(string[] args)
        {
            ShowWindow(Process.GetCurrentProcess().MainWindowHandle, 0);
            var cdriverChecker = new ChromeDriverCheck();
            //cdriverChecker.Update(cdriverChecker.CheckVersion());
            var menu = new SearchMenu();

            Application.EnableVisualStyles();
            Application.Run(menu);

        }
    }
}
