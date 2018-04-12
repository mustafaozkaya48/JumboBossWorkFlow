using _DbEntities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _BusinessLayer.Helpers
{
   public class FileDownloads
    {
        public List<WorkAddition> GetFile()
        {

            List<WorkAddition> listFiles = new List<WorkAddition>();

            string fileSavePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Images");

            DirectoryInfo dirInfo = new DirectoryInfo(fileSavePath);

            int i = 0;

            foreach (var item in dirInfo.GetFiles())
            {
                listFiles.Add(new WorkAddition()
                {

                    Id = new Guid(),

                    Filename = item.Name,

                    FilePath = dirInfo.FullName + @"\" + item.Name

                });

                i = i + 1;

            }

            return listFiles;

        }

    }
}
}
