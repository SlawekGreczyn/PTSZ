using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace PTSZ
{
    class Program
    {
        static void Main(string[] args)
        {
            int option;
            string filePath;
            Instance instance;
            Stopwatch sw = new Stopwatch();
            int delayTime;
            List<string> resposnes = new List<string>();
            List<string> filesToProcess = new List<string>();

            do
            {
                sw.Reset();
                delayTime = 0;
                Console.Clear();

                option = DisplayMenu();

                Console.Clear();

                switch (option)
                {
                    case 1:
                        Directory.CreateDirectory("Instances");
                        Console.WriteLine( "Feature has been blocked for safety. Enter..." );
                        Console.ReadLine();
                        //Instance.GenerateSetToFiles();
                        break;
                    case 2:
                        Directory.CreateDirectory("Results");
                        filePath = FilesHelper.SelectFile("Instances");
                        sw.Start();
                        instance = Instance.FromFile(filePath);
                        Directory.CreateDirectory("Results/List");
                        SolverGreedy.RunAndSave(instance, filePath.Replace(".txt", ".out.txt").Replace("Instances", "Results/List"), out delayTime);
                        sw.Stop();
                        Console.WriteLine(String.Format("Time of execution: {0} \nDe;ay time: {1}", sw.ElapsedMilliseconds, delayTime ));
                        Console.ReadLine();
                        break;
                    case 3:
                        Directory.CreateDirectory("Results");
                        filePath = FilesHelper.SelectFile("Instances");
                        instance = Instance.FromFile(filePath);
                        Directory.CreateDirectory("Results/Dummy");
                        SolverDummy.RunAndSave(instance, filePath.Replace(".txt", ".out.txt").Replace("Instances", "Results/Dummy"), out delayTime );
                        Console.WriteLine(String.Format("Delay time: {0}", delayTime));
                        Console.ReadLine();
                        break;
                    case 4:
                        Directory.CreateDirectory("Results");
                        filePath = FilesHelper.SelectFile("Instances");
                        instance = Instance.FromFile(filePath);
                        Directory.CreateDirectory("Results/Random");
                        SolverRandom.RunAndSave(instance, filePath.Replace(".txt", ".out.txt").Replace("Instances", "Results/Random"), out delayTime);
                        Console.WriteLine(String.Format("Delay time: {0}", delayTime));
                        Console.ReadLine();
                        break;
                    case 5:
                        Directory.CreateDirectory("Results");
                        filePath = FilesHelper.SelectFile("Instances");
                        instance = Instance.FromFile(filePath);
                        string filePathResults = FilesHelper.SelectFile("Results");
                        bool isValid = Validator.Validate( instance, filePathResults, out delayTime );
                        Console.WriteLine(String.Format( "Validator resilt {0}! - delay is equal to {1}", isValid ? "PASS" : "FAILED", delayTime ));
                        Console.ReadLine();
                        break;
                    case 6:
                        filesToProcess = FilesHelper.GetListOfFilesFromDirectory( "Instances" );
                        Directory.CreateDirectory("Results/List");
                        resposnes = new List<string>();
                        for (int i = 0; i < filesToProcess.Count; i++) {
                            string path = filesToProcess[i];
                            sw.Start();
                            instance = Instance.FromFile(path);
                            SolverGreedy.RunAndSave(instance, path.Replace("in", "out").Replace("Instances", "Results/List"), out delayTime);
                            sw.Stop();
                            resposnes.Add(String.Format( "{0}\t{1}\t{2}", path.Replace("Instances/", "").Replace( ".txt", "" ), delayTime, sw.Elapsed.TotalMilliseconds * 1000 ));
                            sw.Reset();
                            delayTime = 0;
                            drawTextProgressBar( i + 1, filesToProcess.Count );
                        }

                        Console.WriteLine("\n\n\nResults:");
                        foreach (string r in resposnes) {
                            Console.WriteLine(r);
                        }
                        Console.WriteLine("Finished... Press enter to continue...");
                        Console.ReadLine();
                        break;
                    case 7:
                        filesToProcess = FilesHelper.GetListOfFilesFromDirectory("Instances");
                        Directory.CreateDirectory("Results/Dummy");
                        resposnes = new List<string>();
                        for (int i = 0; i < filesToProcess.Count; i++)
                        {
                            string path = filesToProcess[i];
                            sw.Start();
                            instance = Instance.FromFile(path);
                            SolverDummy.RunAndSave(instance, path.Replace(".txt", ".out.txt").Replace("Instances", "Results/Dummy"), out delayTime);
                            sw.Stop();
                            resposnes.Add(String.Format("{0}\t{1}\t{2}", path.Replace("Instances/", "").Replace(".txt", ""), delayTime, sw.Elapsed.TotalMilliseconds * 1000 ));
                            sw.Reset();
                            delayTime = 0;
                            drawTextProgressBar(i + 1, filesToProcess.Count);
                        }

                        Console.WriteLine("\n\n\nResults:");
                        foreach (string r in resposnes)
                        {
                            Console.WriteLine(r);
                        }
                        Console.WriteLine("Finished... Press enter to continue...");
                        Console.ReadLine();
                        break;

                    case 8:
                        filesToProcess = FilesHelper.GetListOfFilesFromDirectory("Instances");
                        Directory.CreateDirectory("Results/Random");
                        resposnes = new List<string>();
                        for (int i = 0; i < filesToProcess.Count; i++)
                        {
                            string path = filesToProcess[i];
                            sw.Start();
                            instance = Instance.FromFile(path);
                            SolverRandom.RunAndSave(instance, path.Replace(".txt", ".out.txt").Replace("Instances", "Results/Random"), out delayTime);
                            sw.Stop();
                            resposnes.Add(String.Format("{0}\t{1}\t{2}", path.Replace("Instances/", "").Replace(".txt", ""), delayTime, sw.Elapsed.TotalMilliseconds * 1000));
                            sw.Reset();
                            delayTime = 0;
                            drawTextProgressBar(i + 1, filesToProcess.Count);
                        }

                        Console.WriteLine("\n\n\nResults:");
                        foreach (string r in resposnes)
                        {
                            Console.WriteLine(r);
                        }
                        Console.WriteLine("Finished... Press enter to continue...");
                        Console.ReadLine();
                        break;

                }
            } while (option != 0);
        }

        static public int DisplayMenu()
        {
            Console.WriteLine("PTSZ Program");
            Console.WriteLine();
            Console.WriteLine("1. Generate data.");
            Console.WriteLine("2. Load and solve from file - greedy");
            Console.WriteLine("3. Load and solve from file - dummy");
            Console.WriteLine("4. Load and solve from file - heurestic");
            Console.WriteLine("5. Verify solution");
            Console.WriteLine("6. Run greedy for all files");
            Console.WriteLine("7. Run dummy for all files");
            Console.WriteLine("8. Run heurestic for all files");
            Console.WriteLine("0. Exit");
            string result = Console.ReadLine();

            return Convert.ToInt32(result);
        }

        private static void drawTextProgressBar(int progress, int total)
        {
            //draw empty progress bar
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 32;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            float onechunk = 30.0f / total;

            //draw filled part
            int position = 1;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress.ToString() + " of " + total.ToString() + "    "); //blanks at the end remove any excess
        }
    }
}
