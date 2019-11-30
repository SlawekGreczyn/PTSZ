using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PTSZ
{
    public class Solution
    {
        private Machine[] _machines;
        private int _delyTime;

        public Solution(Machine[] machines, int delayTime)
        {
            _machines = machines;
            _delyTime = delayTime;
        }

        public int DelayTime
        {
            get { return _delyTime; }
        }

        public Machine GetMachine(int i)
        {
            return _machines[i];
        }

        public int MachinesNum
        {
            get { return _machines.Length; }
        }

        public static Solution FromFile(string path, Instance instance)
        {
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);

            int endTime = Int32.Parse(lines[0]);

            Machine[] machines = { new Machine(), new Machine(), new Machine(), new Machine() };

            for (int i = 1; i < lines.Length; i++)
            {
                IEnumerable<int> tasksIds = lines[i].Split(" ").Select(value => int.Parse(value));

                foreach (int id in tasksIds)
                {
                    machines[i - 1].AddTask(instance.GetTask(id));
                }
            }

            return new Solution(machines, endTime);
        }
    }
}
