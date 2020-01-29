using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PTSZ
{
    public class SolverRandom
    {
        static public Machine[] Run( Instance instance, out int delayTime ) {
            int bestDelayTime = int.MaxValue;
            Machine[] bestSolution = null;
            object lockCheck = new object();

            Parallel.For(0, 8, new ParallelOptions { MaxDegreeOfParallelism = 8 }, i =>
            {
                DateTime endTime = DateTime.Now.AddSeconds(60);

                while (DateTime.Now < endTime ) {
                    int tempDelayTime = 0;

                    Machine[] tempSolution = SolverRandom.GenerateRandomSolution(instance, out tempDelayTime);

                    lock (lockCheck)
                    {
                        if (bestSolution == null || bestDelayTime > tempDelayTime)
                        {
                            bestSolution = tempSolution;
                            bestDelayTime = tempDelayTime;
                        }
                    }
                }
            });
            
            delayTime = bestDelayTime;

            return bestSolution;
        }

        public static void RunAndSave( Instance instance, string path, out int delayTimeEx) {
            int delayTime = 0;
            Machine[] solution = SolverRandom.Run(instance, out delayTime);

            using (StreamWriter writer = File.CreateText(path))
            {
                writer.WriteLine(delayTime);

                foreach (Machine machine in solution) {
                    string line = "";

                    foreach (Task task in machine.Tasks) {
                        line += task.j.ToString();
                        line += " ";
                    }

                    line = line.Remove(line.Length - 1);

                    writer.WriteLine(line);
                }
            }

            delayTimeEx = delayTime;
        }

        private static Machine[] GenerateRandomSolution( Instance instance, out int delayTime ) {
            Machine[] machines = { new Machine(), new Machine(), new Machine(), new Machine() };

            Random rnd = new Random();
            var query = instance.Tasks.OrderBy(task => rnd.Next() );
            List<Task> orderedTasks = query.ToList();

            int currentTime = 0;
            delayTime = 0;

            while (true)
            {
                foreach (Machine machine in machines)
                {
                    if (!machine.IsOcuppatedAt(currentTime) && orderedTasks.Count > 0)
                    {
                        int comppletionTime = currentTime + orderedTasks[0].pj;

                        if (comppletionTime - orderedTasks[0].dj > 0)
                        {
                            delayTime += comppletionTime - orderedTasks[0].dj;
                        }

                        machine.AddTask(orderedTasks[0]);
                        orderedTasks.RemoveAt(0);
                    }
                }

                if (orderedTasks.Count == 0)
                {
                    break;
                }

                currentTime++;
            }

            return machines;
        }
    }
}
