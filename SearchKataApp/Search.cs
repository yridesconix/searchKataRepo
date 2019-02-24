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
    }
}