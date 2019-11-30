using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PTSZ
{
    public class Instance
    {
        List<Task> _tasks;
        int _size;

        public Instance(int size, List<Task> tasks) {
            _size = size;
            _tasks = tasks;
        }

        public List<Task> Tasks
        {
            get { return _tasks; }
        }

        public int Size
        {
            get { return _size; }
        }

        public Task GetTask(int id) {
            return _tasks[id];
        } 

        static public Instance FromFile( String path ) {
            string[] lines = File.ReadAllLines(path, Encoding.UTF8);

            int size = Int32.Parse(lines[0]);

            List<Task> tasks = new List<Task>();

            for (int i = 1; i < lines.Length; i++) {
                string[] data = lines[i].Split(' ');
                int pj = Int32.Parse(data[0]);
                int rj = Int32.Parse(data[1]);
                int dj = Int32.Parse(data[2]);

                tasks.Add(new Task(pj, rj, dj, i - 1));
            }

            return new Instance(size, tasks);
        }

        static public void GenerateSetToFiles()
        {
            Console.Clear();
            Console.WriteLine("Generating instances...");

            for (int size = 50; size <= 500; size += 50)
            {
                Console.Write("  size: {0}", size);
                GenerateFile(size);
                Console.WriteLine(" - OK");
            }
        }

        static private void GenerateFile(int size)
        {
            String fileName = String.Format("Instances/Instance_{0}_{1}.txt", "128981", size);
            using (StreamWriter writer = File.CreateText(fileName))
            {
                writer.WriteLine(size);

                List<Task> tasks = Generate(size);

                foreach (Task t in tasks)
                {
                    writer.WriteLine(String.Format("{0} {1} {2}", t.pj, t.rj, t.dj));
                }
            }
        }

        static private List<Task> Generate(int size)
        {
            int pjMax = size / 2;
            int pjMin = size / 10;
            int pjsSum = 0;

            List<int> pjs = new List<int>();
            List<int> rjs = new List<int>();
            List<int> djs = new List<int>();

            Random rand = new Random();

            for (int i = 0; i < size; i++)
            {
                int pj = rand.Next(pjMin, pjMax);
                pjs.Add(pj);
                pjsSum += pj;
            }

            int rjMax = (int)Math.Floor(pjsSum / 4 * 0.75);
            int rjMin = 0;

            for (int i = 0; i < size; i++)
            {
                int rj = rand.Next(rjMin, rjMax);
                rjs.Add(rj);

                int djMax = rj + (int)Math.Floor(pjs[i] * 1.25);
                int djMin = rj + pjs[i];

                int dj = rand.Next(djMin, djMax);
                djs.Add(dj);
            }

            List<Task> result = new List<Task>();

            for (int i = 0; i < size; i++)
            {
                result.Add(new Task(pjs[i], rjs[i], djs[i], i));
            }

            return result;
        }
    }
}
