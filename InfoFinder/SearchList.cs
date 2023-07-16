using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfoFinder
{
    public class SearchList
    {
        public static int PositionSpacer = 0;
        public static Control Render(Point pos, Control container, Query reference)
        {
            string data = reference.Data;
            var panel = new Panel();
            panel.Location = new Point(1 + pos.X, 2 + pos.Y);
            panel.Size = new Size(container.Size.Width, 25);
            panel.BackColor = Color.Gray;

            var text = new Label();
            if (data.Length < 10)
                text.Text = data;
            else
                text.Text = data.Substring(0, 10) + "...";
            text.Location = new Point(5, 5);
            //Progress bar creation
            var progressbar = new ProgressBar();
            progressbar.MarqueeAnimationSpeed = 60 * int.Parse(reference.Pages);
            progressbar.Style = ProgressBarStyle.Marquee;
            progressbar.Location = new Point(80 + pos.X, 5);
            progressbar.Size = new Size(100, container.Size.Height / 10);

            var openFolder = new Button();
            openFolder.Text = "Abrir";
            openFolder.FlatStyle = FlatStyle.System;
            openFolder.Location = new Point(80 + pos.X + progressbar.Size.Width + 5, 4);
            openFolder.Size = new Size(30, container.Size.Height / 9);
            openFolder.Enabled = false;

            panel.Controls.Add(progressbar);
            panel.Controls.Add(text);
            panel.Controls.Add(openFolder);
            return panel;
        }
    }
}
