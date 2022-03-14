using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoFinder
{
    internal class DriverOrganize
    {
        private Queue<Query> Organizations = new Queue<Query>();
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
            await Task.Run(() =>
            {
                var sdriver = new SearchDriver();
                sdriver.StartSearch(query.Data, query.Pages, query.Filter);
            });
        }
    }
    
}
