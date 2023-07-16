using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfoFinder
{
    public partial class SearchMenu : Form
    {
        private DriverOrganize driverOrganize = new DriverOrganize();
        public SearchMenu()
        {
            InitializeComponent();
        }

        private void SearchMenu_Load(object sender, EventArgs e)
        {
            //Check chrome version
            var version = new ChromeDriverCheck();
            versionTxt.Text = "Chrome v" + version.CheckVersion().BrowserVersion + " InfoFinder v" + Application.ProductVersion + " by Asierso";
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if(subjectTxt.Text.Contains(";"))
            {
                var subjects = subjectTxt.Text.Split(';');
                foreach(var subject in subjects)
                {
                    var query = new Query();
                    query.Filter = filtersTxt.Text;
                    query.Data = subject;
                    query.Pages = pagesUdc.Text;
                    var render = SearchList.Render(new Point(0, SearchList.PositionSpacer), activeSearch, query);
                    query.Render = render;
                    driverOrganize.AddQuerry(query);
                    activeSearch.Controls.Add(render);
                    SearchList.PositionSpacer += 35;
                }
            }
            else
            {
                var query = new Query();
                query.Filter = filtersTxt.Text;
                query.Data = subjectTxt.Text;
                query.Pages = pagesUdc.Text;
                driverOrganize.AddQuerry(query);
                var render = SearchList.Render(new Point(0, SearchList.PositionSpacer), activeSearch, query);
                query.Render = render;
                driverOrganize.AddQuerry(query);
                activeSearch.Controls.Add(render);
                SearchList.PositionSpacer += 35;
            }

            driverOrganize.Start();
        }

        private void programURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Asierso/InfoFinder");
        }

        private void filtersTxt_TextChanged(object sender, EventArgs e) => validateData();

        private void subjectTxt_TextChanged(object sender, EventArgs e) => validateData();
        private void validateData()
        {
            if(filtersTxt.Text.Length > 0 && subjectTxt.Text.Length > 0) 
                searchButton.Enabled = true;
            else
                searchButton.Enabled = false;
        }
    }
    
}
