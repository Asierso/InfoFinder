using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfoFinder
{
    public class Message
    {
        public static void ShowError(string title, string message)
        {
            MessageBox.Show(message,title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
