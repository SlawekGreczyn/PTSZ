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

            var query = instance.Tasks.OrderBy(task => task.rj);
            List<Task> orderedTasks = query.ToList();

            int currentTime = 0;
            delayTime = 0;

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
        }
    }

    public class Machine
    {
        bool _isOcuppated = false;
        List<Task> _tasks = new List<Task>();
        Task _currentTask;
        int _freeAt = 0;

        public bool IsOcuppatedAt( int currentTime ) {
            return _freeAt > currentTime;
        }

        public List<Task> Tasks { get { return _tasks; } }
        public Task CurrentTask { get { return _currentTask; } }
        public int FreeAt { get { return _freeAt; } }

        public void AddTask( Task task ) {
            this._currentTask = task;
            this._tasks.Add(task);

            if (_freeAt <= task.rj)
            {
                _freeAt = task.rj + task.pj;
            }
            else
            {
                _freeAt += task.pj;
            }
        }
    }
}
