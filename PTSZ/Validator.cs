using System.Collections.Generic;

namespace PTSZ
{
    public class Validator
    {
        public static bool Validate( Instance instance, string resultPath, out int delayTime ) {
            Solution solution = Solution.FromFile(resultPath, instance);

            delayTime = 0;

            for (int i = 0; i < solution.MachinesNum; i++)
            {
                int currentTime = 0;
                List<Task> tasks = solution.GetMachine(i).Tasks;

                foreach (Task task in tasks) {
                    int startTime = currentTime > task.rj ? currentTime : task.rj;

                    currentTime = startTime + task.pj;

                    if (currentTime > task.dj)
                    {
                        delayTime += currentTime - task.dj;
                    }
                }
            }

            return solution.DelayTime == delayTime;
        }
    }
}
