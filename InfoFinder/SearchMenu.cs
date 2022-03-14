using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfoFinder
{
    public partial class SearchMenu : Form
    {
        public SearchMenu()
        {
            InitializeComponent();
        }

        private void SearchMenu_Load(object sender, EventArgs e)
        {

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            var driverOrganize = new DriverOrganize();
            if(subjectTxt.Text.Contains(";"))
            {
                var subjects = subjectTxt.Text.Split(';');
                foreach(var subject in subjects)
                {
                    var query = new Query();
                    query.Filter = filtersTxt.Text;
                    query.Data = subject;
                    query.Pages = pagesUdc.Text;
                    driverOrganize.AddQuerry(query);
                }
            }
            else
            {
                var query = new Query();
                query.Filter = filtersTxt.Text;
                query.Data = subjectTxt.Text;
                query.Pages = pagesUdc.Text;
                driverOrganize.AddQuerry(query);
            }

            driverOrganize.Start();
        }
    }
}
