using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SearchKataApp.Entities;

namespace SearchKataApp
{
    public class Loader
    {
        /// <summary>
        /// Given a filepath, load the file and extract search terms and searchable data 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool LoadFile(string path)
        {
            var data = new List<Letter>();
            String line;
            var rowNum = 0;
            try
            {
                var streamReader = new StreamReader(path);

                // read from file 
                line = streamReader.ReadLine();

                // read to EOF 
                while (line != null)
                {
                    if (rowNum == 0)
                    {
                        // load search terms from the first line 
                        var searchTerms = line;
                    }
                    //data.AddRange(line);
                    line = streamReader.ReadLine();
                    rowNum++;
                }

                // close the file
                streamReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return true;
        }
    }
}