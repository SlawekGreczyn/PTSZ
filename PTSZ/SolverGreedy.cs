using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PTSZ
{
    public class SolverGreedy
    {
        static public Machine[] Run( Instance instance, out int delayTime ) {
            Machine[] machines = { new Machine(), new Machine(), new Machine(), new Machine() };

            var query = instance.Tasks.OrderBy(task => task.dj);
            List<Task> orderedTasks = query.ToList();

            int currentTime = 0;
            delayTime = 0;

            while (true) {
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

                if (orderedTasks.Count == 0) {
                    break;
                }

                currentTime++;
            }

            return machines;
        }

        public static void RunAndSave( Instance instance, string path, out int delayTimeEx) {
            int delayTime = 0;
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

            delayTimeEx = delayTime;
        }
    }
}
