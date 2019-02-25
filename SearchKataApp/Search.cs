using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchKataApp.Entities;

namespace SearchKataApp
{
    /// <summary>
    /// Parses/searches data 
    /// </summary>
    public class Search
    {
        /// <summary>
        /// Main data repository 
        /// </summary>
        private List<Letter> _data { get; set; }
        /// <summary>
        /// Terms to search for 
        /// </summary>
        private List<string> _searchTerms { get; set; }
        /// <summary>
        /// Output of the search 
        /// </summary>
        private List<string> matches { get; set; }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="searchTerms"></param>
        public Search(List<Letter> data, List<string> searchTerms)
        {
            matches = new List<string>();
            _data = data;
            _searchTerms = searchTerms;
        }

        public List<string> ExecuteSearch()
        {
            return matches;
        }

        public string ReturnHorizontal(int row)
        {
            var letters = _data.Where(x => x.YPosition == row).Select(x => x.LetterChar).ToList();
            return string.Join("", letters.ToArray());
        }

        public string ReturnHorizontalBackwards(int row)
        {
            var letters = _data.Where(x => x.YPosition == row).OrderByDescending(x => x.XPosition).Select(x => x.LetterChar)
                .ToList();
            return string.Join("", letters.ToArray());
        }

        public string ReturnVertical(int column)
        {
            var letters = _data.Where(x => x.XPosition == column).OrderBy(x => x.YPosition).Select(x => x.LetterChar).ToList();
            return string.Join("", letters.ToArray());
        }

        public string ReturnVerticalBackwards(int column)
        {
            var letters = _data.Where(x => x.XPosition == column).OrderByDescending(x => x.YPosition).Select(x => x.LetterChar)
                .ToList();
            return string.Join("", letters.ToArray());
        }

        public List<string> ReturnVerticalForwardsAndBackwards()
        {
            var maxColumns = _data.Max(x => x.XPosition);

            var lines = new List<string>();
            var line = string.Empty;
            var list = new List<Letter>();

            // MOVE ALONG X 
            for (int i = 0; i <= maxColumns; i++)
            {
                list = _data.Where(x => x.XPosition == i && x.YPosition >= 0).OrderBy(x => x.YPosition).ToList();
                line = string.Join("", list.Select(x => x.LetterChar).ToArray());

                // line is complete 
                //Console.WriteLine("FORWARD: " + line);
                lines.Add(line);

                // check for match here 

                //Console.WriteLine("BACKWARD: " + string.Join("", list.OrderByDescending(x => x.YPosition)
                //    .Select(x => x.LetterChar).ToArray()));
                lines.Add(string.Join("", list.OrderByDescending(x => x.YPosition).Select(x => x.LetterChar)
                    .ToArray()));

                // check for match here 

                line = string.Empty;
                list = new List<Letter>();
            }

            return lines;
        }

        public List<string> ReturnDiagonalUpwardsForwardsAndBackwards()
        {
            var maxRows = _data.Max(x => x.YPosition);
            var maxColumns = _data.Max(x => x.XPosition);
            //var yPointer = maxRows;
            //var xPointer = maxColumns;

            // 0, 4 
            // 1, 3 
            // 2, 2 
            // 3, 1 
            // 4, 0 

            var lines = new List<string>();
            var line = string.Empty;
            var list = new List<Letter>();

            // MOVE DOWN ALONG Y  
            for (int i = 1; i <= maxRows; i++)
            {
                var xCoordinate = 0;
                var yCoordinate = i;

                while (xCoordinate <= maxColumns && yCoordinate >= 0)
                {
                    //Console.WriteLine("X: " + xCoordinate.ToString() + " Y: " + yCoordinate.ToString());
                    // add to X, add to Y until X reaches max 
                    line += _data.Where(x => x.XPosition == xCoordinate && x.YPosition == yCoordinate).First().LetterChar;
                    list.Add(new Letter
                    {
                        LetterChar = _data.Where(x => x.XPosition == xCoordinate && x.YPosition == yCoordinate).First().LetterChar,
                        XPosition = xCoordinate,
                        YPosition = yCoordinate
                    });
                    xCoordinate++;
                    yCoordinate--;
                }

                // line is complete 
                //Console.WriteLine("FORWARD: " + line);
                lines.Add(line);

                // check for match here 

                var letters = list.OrderByDescending(x => x.XPosition).Select(x => x.LetterChar).ToList();

                //Console.WriteLine("BACKWARD: " + string.Join("", letters.ToArray()));
                lines.Add(string.Join("", letters.ToArray()));

                // check for match here 

                // check if search term is in the line 
                //var searchTerm = "ILYI";
                //if (line.IndexOf(searchTerm) > 0)
                //{
                //    Console.WriteLine("SEARCH TERM FOUND!");
                //    foreach (var l in list.Where(x => x.XPosition >= line.IndexOf(searchTerm) && x.XPosition <= line.IndexOf(searchTerm) + searchTerm.Length - 1))
                //    {
                //        Console.WriteLine(l.LetterChar + " X: " + l.XPosition.ToString() + " Y: " + l.YPosition.ToString());
                //    }
                //}

                line = string.Empty;
                list = new List<Letter>();
            }

            //foreach(var l in lines)
            //{
            //    Console.WriteLine(l);
            //}

            // MOVE RIGHT ALONG X 
            for (int i = 1; i <= maxColumns - 1; i++)
            {
                var xCoordinate = i;
                var yCoordinate = maxRows;

                while (xCoordinate <= maxColumns && yCoordinate >= 0)
                {
                    //Console.WriteLine("X: " + xCoordinate.ToString() + " Y: " + yCoordinate.ToString());
                    line += _data.Where(x => x.XPosition == xCoordinate && x.YPosition == yCoordinate).First().LetterChar;
                    list.Add(new Letter
                    {
                        LetterChar = _data.Where(x => x.XPosition == xCoordinate && x.YPosition == yCoordinate).First().LetterChar,
                        XPosition = xCoordinate,
                        YPosition = yCoordinate
                    });
                    xCoordinate++;
                    yCoordinate--;
                }

                // line is complete 
                //Console.WriteLine("FORWARD: " + line);
                lines.Add(line);

                // check for match here 

                var letters = list.OrderByDescending(x => x.XPosition).Select(x => x.LetterChar).ToList();

                //Console.WriteLine("BACKWARD: " + string.Join("", letters.ToArray()));
                lines.Add(string.Join("", letters.ToArray()));

                // check for match here 

                line = string.Empty;
                list = new List<Letter>();
            }

            return lines;
        }

