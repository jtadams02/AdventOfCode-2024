using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace AdventOfCode_2024_C_.Day5
{
    class Day5
    {
        private Dictionary<int, HashSet<int>> _rules;
        private List<string> _pages;

        // Biggest constructor yet for this challenge, what fun!
        public Day5(List<List<string>> parts)
        {
            _pages = parts[1];
            _rules = new Dictionary<int, HashSet<int>>();

            foreach (string part in parts[0])
            {
                string[] set = part.Split('|');
                var ruleKey = int.Parse(set[0]);
                var ruleValue = int.Parse(set[1]);

                if (!_rules.ContainsKey(ruleKey))
                {
                    _rules.Add(ruleKey, new HashSet<int>());
                }
                _rules[ruleKey].Add(ruleValue);
            }
        }

        public int Solve()
        {
            int result = 0;

            foreach (string page  in _pages)
            {
                bool bad = false;
                HashSet<int> set = new HashSet<int>();
                string[] p = page.Split(",");

                foreach (string num in p)
                {
                    if (bad) { break; }
                    int x = int.Parse(num);
                    if (_rules.ContainsKey(x)) {
                        foreach (int rule in _rules[x])
                        {
                            if (bad) { break; }
                            if (set.Contains(rule))
                            {
                                // Send to fix-up here
                                result += TryFixup(p);
                                bad = true;
                            }
                        }
                    }
                    
                    set.Add(x);
                }

                // if (!bad) { result += int.Parse(p[p.Length / 2].ToString()); }
            }

            return result;
        }

        // Definitely CAN solve this with recursion. Don't ASk me why I am being so stupid about it!
        public int TryFixup(string[] page)
        {
            List<string> maniuplateMe = new List<string>(page); // I have no idea WHAT i am doing
            List<string> fixedPage = new List<string>();

            while (maniuplateMe.Count > 0 )
            {
                
                int toRemove = 0;
                for(int i = 0;i < maniuplateMe.Count; i++)
                {
                    bool safe = true;
                    int currentNum = int.Parse(maniuplateMe[i]);
                    for (int j = 0; j < maniuplateMe.Count; j++)
                    {
                        if (!safe) { break; }
                        if (j != i)
                        {
                            int otherNum = int.Parse(maniuplateMe[j]);
                            if (_rules.ContainsKey(otherNum) && _rules[otherNum].Contains(currentNum))
                            {
                                safe = false;
                            }
                        }
                        
                    }
                    if (safe) { toRemove = i; break; }
                }
                fixedPage.Add(maniuplateMe[toRemove]);
                maniuplateMe.Remove(maniuplateMe[toRemove]);
            }

            return int.Parse(fixedPage[fixedPage.Count / 2]);
        }
    }
}
