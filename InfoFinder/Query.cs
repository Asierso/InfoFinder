using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfoFinder
{
    public struct Query
    {
        public string Data { get; set; }
        public string Pages { get; set; }
        public string Filter { get; set; }
        public Control Render { get; set; }
    }
}