        public List<string> ReturnDiagonalDownwardsForwardsAndBackwards()
        {
            var maxRows = _data.Max(x => x.YPosition);
            var maxColumns = _data.Max(x => x.XPosition);
            //var yPointer = maxRows;
            //var xPointer = maxColumns;

            // 0, 10 
            // 1, 11 
            // 2, 12 
            // 3, 13 
            // 4, 14 

            var lines = new List<string>();
            var line = string.Empty;
            var list = new List<Letter>();

            // MOVE UP ALONG Y 
            for (int i = maxRows - 1; i >= 0; i--)
            {
                var xCoordinate = 0;
                var yCoordinate = i;

                while (xCoordinate <= maxColumns && yCoordinate <= maxRows)
                {
                    //Console.WriteLine("X: " + xCoordinate.ToString() + " Y: " + yCoordinate.ToString());
                    // add to X, add to Y until X reaches max 
                    line += _data.Where(x => x.XPosition == xCoordinate && x.YPosition == yCoordinate).First().LetterChar;
                    list.Add(new Letter
                    {
                        LetterChar = _data.Where(x => x.XPosition == xCoordinate && x.YPosition == yCoordinate).First().LetterChar,
                        XPosition = xCoordinate,
                        YPosition = yCoordinate
                    });
                    xCoordinate++;
                    yCoordinate++;
                }

                // line is complete 
                //Console.WriteLine("FORWARD: " + line);
                lines.Add(line);

                // check for match here 

                var letters = list.OrderByDescending(x => x.XPosition).Select(x => x.LetterChar).ToList();

                //Console.WriteLine("BACKWARD: " + string.Join("", letters.ToArray()));
                lines.Add(string.Join("", letters.ToArray()));

                // check for match here 

                line = string.Empty;
                list = new List<Letter>();
            }

            // MOVE RIGHT ALONG X 
            for (int i = 1; i <= maxColumns - 1; i++)
            {
                var xCoordinate = i;
                var yCoordinate = 0;

                while (xCoordinate <= maxColumns && yCoordinate <= maxRows)
                {
                    //Console.WriteLine("X: " + xCoordinate.ToString() + " Y: " + yCoordinate.ToString());
                    line += _data.Where(x => x.XPosition == xCoordinate && x.YPosition == yCoordinate).First().LetterChar;
                    list.Add(new Letter
                    {
                        LetterChar = _data.Where(x => x.XPosition == xCoordinate && x.YPosition == yCoordinate).First().LetterChar,
                        XPosition = xCoordinate,
                        YPosition = yCoordinate
                    });
                    xCoordinate++;
                    yCoordinate++;
                }

                // line is complete 
                //Console.WriteLine("FORWARD: " + line);
                lines.Add(line);

                // check for match here 

                var letters = list.OrderByDescending(x => x.XPosition).Select(x => x.LetterChar).ToList();

                //Console.WriteLine("BACKWARD: " + string.Join("", letters.ToArray()));
                lines.Add(string.Join("", letters.ToArray()));

                // check for match here 

                line = string.Empty;
                list = new List<Letter>();
            }

            return lines;
        }
    }
}