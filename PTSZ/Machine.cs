using System;
using System.Collections.Generic;

namespace PTSZ
{
    public class Machine
    {
        List<Task> _tasks = new List<Task>();
        Task _currentTask;
        int _freeAt = 0;

        public bool IsOcuppatedAt(int currentTime)
        {
            return _freeAt > currentTime;
        }

        public List<Task> Tasks { get { return _tasks; } }
        public Task CurrentTask { get { return _currentTask; } }
        public int FreeAt { get { return _freeAt; } }

        public void AddTask(Task task)
        {
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
