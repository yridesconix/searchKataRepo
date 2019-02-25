using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchKataApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var loader = new Loader();

            Console.WriteLine("Enter filepath:");
            var path = Console.ReadLine();
            loader.LoadFile(path);

            Console.WriteLine("File loaded. Ready?");
            Console.ReadKey();

            var search = new Search(loader.Data, loader.SearchTerms);
            var matches = search.ExecuteSearch();

            foreach (var match in matches.OrderBy(x => x))
            {
                Console.WriteLine(match);
            }

            Console.WriteLine("Done?");
            Console.ReadKey();
        }
    }
}