using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchKataApp.Entities;
using SearchKataApp.Enums;

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
            CollectNondiagonal(Directions.Vertical);
            CollectNondiagonal(Directions.Horizontal);
            CollectDiagonal(Directions.Upward);
            CollectDiagonal(Directions.Downward);

            return matches;
        }

        #region Private Methods
        /// <summary>
        /// Collector of forward/backward vertical/horizontal/non-diagonal 
        /// </summary>
        /// <param name="horizontalOrVertical"></param>
        private void CollectNondiagonal(Directions horizontalOrVertical)
        {
            var maxIndex = horizontalOrVertical == Directions.Vertical ? _data.Max(x => x.XPosition)
                : _data.Max(x => x.YPosition);
            var list = new List<Letter>();

            // move along index 
            for (int i = 0; i <= maxIndex; i++)
            {
                list = horizontalOrVertical == Directions.Vertical
                    ? _data.Where(x => x.XPosition == i && x.YPosition >= 0).OrderBy(x => x.YPosition).ToList()
                    : _data.Where(x => x.YPosition == i).OrderBy(x => x.XPosition).ToList();

                // inspect forward matches 
                ExtractAndMatchHorizOrVert(list, horizontalOrVertical, Directions.Forward);

                // inspect backward matches 
                ExtractAndMatchHorizOrVert(list, horizontalOrVertical, Directions.Backward);

                list = new List<Letter>();
            }
        }

        /// <summary>
        /// Collector of foward/backward diagonal upward/downwards 
        /// </summary>
        /// <param name="searchTerms"></param>
        /// <param name="upwardOrDownward"></param>
        private void CollectDiagonal(Directions upwardOrDownward)
        {
            var maxRows = _data.Max(x => x.YPosition);
            var maxColumns = _data.Max(x => x.XPosition);

            ScanYAxis(maxRows, maxColumns, upwardOrDownward);
            ScanXAxis(maxRows, maxColumns, upwardOrDownward);
        }
        #endregion

        #region Helper methods
        /// <summary>
        /// Scans along the Y axis 
        /// </summary>
        /// <param name="maxRows"></param>
        /// <param name="maxColumns"></param>
        /// <param name="upwardOrDownward"></param>
        private void ScanYAxis(int maxRows, int maxColumns, Directions upwardOrDownward)
        {
            var line = string.Empty;
            var list = new List<Letter>();

            // if direction is upward, scan Y axis heading down 
            if (upwardOrDownward == Directions.Upward)
            {
                for (int i = 1; i <= maxRows; i++)
                {
                    var xCoordinate = 0;
                    var yCoordinate = i;

                    while (xCoordinate <= maxColumns && yCoordinate >= 0)
                    {
                        BuildSearchLineAndList(xCoordinate, yCoordinate, ref line, ref list);
                        xCoordinate++;
                        yCoordinate--;
                    }

                    ExtractAndMatchDiagonal(line, list);

                    line = string.Empty;
                    list = new List<Letter>();
                }
            }
            else
            {
                // if direction is downward, scan Y axis heading up 
                for (int i = maxRows - 1; i >= 0; i--)
                {
                    var xCoordinate = 0;
                    var yCoordinate = i;

                    while (yCoordinate <= maxRows && xCoordinate <= maxColumns)
                    {
                        BuildSearchLineAndList(xCoordinate, yCoordinate, ref line, ref list);
                        xCoordinate++;
                        yCoordinate++;
                    }

                    ExtractAndMatchDiagonal(line, list);

                    line = string.Empty;
                    list = new List<Letter>();
                }
            }
        }

        /// <summary>
        /// Scans along the X axis 
        /// </summary>
        /// <param name="maxRows"></param>
        /// <param name="maxColumns"></param>
        /// <param name="upwardOrDownward"></param>
        private void ScanXAxis(int maxRows, int maxColumns, Directions upwardOrDownward)
        {
            var line = string.Empty;
            var list = new List<Letter>();

            // if direction is upward, scan X axis heading diagonally up 
            if (upwardOrDownward == Directions.Upward)
            {
                for (int i = 1; i <= maxColumns - 1; i++)
                {
                    var xCoordinate = i;
                    var yCoordinate = maxRows;

                    while (xCoordinate <= maxColumns && yCoordinate >= 0)
                    {
                        BuildSearchLineAndList(xCoordinate, yCoordinate, ref line, ref list);
                        xCoordinate++;
                        yCoordinate--;
                    }

                    ExtractAndMatchDiagonal(line, list);

                    line = string.Empty;
                    list = new List<Letter>();
                }
            }
            else
            {
                // if direction is downward, scan X axis heading diagonally down 
                for (int i = 1; i <= maxColumns - 1; i++)
                {
                    var xCoordinate = i;
                    var yCoordinate = 0;

                    while (xCoordinate <= maxColumns && yCoordinate <= maxRows)
                    {
                        BuildSearchLineAndList(xCoordinate, yCoordinate, ref line, ref list);
                        xCoordinate++;
                        yCoordinate++;
                    }

                    ExtractAndMatchDiagonal(line, list);

                    line = string.Empty;
                    list = new List<Letter>();
                }
            }
        }

        /// <summary>
        /// Given coordinates, a line and list by reference, aggregates searchables by character 
        /// </summary>
        /// <param name="xCoordinate"></param>
        /// <param name="yCoordinate"></param>
        /// <param name="line"></param>
        /// <param name="list"></param>
        private void BuildSearchLineAndList(int xCoordinate, int yCoordinate, ref string line, ref List<Letter> list)
        {
            line += _data.Where(x => x.XPosition == xCoordinate && x.YPosition == yCoordinate).First().LetterChar;
            list.Add(new Letter
            {
                LetterChar = _data.Where(x => x.XPosition == xCoordinate && x.YPosition == yCoordinate).First().LetterChar,
                XPosition = xCoordinate,
                YPosition = yCoordinate
            });
        }

        /// <summary>
        /// Given a horizontal/vertical ordered list, call match method 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="horizontalOrVertical"></param>
        /// <param name="forwardOrBackward"></param>
        private void ExtractAndMatchHorizOrVert(List<Letter> list, Directions horizontalOrVertical,
            Directions forwardOrBackward)
        {
            var line = string.Empty;
            var matchDirection = Directions.ForwardVertical;

            if (forwardOrBackward == Directions.Forward)
            {
                line = string.Join("", list.Select(x => x.LetterChar).ToArray());
            }
            else
            {
                line = horizontalOrVertical == Directions.Vertical
                    ? string.Join("", list.OrderByDescending(x => x.YPosition).Select(x => x.LetterChar).ToArray())
                    : string.Join("", list.OrderByDescending(x => x.XPosition).Select(x => x.LetterChar).ToArray());
            }

            if (forwardOrBackward == Directions.Forward && horizontalOrVertical == Directions.Vertical)
                matchDirection = Directions.ForwardVertical;
            else if (forwardOrBackward == Directions.Forward && horizontalOrVertical == Directions.Horizontal)
                matchDirection = Directions.ForwardHorizontal;
            else if (forwardOrBackward == Directions.Backward && horizontalOrVertical == Directions.Vertical)
                matchDirection = Directions.BackwardVertical;
            else if (forwardOrBackward == Directions.Backward && horizontalOrVertical == Directions.Horizontal)
                matchDirection = Directions.BackwardHorizontal;

            FindMatch(line, list, matchDirection);
        }

        /// <summary>
        /// Given a line and ordered, detailed list of diagonal searchables, call match method 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="list"></param>
        private void ExtractAndMatchDiagonal(string line, List<Letter> list)
        {
            // inspect forward matches 
            FindMatch(line, list, Directions.ForwardDiagonal);

            var letters = list.OrderByDescending(x => x.XPosition).Select(x => x.LetterChar).ToList();
            line = string.Join("", letters.ToArray());

            // inspect backward matches 
            FindMatch(line, list, Directions.BackwardDiagonal);
        }
        #endregion

        /// <summary>
        /// Given a search line, a detailed, ordered list and a direction in which to match, collect search matches 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="list"></param>
        /// <param name="matchDirection"></param>
        private void FindMatch(string line, List<Letter> list, Directions matchDirection)
        {
            var searchOutput = string.Empty;
            var position = 0;

            foreach (var searchTerm in _searchTerms)
            {
                if (line.IndexOf(searchTerm) >= 0)
                {
                    searchOutput = searchTerm + ": ";

                    if (matchDirection == Directions.ForwardVertical || matchDirection == Directions.BackwardVertical)
                    {
                        position = matchDirection == Directions.BackwardVertical
                            ? list.Max(x => x.YPosition) - line.IndexOf(searchTerm)
                            : list.Min(x => x.YPosition) + line.IndexOf(searchTerm);

                        list = matchDirection == Directions.BackwardVertical
                            ? list.OrderByDescending(x => x.YPosition)
                                .Where(x => x.YPosition >= position - searchTerm.Length + 1 && x.YPosition <= position)
                                .ToList()
                            : list
                                .Where(x => x.YPosition >= position && x.YPosition <= position + searchTerm.Length - 1)
                                .ToList();
                    }
                    else
                    {
                        position = (matchDirection == Directions.BackwardHorizontal || matchDirection == Directions.BackwardDiagonal)
                            ? list.Max(x => x.XPosition) - line.IndexOf(searchTerm) // backward horizontal and backward diagonal 
                            : list.Min(x => x.XPosition) + line.IndexOf(searchTerm); // forward horizontal and forward diagonal 

                        list = (matchDirection == Directions.BackwardHorizontal || matchDirection == Directions.BackwardDiagonal)
                            ? list.OrderByDescending(x => x.XPosition) // backward horizontal and backward diagonal 
                                .Where(x => x.XPosition >= position - searchTerm.Length + 1 && x.XPosition <= position)
                                .ToList()
                            : list.Where(x => x.XPosition >= position && x.XPosition <= position + searchTerm.Length - 1)
                                .ToList(); // forward horizontal and forward diagonal 
                    }

                    foreach (var l in list)
                    {
                        searchOutput += "(" + l.XPosition.ToString() + "," + l.YPosition.ToString() + "),";
                    }

                    matches.Add(searchOutput.Substring(0, searchOutput.Length - 1));                    
                }
            }
        }
    }
}