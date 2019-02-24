using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SearchKataApp.Entities;

namespace SearchKataApp
{
    /// <summary>
    /// Loads file 
    /// </summary>
    public class Loader
    {
        /// <summary>
        /// Search terms 
        /// </summary>
        public List<string> SearchTerms { get; set; }
        /// <summary>
        /// Main repository of searchable data 
        /// </summary>
        public List<Letter> Data { get; set; }

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
                        SearchTerms = GetSearchTerms(line);
                    }
                    data.AddRange(ParseLine(line, rowNum));
                    line = streamReader.ReadLine();
                    rowNum++;
                }

                // close the file
                streamReader.Close();

                // load all searchable data 
                Data = data;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            return true;
        }

        /// <summary>
        /// Given a line of text and a row number, aggregate X/Y coordinate info on all letters in the line 
        /// NOTE: this does not include the header/first line, which specs say has the search terms 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="rowNum"></param>
        /// <returns></returns>
        private List<Letter> ParseLine(string line, int rowNum)
        {
            var xPosition = 0;
            var data = new List<Letter>();
            var allLetters = line.Split(',').ToList();

            foreach (var letter in allLetters)
            {
                data.Add(new Letter
                {
                    LetterChar = letter.ToString(),
                    XPosition = xPosition,
                    YPosition = rowNum - 1
                });
                xPosition++;
            }

            return data;
        }

        /// <summary>
        /// Given a comma delimited string, extract the search terms 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private List<string> GetSearchTerms(string line)
        {
            return line.Split(',').ToList();
        }
    }
}