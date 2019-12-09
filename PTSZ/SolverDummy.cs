using System;
using System.Collections.Generic;
using System.IO;

namespace PTSZ
{
    public class SolverDummy
    {
        static public Machine[] Run(Instance instance, out int delayTime ) {
            Machine[] machines = { new Machine(), new Machine(), new Machine(), new Machine() };

            List<Task> tasks = instance.Tasks;

            delayTime = 0;

            int limit = (int)Math.Ceiling(tasks.Count / 4.0 );


            for (int i = 0; i < machines.Length; i++)
            {
                int currentTime = 0;

                for (int j = i * limit; j < (limit * (i + 1)); j++)
                {
                    if (tasks.Count <= j)
                    {
                        break;
                    }

                    currentTime = Math.Max(currentTime, tasks[j].rj);

                    int comppletionTime = currentTime + tasks[j].pj;

                    if (comppletionTime - tasks[j].dj > 0)
                    {
                        delayTime += comppletionTime - tasks[j].dj;
                    }

                    currentTime = comppletionTime;

                    machines[i].AddTask(tasks[j]);
                }
            }

            return machines;
        }

        public static void RunAndSave(Instance instance, string path)
        {
            int delayTime;
            Machine[] solution = SolverDummy.Run(instance, out delayTime);

            using (StreamWriter writer = File.CreateText(path))
            {
                writer.WriteLine(delayTime);

                foreach (Machine machine in solution)
                {
                    string line = "";

                    foreach (Task task in machine.Tasks)
                    {
                        line += task.j.ToString();
                        line += " ";
                    }

                    line = line.Remove(line.Length - 1);

                    writer.WriteLine(line);
                }
            }

            Console.WriteLine(String.Format("Delay time for {0} - {1}", path, delayTime));
            Console.WriteLine( "Press ennter to continue...." );
            Console.ReadLine();
        }
    }
}
