using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibraryADO;
using UCSD.VideoLibrary;

namespace TestAdo
{
    class Program
    {
        static void Main(string[] args)
        {
            VideoSearchCriteria vtest = new VideoSearchCriteria();
            vtest.Title = "*";
            vtest.Director = "*";
            vtest.Year = 1984;
            ADOImp test = new ADOImp();
            VideoSearchResult result = new VideoSearchResult();

            Console.WriteLine("{0}", test.SearchVideoLibrary(vtest));
            Console.ReadLine();
            
            
            

        }
    }
}
