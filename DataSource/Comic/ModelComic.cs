using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSource.Comic
{
    public static class ModelComic
    {
        private static ComicEntities comicEntities;

        public static ComicEntities ComicEntities
        {
            get
            {
                if (comicEntities == null)
                {
                    comicEntities = new ComicEntities();
                }

                return comicEntities;
            }
        }
    }
}
