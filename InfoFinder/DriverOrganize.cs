using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace InfoFinder
{
    internal class DriverOrganize
    {
        private Queue<Query> Organizations = new Queue<Query>();
        public List<Query> Processing = new List<Query>();
        public void Start()
        {
            for(int i = Organizations.Count - 1; i >= 0; i--)
            {
                var query = Organizations.Dequeue();
                RenderQuery(query);
            }
        }
        public void AddQuerry(Query query) => Organizations.Enqueue(query);

        private async void RenderQuery(Query query)
        {
            var render = query.Render;
            await Task.Run(() =>
            {
                Processing.Add(query);
                var sdriver = new SearchDriver();
                sdriver.StartSearch(query.Data, query.Pages, query.Filter);
                Processing.Remove(query);
            });
            if (render != null)
            {
                ((System.Windows.Forms.ProgressBar)render.Controls[0]).Value = 100;
                ((System.Windows.Forms.ProgressBar)render.Controls[0]).Style = ProgressBarStyle.Continuous;
                ((System.Windows.Forms.Button)render.Controls[2]).Enabled = true;
                ((System.Windows.Forms.Button)render.Controls[2]).Click += (o, e) =>
                {
                    Process.Start(Path.GetFullPath(query.Data));
                };
            }
        }
    }
    
}
