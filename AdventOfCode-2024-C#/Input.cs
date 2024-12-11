﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_2024_C_
{
    class Input
    {
        private string _path;
        private string? _directory = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;
        public Input(string path) { _path = path; }

        public List<List<int>> GetDay1()
        {
            List<List<int>> numbers = new List<List<int>>();
            string line;
            StreamReader sr = new StreamReader((_directory+_path));
            line = sr.ReadLine();
            while (line != null) 
            {
                string[] splitted = line.Split(null);
                List<int> currentLine = new List<int>();
                foreach(string x in splitted)
                {
                    if (!string.IsNullOrEmpty(x)) currentLine.Add(int.Parse(x));
                }
                numbers.Add(currentLine);
                line = sr.ReadLine();
            }
            sr.Close();
            return numbers;
        }
    }
}