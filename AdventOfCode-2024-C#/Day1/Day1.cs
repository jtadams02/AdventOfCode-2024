using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2024_C_.Day1
{
    class Day1
    {
        List<List<int>> _input;

        public Day1(List<List<int>> input) => _input = input;

        public int SolvePartOne() 
        {
            int res = 0;
            List<int> leftList = new List<int>();
            List<int> rightList = new List<int>();

            foreach(List<int> pair in _input)
            {
                leftList.Add(pair[0]);
                rightList.Add(pair[1]);
            }

            leftList.Sort();
            rightList.Sort();
            for (int i = 0; i < leftList.Count; i++)
            {
                res += Math.Abs(leftList[i] - rightList[i]);
            }
            return res;
        }

        public int SolvePartTwo()
        {
            // Need to do the same thing as before
            int res = 0;
            List<int> leftList = new List<int>();
            List<int> rightList = new List<int>();

            foreach (List<int> pair in _input)
            {
                leftList.Add(pair[0]);
                rightList.Add(pair[1]);
            }

            Dictionary<int,int> map = new Dictionary<int,int>();
            foreach(int num in rightList)
            {
                if (map.ContainsKey(num)) map[num]++; else map[num]=1;
            }

            foreach(int num in leftList)
            {
                if (map.ContainsKey(num))
                {
                    res += (num * map[num]);
                }
            }

            return res;
        }
    }
}
