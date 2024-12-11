using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AdventOfCode_2024_C_.Day2
{
    class Day2
    {
        private List<List<int>> _input;

        public Day2(List<List<int>> input) => _input = input;

        public int Solve()
        {
            int result = 0;
            foreach (List<int> report in _input)
            {
                if (report.Count > 1) // Will always be greater than 1, copilot just REALLY wanted to suggest this so Im letting it think its helping
                {
                    for(int i=0;i<report.Count; i++)
                    {
                        if (DecreasingChecker(report, i))
                        {
                            result++;
                            break;
                        } else if (IncreasingChecker(report, i))
                        {
                            result++;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        private bool DecreasingChecker(List<int> r,int ignore)
        {
            List<int> report = new List<int>(r);
            report.RemoveAt(ignore);
            int k = 1;
            int prev_index = 0;
            for(int i = k; i < report.Count; i++)
            {
                if (report[i] >= report[prev_index] || ((report[prev_index] - report[i]) > 3))
                {
                    return false;
                }
                prev_index++;
            }
            return true;
        }

        private bool IncreasingChecker(List<int> r,int ignore)
        {
            int k = 1;
            List<int> report = new List<int>(r);
            report.RemoveAt(ignore);

            int prev_index = 0;
            for(int i = k; i < report.Count; i++)
            {
                if (report[i] <= report[prev_index] || ((report[i] - report[prev_index]) > 3))
                {
                    return false;
                }
                prev_index++;
            }
            return true;
        }
    }
}
