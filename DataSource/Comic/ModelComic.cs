using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Comic
{
    public class ModelComic
    {
        public static ComicEntities ComicEntities
        {
            get
            {
                return new ComicEntities();
            }
        }
    }
}
