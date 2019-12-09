using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace PTSZ
{
    public class SolverGreedy
    {
        static public Machine[] Run( Instance instance, out int delayTime ) {
            Machine[] machines = { new Machine(), new Machine(), new Machine(), new Machine() };

            var query = instance.Tasks.OrderBy(task => task.rj);
            List<Task> orderedTasks = query.ToList();

            Stopwatch sw = new Stopwatch();
            int currentTime = 0;
            delayTime = 0;

            sw.Start();
            while (true) {
                foreach (Machine machine in machines)
                {
                    if (!machine.IsOcuppatedAt(currentTime) && orderedTasks.Count > 0)
                    {
                        if (currentTime + orderedTasks[0].pj > orderedTasks[0].dj) {
                            delayTime += (currentTime + orderedTasks[0].pj) - orderedTasks[0].dj;
                        }

                        machine.AddTask(orderedTasks[0]);
                        orderedTasks.RemoveAt(0);
                    }
                }

                if (orderedTasks.Count == 0) {
                    break;
                }

                currentTime++;
            }
            sw.Stop();
            Console.WriteLine("Time of execution = {0}", sw.ElapsedMilliseconds);

            return machines;
        }

        public static void RunAndSave( Instance instance, string path) {
            int delayTime;
            Machine[] solution = SolverGreedy.Run(instance, out delayTime);

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

            Console.WriteLine(String.Format("Delay time for {0} - {1}", path, delayTime));
            Console.WriteLine("Press ennter to continue....");
            Console.ReadLine();
        }
    }
}
