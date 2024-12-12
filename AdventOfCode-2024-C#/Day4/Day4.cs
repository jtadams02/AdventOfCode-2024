using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.XPath;

namespace AdventOfCode_2024_C_.Day4
{
    class Day4
    {
        private List<string> _wordGrid;
        private Tuple<int, int>[] _GoTo =
            {
                Tuple.Create(1,-1),
                Tuple.Create(1,0),
                Tuple.Create(1,1),
                Tuple.Create(0,-1),
                Tuple.Create(0,1),
                Tuple.Create(-1,-1),
                Tuple.Create(-1,0),
                Tuple.Create(-1,1),
            };
        private HashSet<(int x, int y)> _VisitedHorizontal = new HashSet<(int x, int y)>();
        public Day4(List<string> wordGrid)
        {
            _wordGrid = wordGrid;
        }

        public int Solve()
        {
            int result = 0;
            for (int row = 0; row < _wordGrid.Count; row++)
            {
                for (int col = 0; col < _wordGrid[row].Length; col++)
                {
                    // result += DFS(row, col, "");
                    result += LiveMas(row, col);
                    
                }
            }
            return result;
        }
        
        public int LiveMas(int row, int col)
        {
            int result = 0;
            if (GetCurr(row, col) != "A") { return 0; }
            
            // Make sure we have room above
            if (row-1 >=0 && (col-1>=0 && col+1 < _wordGrid[row - 1].Length))
            {
                // Make sure we have enough room below. Shouldn't need to recheck columns
                if (row+1 < _wordGrid.Count)
                {
                    string leftX = GetCurr(row - 1, col - 1) + GetCurr(row, col) + GetCurr(row + 1, col + 1);
                    string rightX = GetCurr(row + 1, col - 1) + GetCurr(row, col) + GetCurr(row - 1, col + 1);
                    if ((leftX.Equals("SAM") || leftX.Equals("MAS")) && (rightX.Equals("SAM") || rightX.Equals("MAS"))) { result += 1; }
                }
            }

            return result;
        }
        public int DFS(int row, int col, string curr)
        {
            int XMASCount = 0;
            // So we need to go in so many directions
            // * * *
            // * c *
            // * * *
            // We need to make sure we go to ALL of the stars
            XMASCount += VerticalCheck(row, col);
            XMASCount += HorizontalCheck(row, col);
            XMASCount += DiagonalCheck(row, col);

            return XMASCount;
        }

        private int DiagonalCheck(int row, int col)
        {
            int count = 0;
            // Diagonal Check needs to go in 4 directions. Lol...

            // Up and Left
            if (row - 3 >=0 && col -3 >= 0)
            {
                string upleft = GetCurr(row, col);
                for(int i = 1; i <= 3; i++)
                {
                    upleft += GetCurr(row-i, col-i); // Does this work?
                }
                if (upleft.Equals("XMAS")) { count++; }
            }
            // Up and Right
            if (row - 3 >=0 && col + 3 < _wordGrid[row - 3].Length)
            {
                string upright = GetCurr(row, col);
                int rowPtr = row - 1;
                int colPtr = col+1;
                while (rowPtr >= row - 3)
                {
                    upright += GetCurr(rowPtr, colPtr);
                    rowPtr--;
                    colPtr++;
                }
                if (upright.Equals("XMAS")) { count++; }
            }
            // Down and Left
            if (row + 3 < _wordGrid.Count && col - 3 >= 0)
            {
                string downleft = GetCurr(row, col);
                int rowPtr = row + 1;
                int colPtr = col-1;

                while (rowPtr <= row + 3)
                {
                    downleft += GetCurr(rowPtr, colPtr);
                    rowPtr++;
                    colPtr--;
                }
                if (downleft.Equals("XMAS")) { count++; }

            }
            // Down and Right
            if (row+3 < _wordGrid.Count && col +3 < _wordGrid[row + 3].Length)
            {
                string downright = GetCurr(row, col);
                int rowPtr = row + 1;
                int colPtr = col+1;
                
                while (rowPtr <= row + 3)
                {
                    downright += GetCurr(rowPtr, colPtr);
                    rowPtr++;
                    colPtr++;
                }
                if (downright.Equals("XMAS")) { count++; }
            }

            return count;
        }

        private int VerticalCheck(int row, int col)
        {
            if (_wordGrid[row][col] != 'X' && _wordGrid[row][col] != 'S') { return 0; }
            int count = 0;
            if (row + 3 < _wordGrid.Count)
            {
                string down = _wordGrid[row][col].ToString();
                for (int i = row + 1; i <= row + 3; i++)
                {
                    down += _wordGrid[i][col];
                }

                if (down.Equals("SAMX") || down.Equals("XMAS"))
                {
                    count++;
                }
            }
            return count;
        }

        // God I am such an idiot. Spent way too long on this :(
        private int HorizontalCheck(int row, int col)
        {
            if (_wordGrid[row][col] != 'X' && _wordGrid[row][col] != 'S') { return 0; } // Shortened for making debug faster

            int count = 0;
            if (col + 3 < _wordGrid[row].Length) 
            {
                string right = _wordGrid[row][col].ToString();
                for (int i = col + 1; i <= col + 3; i++)
                {
                    right += _wordGrid[row][i];
                }

                if (right.Equals("XMAS") || right.Equals("SAMX"))
                {
                    count++;
                }
            }
            return count;
        }

        private string GetCurr(int row, int col)
        {
            return _wordGrid[row][col].ToString();
        }
    }
}
